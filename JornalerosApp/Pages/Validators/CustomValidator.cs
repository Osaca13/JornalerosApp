using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JornalerosApp.Pages.Validators
{
    public class CustomValidator : ComponentBase, IDisposable
    {
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> PropertyInfoCache = new System.Collections.Concurrent.ConcurrentDictionary<(Type, string), PropertyInfo>();

        [CascadingParameter] public EditContext CurrentEditContext { get; set; }
        [Inject] private IServiceProvider serviceProvider { get; set; }

        private ValidationMessageStore messages;
        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(CustomValidator)} requires a cascading " +
                                                    $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(CustomValidator)} " + "inside an EditForm.");
            }

            this.messages = new ValidationMessageStore(CurrentEditContext);

            // Perform object-level validation on request
            CurrentEditContext.OnValidationRequested += validateModel;

            // Perform per-field validation on each field edit
            CurrentEditContext.OnFieldChanged += ValidateField;
        }

        private void validateModel(object sender, ValidationRequestedEventArgs e)
        {
            messages.Clear();
            var editContext = (EditContext)sender;
            var validationContext = new ValidationContext(editContext.Model);
            validationContext.InitializeServiceProvider(type => this.serviceProvider.GetService(type));
            var validationResults = new List<ValidationResult>();
            bool v = Validator.TryValidateObject(editContext.Model, validationContext, validationResults, true);

            // Transfer results to the ValidationMessageStore
            
            foreach (var validationResult in validationResults)
            {
                if (!validationResult.MemberNames.Any())
                {
                    messages.Add(new FieldIdentifier(editContext.Model, fieldName: string.Empty), validationResult.ErrorMessage);
                    continue;
                }

                foreach (var memberName in validationResult.MemberNames)
                {
                    messages.Add(editContext.Field(memberName), validationResult.ErrorMessage);
                }
            }
            
            editContext.NotifyValidationStateChanged();
        }

        private void ValidateField(object? sender, FieldChangedEventArgs e)
        {
            if (!TryGetValidatableProperty(e.FieldIdentifier, out var propertyInfo)) return;

            var propertyValue = propertyInfo.GetValue(e.FieldIdentifier.Model);
            var validationContext = new ValidationContext(CurrentEditContext.Model) { MemberName = propertyInfo.Name };
            validationContext.InitializeServiceProvider(type => this.serviceProvider.GetService(type));

            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, validationContext, results);
            messages.Clear(e.FieldIdentifier);
            messages.Add(e.FieldIdentifier, results.Select(result => result.ErrorMessage));

            // We have to notify even if there were no messages before and are still no messages now,
            // because the "state" that changed might be the completion of some async validation task
            CurrentEditContext.NotifyValidationStateChanged();
        }

        private static bool TryGetValidatableProperty(in FieldIdentifier fieldIdentifier, [NotNullWhen(true)] out PropertyInfo propertyInfo)
        {
            var cacheKey = (ModelType: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);

            if (PropertyInfoCache.TryGetValue(cacheKey, out propertyInfo)) return true;

            // DataAnnotations only validates public properties, so that's all we'll look for
            // If we can't find it, cache 'null' so we don't have to try again next time
            propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

            // No need to lock, because it doesn't matter if we write the same value twice
            PropertyInfoCache[cacheKey] = propertyInfo;

            return propertyInfo != null;
        }

        public void Dispose()
        {
            if (CurrentEditContext == null) return;
            CurrentEditContext.OnValidationRequested -= validateModel;
            CurrentEditContext.OnFieldChanged -= ValidateField;
        }

    }
}

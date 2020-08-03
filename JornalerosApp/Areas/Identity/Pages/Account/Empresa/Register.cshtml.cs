using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using JornalerosApp.Shared.Models;
using JornalerosApp.Shared.Data.Migrations;
using JornalerosApp.Data;
using JornalerosApp.Shared.Services;
using JornalerosApp.Services;

namespace JornalerosApp.Areas.Identity.Pages.Account.Empresa
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IDbServices<JornalerosApp.Shared.Models.Empresa> _empresaServices;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IDbServices<JornalerosApp.Shared.Models.Empresa> empresaServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
            _empresaServices = empresaServices;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        private Shared.Models.Empresa Empresa { get; set; } = new Shared.Models.Empresa();
        public IList<AuthenticationScheme> ExternalLogins { get; set; }        

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Correo Electrónico")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nombre de Empresa")]
            public string NombreEmpresa { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Persona de Contacto")]
            public string NombreContacto { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Cargo")]
            public string Cargo { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Actividad")]
            public string Actividad { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Telefono")]
            public decimal? Telefono { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Dirección")]
            public string Dirección { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Código Postal")]
            public string CodigoPostal { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Provincia")]
            public string Provincia { get; set; }

            [Required]
            [ValidacionNIF]
            [StringLength(9, MinimumLength = 9)]
            [DataType(DataType.Text)]
            [Display(Name = "CIF/NIF")]
            public string CIF { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            string route = this.RouteData.Values.Where(p => p.Key == "page").First().Value.ToString().Split("/")[2];

            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code, returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    if (await _roleManager.FindByNameAsync(route) == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = route,
                            NormalizedName = route.ToUpper(),
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });
                    }
                    IdentityResult roleResult = await _userManager.AddToRoleAsync(user, route);
                    await CrearEmpresa(user);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private async Task CrearEmpresa(IdentityUser user)
        {
            Empresa.CorreoElectronico = user.Email;
            Empresa.NombreEmpresa = this.Input.NombreEmpresa;
            Empresa.NombreContacto = this.Input.NombreContacto;
            Empresa.Nifcif = this.Input.CIF;
            Empresa.Cargo = this.Input.Cargo;
            Empresa.IdEmpresa = user.Id;
            Empresa.Actividad = Input.Actividad;
            Empresa.Provincia = Input.Provincia;
            Empresa.Telefono = Input.Telefono;
            Empresa.Dirección = Input.Dirección;
            Empresa.CodigoPostal = Input.CodigoPostal;
            await _empresaServices.AddItem(Empresa);
        }

        private void ComprobarNIF()
        {

        }
    }
}

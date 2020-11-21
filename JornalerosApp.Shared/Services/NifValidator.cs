using System.ComponentModel.DataAnnotations;

namespace JornalerosApp.Shared.Services
{
    public class NifValidatorAttribute : ValidationAttribute
    {
        private string _message;
        public NifValidatorAttribute():base("Documento no válido")
        {
            
        }

        private string dni { get; set; }

        public override bool IsValid(object value)
        {
            if (NumeroDNI(value as string))
            {
                return true;
            }
            return false;
        }

        //public new PropertyValidatorOptions Options { get; }

        //protected override bool IsValid(PropertyValidatorContext context)
        //{
        //    var persona = context.InstanceToValidate;
        //    if (NumeroDNI(context.PropertyValue as string))
        //    {
        //        return true;
        //    }
        //    return false; 
        //}

        private bool NumeroDNI(string numero)
        {
            char[] array = numero.ToCharArray();
            string resulDNI = string.Empty;
            if (char.IsNumber(array[0]))
            {
                resulDNI = string.Concat(array[0]);
                dni = "NIF";
            }
            else
            {
                dni = "NIE";
                if (array[0].Equals("X"))
                {
                    resulDNI = string.Concat("0");
                }
                if (array[0].Equals("Y"))
                {
                    resulDNI = string.Concat("1");
                }
                if (array[0].Equals("Z"))
                {
                    resulDNI = string.Concat("2");
                }
            }
            for (int j = 1; j < 8; j++)
            {
                resulDNI = string.Concat(resulDNI, array[j].ToString());
            }
            var ultimaletra = array[8].ToString();
            return Modulo23(int.Parse(resulDNI)).Equals(ultimaletra);
        }

        private static string Modulo23(int numeroDNI)
        {
            int result = numeroDNI % 23;
            string letra = string.Empty;
            switch (result)
            {
                case 0:
                    {
                        letra = "T";
                        break;
                    }
                case 1:
                    {
                        letra = "R";
                        break;
                    }
                case 2:
                    {
                        letra = "W";
                        break;
                    }
                case 3:
                    {
                        letra = "A";
                        break;
                    }
                case 4:
                    {
                        letra = "G";
                        break;
                    }
                case 5:
                    {
                        letra = "M";
                        break;
                    }
                case 6:
                    {
                        letra = "Y";
                        break;
                    }
                case 7:
                    {
                        letra = "F";
                        break;
                    }
                case 8:
                    {
                        letra = "P";
                        break;
                    }
                case 9:
                    {
                        letra = "D";
                        break;
                    }
                case 10:
                    {
                        letra = "X";
                        break;
                    }
                case 11:
                    {
                        letra = "B";
                        break;
                    }
                case 12:
                    {
                        letra = "N";
                        break;
                    }
                case 13:
                    {
                        letra = "J";
                        break;
                    }
                case 14:
                    {
                        letra = "Z";
                        break;
                    }
                case 15:
                    {
                        letra = "S";
                        break;
                    }
                case 16:
                    {
                        letra = "Q";
                        break;
                    }
                case 17:
                    {
                        letra = "V";
                        break;
                    }
                case 18:
                    {
                        letra = "H";
                        break;
                    }
                case 19:
                    {
                        letra = "L";
                        break;
                    }
                case 20:
                    {
                        letra = "C";
                        break;
                    }
                case 21:
                    {
                        letra = "K";
                        break;
                    }
                case 22:
                    {
                        letra = "E";
                        break;
                    }
            }
            return letra;
        }

        //public override IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        //{
        //    var persona = context.InstanceToValidate;
        //    var resul = new List<ValidationFailure>();
        //    if (!NumeroDNI(context.PropertyValue as string))
        //    {
        //        resul.Add(new ValidationFailure(context.PropertyName, dni +" no válido"));                
        //    }
        //    return resul;
        //}

        //public override async Task<IEnumerable<ValidationFailure>> ValidateAsync(PropertyValidatorContext context, CancellationToken cancellation)
        //{
        //    var resul = new List<ValidationFailure>();
        //    await Task.Run(() => 
        //    {                
        //       if (!NumeroDNI(context.PropertyValue as string))
        //       {
        //           resul.Add(new ValidationFailure(context.PropertyName, "No válido"));
        //       }               
        //    });
        //    return resul;
        //}

        //public override bool ShouldValidateAsynchronously(IValidationContext context)
        //{
        //    return false;
        //}

        //protected override bool IsValid(PropertyValidatorContext context)
        //{           
        //    if (NumeroDNI(context.PropertyValue as string))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}

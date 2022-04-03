using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EconomicManagementAPP.Validations
{
    public class PasswordCapitalLetter : ValidationAttribute
    {
        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            string password = value.ToString();
            int length = password.Length;
            var flag = false;
            Regex rxCapital = new Regex("[A-Z]");

            for (int i = 0; i < length; i++)
            {

                if (rxCapital.IsMatch(password.ToString()[i].ToString()))
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                return new ValidationResult("The password should have min one capital letter");
            }


            return ValidationResult.Success;
        }
    }
}

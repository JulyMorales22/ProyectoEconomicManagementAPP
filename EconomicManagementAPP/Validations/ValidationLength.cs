using System.ComponentModel.DataAnnotations;



namespace EconomicManagementAPP.Validations
{
    public class ValidationLength : ValidationAttribute
    {
        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            string password = value.ToString();
            int length = password.Length;

            if (length < 8)
            {
                return new ValidationResult("The password should have min eight characters");
            }
            return ValidationResult.Success;
        }
    }
}

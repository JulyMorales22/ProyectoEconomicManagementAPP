using EconomicManagementAPP.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;


namespace EconomicManagementAPP.test
{
    [TestClass]
    public class ValidationsPasswordTest
    {
        [TestMethod]
        public void ValidationsPasswordLength_ReturnError()
        {
            var validationsPassword = new ValidationLength();
            var data = "July";

            var context = new ValidationContext(new { Password = data });

            var testResult = validationsPassword.GetValidationResult(data, context);

            Assert.AreEqual("The password should have min eight characters", testResult?.ErrorMessage);

        }
        [TestMethod]
        public void ValidationsPassword_ReturnError()
        {
            var validationsPassword = new ValidationsPassword();
            var data = "JulyMorales";

            var context = new ValidationContext(new { Password = data });

            var testResult = validationsPassword.GetValidationResult(data, context);

            Assert.AreEqual("The password should have min one number ", testResult?.ErrorMessage);

        }
        [TestMethod]
        public void PasswordCapitalLetter_ReturnError()
        {
            var validationsPassword = new PasswordCapitalLetter();
            var data = "julymorales";

            var context = new ValidationContext(new { Password = data });

            var testResult = validationsPassword.GetValidationResult(data, context);

            Assert.AreEqual("The password should have min one capital letter", testResult?.ErrorMessage);

        }

        [TestMethod]
        public void NullData_NoErrorMessage()
        {
            var validationPassword = new PasswordCapitalLetter();
            string data = "";

            var context = new ValidationContext(new { Password = data });

            var testResult = validationPassword.GetValidationResult(data, context);

            Assert.IsNull(testResult);
        }

    }
}

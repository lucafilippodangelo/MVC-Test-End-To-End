using BankingSite.Controllers;
using BankingSite.FunctionalUITests.PageObjectModels;
using BankingSite.Models;
using NUnit.Framework;

namespace BankingSite.FunctionalUITests
{
    [TestFixture]
    public class LoanApplicationTests
    {
        [Test]
        public void ShouldAcceptLoanApplication()
        {
            var applyPage =
                BrowserHost.Instance.NavigateToInitialPage<LoanApplicationController, LoanApplicationPage>(
                    x => x.Apply());

            var applicationDetails = new LoanApplication
            {
                FirstName = "Gentry",
                LastName = "Smith",
                Age = 42,
                AnnualIncome = 99999999
            };

            var acceptPage= applyPage.SubmitApplication<AcceptedPage>(applicationDetails);

            var acceptMessageText = acceptPage.AcceptedMessage;

            Assert.That(acceptMessageText, Is.EqualTo("Congratulations Gentry - Your application was accepted!"));            
        }


        [Test]
        public void ShouldDeclineLoanApplication()
        {
            var applyPage =
                BrowserHost.Instance.NavigateToInitialPage<LoanApplicationController, LoanApplicationPage>(
                    x => x.Apply());


            var applicationDetails = new LoanApplication
            {
                FirstName = "Gentry",
                LastName = "Smith",
                Age = 16,
                AnnualIncome = 20000
            };


            var declinePage = applyPage.SubmitApplication<DeclinedPage>(applicationDetails);

            Assert.That(declinePage.DeclinedMessage, Is.EqualTo("Sorry Gentry - We are unable to offer you a loan at this time."));                       
        }

    }
}

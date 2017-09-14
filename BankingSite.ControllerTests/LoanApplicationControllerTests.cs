using BankingSite.Controllers;
using BankingSite.Models;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BankingSite.ControllerTests
{
    [TestFixture]
    public class LoanApplicationControllerTests
    {
        //LD STEP11
        [Test]
        public void ShouldRenderDefaultView()
        {
            //LD we need to create some fuck instances by "mock" to pass in input to the controller
            var fakerepository = new Mock<IRepository>();
            var fakeLoanApplicationScorer = new Mock<ILoanApplicationScorer>();

            //LD here I call the controller, important to say that for this specific action I 
            // don't need of specific implementation of instances in input
            var sut = new LoanApplicationController(fakerepository.Object, fakeLoanApplicationScorer.Object);

            //LD STEP12 - here we are using "Fluent MVC Testing"
            sut.WithCallTo(x => x.Apply()).ShouldRenderDefaultView();
        }

        //LD STEP13
        [Test]
        public void ShouldRedirectToAcceptedViewOnSuccessfulApplication()
        {
            var fakerepository = new Mock<IRepository>();
            var fakeLoanApplicationScorer = new Mock<ILoanApplicationScorer>();

            //I get the controller
            var sut = new LoanApplicationController(fakerepository.Object, fakeLoanApplicationScorer.Object);

            // I prepare the input to pass to the controller call
            var acceptedApplication = new LoanApplication
            {
                IsAccepted = true
            };

            //Here we verify that when we call the action "Apply" giving a "Loan Application" where "Is Accepted"
            // is true, the redirect is to the "Accepted" view.
            sut.WithCallTo(x => x.Apply(acceptedApplication)).ShouldRedirectTo<int>(x => x.Accepted);
        }

        //LD STEP13 
        [Test]
        public void ShouldRedirectToDeclinedViewOnUnsuccessfulApplication()
        {
            var fakerepository = new Mock<IRepository>();
            var fakeLoanApplicationScorer = new Mock<ILoanApplicationScorer>();

            var sut = new LoanApplicationController(fakerepository.Object, fakeLoanApplicationScorer.Object);

            var declinedApplication = new LoanApplication
            {
                IsAccepted = false
            };


            sut.WithCallTo(x => x.Apply(declinedApplication))
               .ShouldRedirectTo<int>(x => x.Declined);
        }
    }
}

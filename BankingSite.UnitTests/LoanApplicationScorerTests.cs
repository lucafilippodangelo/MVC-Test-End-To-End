using BankingSite.Models;
using Moq;
using NUnit.Framework;

namespace BankingSite.UnitTests
{
    //LD STEP4
    [TestFixture]
    public class LoanApplicationScorerTests
    {
        [Test]
        public void ShouldDeclineWhenTooYoung()
        {
            var fakeCreditHistoryChecker = new Mock<ICreditHistoryChecker>();            

            fakeCreditHistoryChecker.Setup(
                x => x.CheckCreditHistory(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            var sut = new LoanApplicationScorer(fakeCreditHistoryChecker.Object);

            var application = new LoanApplication
                              {
                                  Age = 21
                              };

            sut.ScoreApplication(application);

            Assert.That(application.IsAccepted, Is.False);
        }


        [Test]
        public void ShouldAcceptWhenYoungButWealthy()
        {
            var fakeCreditHistoryChecker = new Mock<ICreditHistoryChecker>();

            fakeCreditHistoryChecker.Setup(
                x => x.CheckCreditHistory(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            var sut = new LoanApplicationScorer(fakeCreditHistoryChecker.Object);

            var application = new LoanApplication
            {
                AnnualIncome = 1000000.01m,
                Age = 21
            };

            sut.ScoreApplication(application);

            Assert.That(application.IsAccepted, Is.True);
        }

        //LD STEP6
        [Test]
        public void ShouldDeclineWhenNotTooYoungAndWealthyButPoorCredit()
        {
            //LD creating a fake "ICreditHistoryChecker" instance, in order to pass it to "LoanApplicationScorer"
            // like in "//LD STEP5"
            var fakeCreditHistoryChecker = new Mock<ICreditHistoryChecker>();

            //LD STEP7 we express inside "Setup" that I want specify the method "CheckCreditHistory". 
            // In this case for any input, the return will be "false".
            fakeCreditHistoryChecker.Setup(
                x => x.CheckCreditHistory(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);


            var sut = new LoanApplicationScorer(fakeCreditHistoryChecker.Object);

            var application = new LoanApplication
            {
                AnnualIncome = 1000000.01m,
                Age = 22
            };

            sut.ScoreApplication(application);

            Assert.That(application.IsAccepted, Is.False);
        }

        //LD STEP5
        [Test]
        public void ShouldDeclineWhenNotTooYoungAndWealthyButPoorCredit_Classical()
        {
            var sut = new LoanApplicationScorer(new CreditHistoryChecker());

            var application = new LoanApplication
            {
                // Need to specify criteria that will cause 
                // real CreditHistoryChecker to decline
                FirstName = "Sarah",
                LastName = "Smith",

                AnnualIncome = 1000000.01m,
                Age = 22
            };

            sut.ScoreApplication(application);

            Assert.That(application.IsAccepted, Is.False);
        }
    }
}

using System.Net;
using BankingSite.Controllers;
using BankingSite.Models;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BankingSite.ControllerTests
{
    [TestFixture]
    public class LoanApplicationSearchControllerTests
    {
        [Test]
        public void ShouldReturn404StatusWhenLoanIdNotExists()
        {
            var fakerepository = new Mock<IRepository>();

            var sut = new LoanApplicationSearchController(fakerepository.Object);

            sut.WithCallTo(x => x.ApplicationStatus(99)).ShouldGiveHttpStatus(HttpStatusCode.NotFound);
        }

        [Test]
        public void ShouldRenderApplicationWhenIdExists()
        {
            var fakerepository = new Mock<IRepository>();

            fakerepository.Setup(x => x.Find(99))
                          .Returns(new LoanApplication { FirstName = "Larry" });

            var sut = new LoanApplicationSearchController(fakerepository.Object);

            sut.WithCallTo(x => x.ApplicationStatus(99))
                .ShouldRenderDefaultView()
                .WithModel<LoanApplication>(x => x.FirstName == "Larry");


        }



    }
}

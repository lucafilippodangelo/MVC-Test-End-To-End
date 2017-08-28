using OpenQA.Selenium;
using TestStack.Seleno.PageObjects;

namespace BankingSite.FunctionalUITests.PageObjectModels
{
    public class DeclinedPage : Page
    {
        public string DeclinedMessage
        {
            get
            {
                return Find.Element(By.Id("declineMessage")).Text;
            }
        }
    }
}
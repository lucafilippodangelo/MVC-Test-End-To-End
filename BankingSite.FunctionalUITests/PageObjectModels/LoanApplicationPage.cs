using BankingSite.Models;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Locators;

namespace BankingSite.FunctionalUITests.PageObjectModels
{
    public class LoanApplicationPage : Page<LoanApplication>
    {      
        public T SubmitApplication<T>(LoanApplication application) where T : UiComponent, new()
        {
            Input.Model(application);

            return Navigate.To<T>(By.Id("Apply"));
        }
    }
}

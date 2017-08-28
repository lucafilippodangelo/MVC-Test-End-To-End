using System;
using NUnit.Framework;

namespace BankingSite.FunctionalUITests
{
    [SetUpFixture]
    public class TestFixtureLifecycle : IDisposable
    {        
        public void Dispose()              
        {
            // Cleanup and close browser
            BrowserHost.Instance.Dispose();
        }
    }
}

using NUnit.Framework;
using Moq;
using Browser;
using Browser.JavaScriptCalls;
using System.Collections;
using ProductConfig;

namespace BrowserTest
{
    [TestFixture]
    public class BrowserTest
    {
        Mock<IBrowser> browser;

        ICallsToJs callsToJs;

        object[] Obj;

        [SetUp]
        public void SetupTest()
        {
            browser = new Mock<IBrowser>();
            callsToJs = new CallsToJs(browser.Object);
            Obj = new object[] { };

        }

        [Test]
        public void TestCallLoadImages()
        {
            var arrayList = new ArrayList() { "url1", "url2" };
            browser.Setup(p => p.InvokeScriptAsync(Config.LoadImages, "url1;url2")).Verifiable();
            callsToJs.LoadImages(arrayList);
            browser.VerifyAll();
        }

        [Test]
        public void TestCallShowLoading()
        {
            browser.Setup(p => p.InvokeScriptAsync(Config.LoadingPage, Obj)).Verifiable();
            callsToJs.ShowLoading();
            browser.VerifyAll();
        }

        [Test]
        public void TestCallShowNoInternet()
        {
            browser.Setup(p => p.InvokeScriptAsync(Config.LoadNoInternet, Obj)).Verifiable();
            callsToJs.ShowNoInternet();
            browser.VerifyAll();
        }

        [Test]
        public void TestCallShowNoInmagesPage()
        {
            browser.Setup(p => p.InvokeScriptAsync(Config.LoadNoResultsPage, Obj)).Verifiable();
            callsToJs.ShowNoImages();
            browser.VerifyAll();
        }

    }
}

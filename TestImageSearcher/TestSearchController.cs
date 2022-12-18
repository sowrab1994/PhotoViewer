using NUnit.Framework;
using Browser.JavaScriptCalls;
using Browser;
using ImageSearcher;
using Moq;
using System.Collections;
using System.Threading.Tasks;
using System;

namespace TestPhotoViewer
{

    [TestFixture]
    internal class TestSearchController
    {
        SearchController controller;
        Mock<ICallsToJs> callsToJs;
        Mock<IBrowser> browser;
        Mock<IImageSearchService> imgSearcher;
        bool IsStackAddedEventHandlerCalled;
        bool IsStackEmptyEventHandlerCalled;



        [SetUp]
        public void SetupTest()
        {
            callsToJs = new Mock<ICallsToJs>();
            browser = new Mock<IBrowser>();
            imgSearcher= new Mock<IImageSearchService>();
            controller = new SearchController(browser.Object, imgSearcher.Object, callsToJs.Object);
            IsStackAddedEventHandlerCalled = false;
            IsStackEmptyEventHandlerCalled = false;
        }

        [Test]
        public void TestQueryImagesForFirstTimeAndCheckStack()
        { 
            var searchResponse = new SearchResponse();
            searchResponse.ImagesArray = new ArrayList() { "testurl" };
            searchResponse.IsRequestSuccessful = true;
            searchResponse.ResponsePages = 1;
            imgSearcher.Setup(p => p.SetPage(1)).Verifiable();
            imgSearcher.Setup(p => p.GetImagesForSearchString(It.IsAny<string>())).Returns(searchResponse);
            callsToJs.Setup(p => p.ShowLoading()).Verifiable();
            callsToJs.Setup(p => p.LoadImages(It.IsAny<ArrayList>())).Verifiable();
            Task t = controller.QueryImages("SampleText");
            t.Wait();
            Assert.IsTrue(t.IsCompleted);
            var stack = controller.GetSearchStack();
            Assert.AreEqual(1, stack.Count);
            Tuple<string, int> tuple;
            stack.TryPeek(out tuple);
            Assert.AreEqual(new Tuple<string, int>("SampleText", 1),tuple);
            callsToJs.VerifyAll();
            imgSearcher.VerifyAll();
        }

        [Test]
        public void TestQueryImagesWhenHttpRequestFails()
        {
            var searchResponse = new SearchResponse();
            searchResponse.ImagesArray = new ArrayList() {  };
            searchResponse.IsRequestSuccessful = false;
            searchResponse.ResponsePages = 0;
            imgSearcher.Setup(p => p.GetImagesForSearchString(It.IsAny<string>())).Returns(searchResponse);
            imgSearcher.Setup(p => p.SetPage(1)).Verifiable();
            callsToJs.Setup(p => p.ShowLoading()).Verifiable();
            callsToJs.Setup(p => p.ShowNoInternet()).Verifiable();
            Task t = controller.QueryImages("SampleText");
            t.Wait();
            Assert.IsTrue(t.IsCompleted);
            callsToJs.VerifyAll();
            imgSearcher.VerifyAll();
        }

        [Test]
        public void TestQueryImagesWhenNoImagesAreReturned()
        {
            var searchResponse = new SearchResponse();
            searchResponse.ImagesArray = new ArrayList() { };
            searchResponse.IsRequestSuccessful = true;
            searchResponse.ResponsePages = 0;
            imgSearcher.Setup(p => p.SetPage(1)).Verifiable();
            imgSearcher.Setup(p => p.GetImagesForSearchString(It.IsAny<string>())).Returns(searchResponse);
            callsToJs.Setup(p => p.ShowLoading()).Verifiable();
            callsToJs.Setup(p => p.ShowNoImages()).Verifiable();
            Task t = controller.QueryImages("SampleText");
            t.Wait();
            Assert.IsTrue(t.IsCompleted);
            callsToJs.VerifyAll();
            imgSearcher.VerifyAll();
        }

        [Test]
        public void TestEventHandlerWhenTwoQueriesAreMade()
        {
            var searchResponse = new SearchResponse();
            searchResponse.ImagesArray = new ArrayList() { "testurl" };
            searchResponse.IsRequestSuccessful = true;
            searchResponse.ResponsePages = 1;
            imgSearcher.Setup(p => p.SetPage(1)).Verifiable();
            imgSearcher.Setup(p => p.GetImagesForSearchString(It.IsAny<string>())).Returns(searchResponse);
            callsToJs.Setup(p => p.ShowLoading()).Verifiable();
            callsToJs.Setup(p => p.LoadImages(It.IsAny<ArrayList>())).Verifiable();

            SearchController.OnStackAdded += StackAddedEventHandler;

            Task t1 = controller.QueryImages("SampleText");
            t1.Wait();

            Task t2 = controller.QueryImages("SampleText2");
            t2.Wait();

            var stack = controller.GetSearchStack();
            Assert.AreEqual(2, stack.Count);
            Tuple<string, int> tuple;
            stack.TryPeek(out tuple);
            Assert.AreEqual(new Tuple<string, int>("SampleText2", 1), tuple);
            callsToJs.VerifyAll();
            imgSearcher.VerifyAll();
            Assert.IsTrue(IsStackAddedEventHandlerCalled);
        }

        [Test]
        public void TestQueryImagesofNextPageAndPreviousPage()
        {
            SearchController.OnStackEmpty += StackEmptyEventHandler;
            var searchResponse = new SearchResponse();
            searchResponse.ImagesArray = new ArrayList() { "testurl" };
            searchResponse.IsRequestSuccessful = true;
            searchResponse.ResponsePages = 1;
            imgSearcher.Setup(p => p.SetPage(1)).Verifiable();
            imgSearcher.Setup(p => p.GetImagesForSearchString(It.IsAny<string>())).Returns(searchResponse);
            callsToJs.Setup(p => p.ShowLoading()).Verifiable();
            callsToJs.Setup(p => p.LoadImages(It.IsAny<ArrayList>())).Verifiable();
            Task t = controller.QueryImages("SampleText");
            t.Wait();

            imgSearcher.Setup(p => p.SetPage(2)).Verifiable();

            controller.QueryImagesOfNextPage();

            var stack = controller.GetSearchStack();
            Assert.AreEqual(2, stack.Count);


            Assert.IsTrue(t.IsCompleted);
           
            Tuple<string, int> tuple;
            stack.TryPeek(out tuple);
            Assert.AreEqual(new Tuple<string, int>("SampleText", 2), tuple);
            callsToJs.VerifyAll();
            imgSearcher.VerifyAll();
            Assert.IsTrue(IsStackAddedEventHandlerCalled);

            controller.QueryImagesOfPreviousPage();
            Assert.AreEqual(1, controller.GetSearchStack().Count);
            Assert.IsTrue(IsStackEmptyEventHandlerCalled);

        }

        private void StackEmptyEventHandler(object sender, EventArgs e)
        {
            IsStackEmptyEventHandlerCalled= true;
        }

        private void StackAddedEventHandler(object sender, EventArgs e)
        {
            IsStackAddedEventHandlerCalled= true;
        }
    }
}

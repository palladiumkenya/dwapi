using System;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using NUnit.Framework;

namespace Dwapi.ExtractsManagement.Core.Tests.Model
{
    [TestFixture]
    public class ExtractHistoryTests
    {
        private ExtractHistory _extractHistory;
        [SetUp]
        public void SetUp()
        {
            _extractHistory=new ExtractHistory();
        }

        [Test]
        public void should_Start_On_Idle()
        {
            Assert.AreEqual(ExtractStatus.Idle, _extractHistory.Status);
            Console.WriteLine(_extractHistory);
        }

        [Test]
        public void should_Update_ExtractHistory()
        {
            // Idle
            Assert.AreEqual(ExtractStatus.Idle,_extractHistory.Status);
            // Idle > Finding
            var finding = ExtractHistory.UpdateHistory(_extractHistory);
            Assert.AreEqual(ExtractStatus.Finding, finding.Status);
            // Finding > Loading
            var loading = ExtractHistory.UpdateHistory(finding);
            Assert.AreEqual(ExtractStatus.Loading, loading.Status);
            // Loading > Validating
            var validating = ExtractHistory.UpdateHistory(loading);
            Assert.AreEqual(ExtractStatus.Validating, validating.Status);
            // Validating > Sending
            var sending = ExtractHistory.UpdateHistory(validating);
            Assert.AreEqual(ExtractStatus.Sending, sending.Status);

            Console.WriteLine(_extractHistory);
            Console.WriteLine(finding);
            Console.WriteLine(loading);
            Console.WriteLine(validating);
            Console.WriteLine(sending);
        }

        [Test]
        public void should_Update_ExtractHistory_Stats()
        {
            // Found
            var finding = ExtractHistory.UpdateHistory(_extractHistory);
            Assert.AreEqual(ExtractStatus.Finding, finding.Status);
            var found = ExtractHistory.UpdateHistoryStats(finding, 10);
            Assert.AreEqual(10, found.Found);
            Assert.AreEqual(DateTime.Now.Date, found.DateFound.Value.Date);
            Assert.AreEqual(ExtractStatus.Loading, found.Status);

            //Loaded
            var loading = ExtractHistory.UpdateHistory(finding);
            Assert.AreEqual(ExtractStatus.Loading, loading.Status);
            var loaded = ExtractHistory.UpdateHistoryStats(loading, 10);
            Assert.AreEqual(10, loaded.Found);
            Assert.AreEqual(DateTime.Now.Date, loaded.DateFound.Value.Date);
            Assert.AreEqual(ExtractStatus.Validating, loaded.Status);





            Console.WriteLine(_extractHistory);
            Console.WriteLine(finding);
            Console.WriteLine(found);
            Console.WriteLine(loading);
            Console.WriteLine(loaded);
            /*
           Console.WriteLine(validating);
           Console.WriteLine(validated);

           Console.WriteLine(sending);
           Console.WriteLine(sent);
           */
        }
    }
}
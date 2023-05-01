using System.Collections.ObjectModel;
using TicketsSystem.Models;
using TicketsSystem.Services;

namespace TestTicketsSystem
{
    public class CalculateTicketsPricesUnitTest
    {
        private IList<TicketsDetails> tickets;

        [OneTimeSetUp]
        public void Setup()
        {
            tickets = new List<TicketsDetails>();
        }

        [Test]
        [TestCase(0, false, "2023-4-15 6:0:0", 9.5)]
        [TestCase(0, true, "2023-4-15 6:0:0", 8.55)]
        [TestCase(0, true, "2023-4-15 8:0:0", 10)]
        [TestCase(1, false, "2023-4-15 6:0:0", 4.75)]
        [TestCase(1, true, "2023-4-15 6:0:0", 4.75)]
        [TestCase(1, true, "2023-4-15 8:0:0", 10)]
        [TestCase(2, false, "2023-4-15 6:0:0", 6.27)]
        [TestCase(2, true, "2023-4-15 6:0:0", 6.27)]
        [TestCase(2, true, "2023-4-15 8:0:0", 10)]

        public void CalculateTicketsPricesTest(int typeOfCard, bool hasChild, DateTime startTime, decimal expectedResult)
        {
            TicketsDetails ticket = new TicketsDetails(String.Empty, String.Empty, startTime, startTime, 10);
            tickets.Add(ticket);

            CalculationsService calculationsService = new CalculationsService();
            ObservableCollection<TicketsInformation> collection = calculationsService.CalculateTicketsPrices(tickets, hasChild, typeOfCard);

            Assert.That(expectedResult.Equals(collection[0].Price),
                   String.Format("Expected result was: {0}, but actual is: {1}", expectedResult, collection[0].Price));
        }

        [TearDown]
        public void TearDown()
        {
            tickets.Clear();
        }
    }
}

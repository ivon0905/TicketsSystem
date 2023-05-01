using TicketsSystem.Models;
using TicketsSystem.Services;
using TicketsSystem.ViewModels;

namespace TestTicketsSystem
{
    [TestFixture]
    public class IsRushTimeUnitTest
    {
        [Test]
        [TestCase("2023-4-15 6:0:0", false)]
        [TestCase("2023-4-15 7:30:0", true)]
        [TestCase("2023-4-15 14:0:0", false)]
        [TestCase("2023-4-15 15:0:0", false)]
        [TestCase("2023-4-15 16:0:0", true)]
        [TestCase("2023-4-15 16:30:0", true)]
        [TestCase("2023-4-15 20:0:0", false)]
        [TestCase("2023-4-15 22:0:0", false)]
        public void IsRushTimeTest(DateTime startTime, bool expectedResult)
        {
            CalculationsService service = new CalculationsService();
            bool result = service.IsRushTime(startTime);
            if (expectedResult)
            {
                Assert.IsTrue(result, "Expected result was true, but was: " + result);
            }
            else
            {
                Assert.IsFalse(result, "Expected result was false, but was: " + result);
            }
        }
    }
}

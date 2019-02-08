using NUnit.Framework;

namespace VideoStoreKata.Tests
{
    public class VideoStoreTests
    {
        private VideoStore _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new VideoStore();
        }
        
        [Test]
        public void GetStatement_WhenNoRentals_MustHaveNoRecords()
        {
            var result = _subject.GetStatement();

            Assert.That(result.Rentals.Count, Is.Zero);
        }
        
        [Test]
        public void GetStatement_WhenNoRentals_MustOweNothing()
        {
            var result = _subject.GetStatement();

            Assert.That(result.MoneyOwed, Is.Zero);
        }
        
        [Test]
        public void GetStatement_WhenNoRentals_MustHaveNoFrequentRenterPoints()
        {
            var result = _subject.GetStatement();

            Assert.That(result.FrequentRenterPoints, Is.Zero);
        }
        
        [Test]
        [TestCase("")]
        [TestCase("John")]
        [TestCase("Dave")]
        public void GetStatement_WhenNoRentals_MustIncludeCustomerName(string customerName)
        {
            _subject = new VideoStore(customerName);
            
            var result = _subject.GetStatement();

            Assert.That(result.CustomerName, Is.EqualTo(customerName));
        }
        
        [Test]
        public void GetStatement_When1RegularMovieRentedFor1Day_MustReturn1Rental()
        {
            _subject.RentRegularMovie("Jaws");
            
            var result = _subject.GetStatement();

            Assert.That(result.Rentals.Count, Is.EqualTo(1));
        }
        
        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 3.5)]
        [TestCase(4, 5)]
        [TestCase(5, 6.5)]
        public void GetStatement_When1RegularMovieRentedFor1Day_MustReturnRentalPrice(int numberOfDays, decimal expectedCost)
        {
            _subject.RentRegularMovie("Jaws", numberOfDays);
            
            var result = _subject.GetStatement();

            Assert.That(result.Rentals[0].Price, Is.EqualTo(expectedCost));
        }
        
        [Test]
        public void GetStatement_When2RegularMoviesRentedFor1Day_MustReturn2Rentals()
        {
            _subject.RentRegularMovie("Jaws", 1);
            _subject.RentRegularMovie("Honey I Shrunk The Kids", 1);
            
            var result = _subject.GetStatement();

            Assert.That(result.Rentals.Count, Is.EqualTo(2));
        }
        
        [Test]
        public void GetStatement_When2RegularMoviesRentedFor1And3Days_MustReturnCorrectPrice()
        {
            _subject.RentRegularMovie("Jaws", 1);
            _subject.RentRegularMovie("Honey I Shrunk The Kids", 3);
            
            var result = _subject.GetStatement();

            Assert.That(result.Rentals[0].Price, Is.EqualTo(2));
            Assert.That(result.Rentals[1].Price, Is.EqualTo(3.5m));
        }
    }
}
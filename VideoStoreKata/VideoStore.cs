using System.Collections.Generic;
using System.Linq;

namespace VideoStoreKata
{
    public class VideoStore
    {
        private readonly string _customer;
        private List<int> _numberOfDays;

        private const decimal RegularPriceForUpTo2Days = 2;
        private const decimal RegularPricePerDay = 1.5m;
        
        public VideoStore(string customer = null)
        {
            _customer = customer;
            _numberOfDays = new List<int>();
        }

        public void RentRegularMovie(string movie, int numberOfDays = 0)
        {
            _numberOfDays.Add(numberOfDays);
        }

        public Statement GetStatement()
        {
            var rentals = new List<Rental> { };

            if (_numberOfDays.Any())
            {
                for (int i = 0; i < _numberOfDays.Count; i++)
                {
                    rentals.Add(new Rental
                    {
                        Price = CalculateRegularPrice(_numberOfDays[i])
                    });
                }
            }

            return new Statement
            {
                Rentals = rentals,
                CustomerName = _customer
            };

        }
        
        private decimal CalculateRegularPrice(int numberOfDays)
        {
            var price = 0m;
            
            if (numberOfDays >= 1)
            {
                price = RegularPriceForUpTo2Days;
            }
            
            if (numberOfDays > 2)
            {
                price += (numberOfDays - 2) * RegularPricePerDay;
            }

            return price;
        }
    }

    public class Statement
    {
        public List<Rental> Rentals { get; set; }

        public decimal MoneyOwed { get; set; }

        public int FrequentRenterPoints { get; set; }

        public string CustomerName { get; set; }
    }

    public class Rental
    {
        public decimal Price { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    //создайте класс AccountingModel здесь
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double total;
        private double discount;

        public double Price
        {
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
                Notify(nameof(Total));
                //Console.WriteLine("Price " + price);
                //Console.WriteLine("Total " + Total);
            }
            get
            {
                return price;
            }
        }

        public int NightsCount 
        {
            set
            {
                if (value <= 0)
                    throw new ArgumentException();
                nightsCount = value;
                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
            get { return nightsCount; }
        }

        public double Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                if (Discount > 100)
                    throw new ArgumentException();
                Notify(nameof(Discount));
                Notify(nameof(Total));
            } 
        }

        public double Total
        {
            get
            {
                if (Price * NightsCount * (1 - Discount / 100) < 0)
                    throw new ArgumentException();
                return Price * NightsCount * (1 - Discount / 100);
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                total = value;
                Notify(nameof(Total));
                discount = (1 - total / (Price * NightsCount)) * 100;
                Notify(nameof(Discount));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleParking
{
    public class Parking
    {
        private object locker = new object();

        /// <summary>
        /// Instance of singleton object
        /// </summary>
        private static Lazy<Parking> parking { get; set; } = new Lazy<Parking>(() => new Parking());

        /// <summary>
        /// Property for get singleton object
        /// </summary>
        public static Parking Instance
        {
            get { return parking.Value; }        
        }

        private double balance=0;

        private Parking() { }

        /// <summary>
        /// Get parking settings 
        /// </summary>
        public Settings Settings{ get { return Settings.Instance; } }

        /// <summary>
        /// Get list of cars which staying in parking
        /// </summary>
        public List<Car> Cars { get; } = new List<Car>();

        /// <summary>
        /// Get list of transactions
        /// </summary>
        public List<Transaction> Transactions { get; } = new List<Transaction>();

        /// <summary>
        /// Show parking income
        /// </summary>
        public double Balance
        {
            get
            {
                lock (locker)
                {
                    return parking.Value.balance;
                }
            }
            set
            {
                lock (locker)
                {
                    if (value > 0)
                        parking.Value.balance = value;
                    else
                        throw new Exception("Баланс не может быть меньше нуля!");
                }
            }
        }

        public void ShowAutosInParking()
        {
            Console.WriteLine("\nНомер авто - тип авто");
            Cars.OrderBy(car=>car.CarType.ToString()).ThenBy(car=>car.CarNumber).ToList<Car>().ForEach((car) => {
                Console.WriteLine($"{car.CarNumber} - {car.CarType}");
            });
        }

        public Car FindAutoByNumber(string carNumber)
        {
            return Cars.Find((car) => { return carNumber == car.CarNumber; });
        }

        public int CountFreePlaces { get { return Settings.ParkingSpace - Cars.Count; } }

        public void ShowTransactions()
        {
            Console.WriteLine("\nДата - Номер авто - Сумма");
            Transactions.OrderBy(tr=>tr.CarNumber).Where(tr=>tr.DateTime.AddMinutes(1)>=DateTime.Now).ToList<Transaction>().ForEach((transaction) => {
                Console.WriteLine($"{transaction.DateTime.ToString()} - {transaction.CarNumber} - {transaction.WriteOff}");
            });
        }

    }
}

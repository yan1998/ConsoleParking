using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Timers;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace ConsoleParking
{
    public class Parking:IDisposable
    {
        private class TransactionLog
        {
            public string Date { get; set; }
            public double Sum { get; set; }
        }

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

        private bool disposed = false;
        private Timer timer = new Timer(60000);
        private double balance = 0;

        private Parking()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            List<TransactionLog> transactions = new List<TransactionLog>();
            if (File.Exists("Transaction.log"))
            {
                transactions = JsonConvert.DeserializeObject<List<TransactionLog>>(File.ReadAllText("Transaction.log"));
                File.Delete("Transaction.log");
            }
            double sum = Transactions.Where(tra => tra.DateTime.AddMinutes(1) >= DateTime.Now).Sum(tra => tra.WriteOff);
            transactions.Add(new TransactionLog() { Sum = sum, Date = DateTime.Now.ToString() });
            string newJson = JsonConvert.SerializeObject(transactions);

            File.WriteAllText("Transaction.log", newJson);
        }

        /// <summary>
        /// Get parking settings 
        /// </summary>
        public Settings Settings { get { return Settings.Instance; } }

        /// <summary>
        /// Get list of cars which staying in parking
        /// </summary>
        public List<Car> Cars { get; } = new List<Car>();

        /// <summary>
        /// Get list of transactions
        /// </summary>
        public ConcurrentBag<Transaction> Transactions { get; } = new ConcurrentBag<Transaction>();

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
            Cars.OrderBy(car => car.CarType.ToString()).ThenBy(car => car.CarNumber).ToList<Car>().ForEach((car) =>
            {
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
            Transactions.OrderBy(tr=>tr.DateTime).Where(tr => tr.DateTime.AddMinutes(1) >= DateTime.Now).ToList<Transaction>().ForEach((transaction) =>
            {
                Console.WriteLine($"{transaction.DateTime.ToString()} - {transaction.CarNumber} - {transaction.WriteOff}");
            });
        }

        public void ShowLogFile()
        {
            if (File.Exists("Transaction.log"))
            {
                List<TransactionLog> transactions= JsonConvert.DeserializeObject<List<TransactionLog>>(File.ReadAllText("Transaction.log"));
                Console.WriteLine("\nДата - Сумма");
                transactions.ForEach((tr) => { Console.WriteLine($"{tr.Date.ToString()} - {tr.Sum}"); });
            }
            else
                Console.WriteLine("\nФайл Transaction.log ещё не сгенерировался!");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                timer.Dispose();

            disposed = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleParking
{
    public class Settings
    {

        /// <summary>
        /// Instance of singleton object
        /// </summary>
        private static readonly Lazy<Settings> settings = new Lazy<Settings>(() => new Settings());

        /// <summary>
        /// Property for get singleton object
        /// </summary>
        public static Settings Instance { get { return settings.Value; } }

        private int timeout=3;
        private double fine=1.5;
        private int parkingSpace=10;
        private Dictionary<CarType, float> dictionary=new Dictionary<CarType, float>()
        {
            [CarType.Truck] = 5,
            [CarType.Passenger] = 3,
            [CarType.Bus] = 2,
            [CarType.Motorcycle] = 1
        };

        private Settings(){}
        
        /// <summary>
        /// Time for payment 
        /// </summary>
        public int Timeout
        {
            get { return settings.Value.timeout; }
            set
            {
                if (value > 0)
                    settings.Value.Timeout = value;
                else
                    throw new Exception("Время оплаты парковки должно быть ненулевым!");
            }
        }
        
        /// <summary>
        /// Prices which depend on car type
        /// </summary>
        public Dictionary<CarType, float> Dictionary
        {
            get { return settings.Value.dictionary; }
        }

        /// <summary>
        /// Quantity all places for cars in the parking
        /// </summary>
        public int ParkingSpace
        {
            get { return settings.Value.parkingSpace; }
            set
            {
                if (value > 0)
                    settings.Value.parkingSpace = value;
                else
                    throw new Exception("Количество парковочных мест должно быть ненулевым!");
            }
        }

        /// <summary>
        /// Coefficient of fine
        /// </summary>
        public double Fine
        {
            get { return settings.Value.fine; }
            set
            {
                if (value > 0)
                    settings.Value.fine = value;
                else
                    throw new Exception("Штраф должен быть ненулевым!");
            }
        }
        
    }
}

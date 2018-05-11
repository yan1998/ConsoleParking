using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleParking
{
    public class Car
    {
        private string carNumber;
        private double balance;

        public Car(string carNumber, double balance, CarType carType)
        {
            CarNumber = carNumber;
            Balance = balance;
            CarType = carType;
        }

        /// <summary>
        /// Properties for get car identifier
        /// </summary>
        public string CarNumber
        {
            get { return carNumber; }
            private set
            {
                if (value.Length != 0)
                    carNumber = value;
                else
                    throw new Exception("Номер авто должен быть указан!");
            }
        }

        /// <summary>
        /// Properties for car balance
        /// </summary>
        public double Balance
        {
            get { return balance; }
            private set
            {
                if (value > 0)
                    balance = value;
                else
                    throw new Exception("Баланс не может быть равным нулю");
            }
        }

        /// <summary>
        /// Properties for get car type
        /// </summary>
        public CarType CarType { get; private set; }

        public void AddToParking(Parking parking)
        {
            if (parking.Cars.Count == parking.Settings.ParkingSpace)
            {
                Console.WriteLine("Вы не можете припоркавать автомобиль! Все места на паркове заняты!");
                return;
            }
            else if (parking.Settings.Dictionary[CarType] > Balance)
            {
                Console.WriteLine("У вас не хватает денег для парковки!");
                return;
            }
            else
            {
                parking.Cars.Add(this);
                Console.WriteLine("Автомобиль был успешно добавлен в паркинг!");
            }
        }
    }
}

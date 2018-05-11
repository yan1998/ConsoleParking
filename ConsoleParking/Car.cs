using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleParking
{
    public class Car
    {
        private string carNumber;

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
        public double Balance { get; set; }

        /// <summary>
        /// Properties for get car type
        /// </summary>
        public CarType CarType { get; private set; }

        public void AddToParking(Parking parking)
        {
            if (parking.Settings.Dictionary[CarType] > Balance)
                Console.WriteLine("У вас не хватает денег для парковки!");
            else
            {
                parking.Cars.Add(this);
                Console.WriteLine("Автомобиль был успешно добавлен в паркинг!");
            }
        }

        public void RemoveFromParking(Parking parking)
        {
            if (Balance < 0)
                Console.WriteLine("Вы не можете покинуть паркинг! У вас есть долги!");
            else
            {
                parking.Cars.Remove(this);
                Console.WriteLine("Автомобиль успешно покинул паркинг!");
            }
        }
    }
}

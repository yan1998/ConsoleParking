﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleParking
{
    public class Parking
    {
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
            get { return parking.Value.balance; }
            set
            {
                if (value > 0)
                    parking.Value.balance = value;
                else
                    throw new Exception("Баланс не может быть меньше нуля!");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleParking
{
    public class Transaction
    {
        private string carNumber;
        private double writeOff;

        public Transaction(DateTime dateTime, string carNumber, double writeOff)
        {
            DateTime = dateTime;
            CarNumber = carNumber;
            WriteOff = writeOff;
        }

        /// <summary>
        /// Properties which contain a transaction date and time 
        /// </summary>
        public DateTime DateTime{ get; private set; }

        /// <summary>
        /// Properties for set car identifier
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
        /// Properties which contain a write-off money
        /// </summary>
        public double WriteOff{
            get { return writeOff; }
            private set
            {
                if (value >= 0)
                    writeOff = value;
                else
                    throw new Exception("Списанные средства не могут быть отрицательными!");
            }
        }
    }
}

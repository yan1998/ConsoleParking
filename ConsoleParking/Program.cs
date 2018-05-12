using System;
using System.IO;

namespace ConsoleParking
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("Transaction.log"))
                File.Delete("Transaction.log");
            Console.WriteLine("Вас приветствует Горшков Ян!)");
            Menu.MainMenu();
            Console.WriteLine("\nСпасибо за использование!");
            Parking.Instance.Dispose();
            Console.ReadKey();
        }
    }
}

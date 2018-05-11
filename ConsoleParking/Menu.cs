using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleParking
{
    public static class Menu
    {
        static Parking parking = Parking.Instance;

        public static void MainMenu()
        {
            ShowMainMenu();
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("\nОжидаю нажатия...(Для повтороного открытия меню нажмите 7)");
                key = Console.ReadKey();
                if (!Char.IsDigit(key.KeyChar))
                    Console.WriteLine("\nНеобходимо нажимать только цифры!");
                else
                    switch (key.KeyChar)
                    {
                        case '1': AddCarToParking(); break;
                        case '2': DeleteCarFromParking(); break;
                        case '3': ReplenishmentAutoBalance(); break;
                        case '4': ShowTransactionStory(); break;
                        case '5': ShowParkingIncome(); break;
                        case '6': ShowFreePlaces(); break;
                        case '7': ShowMainMenu(); break;
                    }
            } while (key.KeyChar != '0');
        }


        private static void ShowMainMenu()
        {
            Console.Write("\nМеню:\n1) Добавить авто на парковку\n" +
                "2) Удалить авто с парковки\n" +
                "3) Пополнить баланс авто\n" +
                "4) Просмотр истории транзакций за последнюю минуту\n" +
                "5) Просмотр общего дохода парковки\n" +
                "6) Просмотр свободных мест\n" +
                "7) Вывести меню\n" +
                "0) Выход из программы\n");
        }

        private static void AddCarToParking()
        {
            if (parking.CountFreePlaces == 0)
            {
                Console.WriteLine("Вы не можете припоркавать автомобиль! Все места на паркове заняты!");
                return;
            }
            try
            {
                Console.Write("\nВведите номер автомобиля: ");
                string carNumber = Console.ReadLine();
                Console.Write("Введите баланс автомобиля: ");
                double carBalance = double.Parse(Console.ReadLine());
                int carType;
                do
                {
                    Console.Write("Выберите тип автомобиля:\n\t1)Passenger\n\t2)Truck\n\t3)Bus\n\t4)Motorcycle\nВведите нужное число: ");
                    carType = int.Parse(Console.ReadLine());
                } while (carType < 0 || carType > 4);
                Car car = new Car(carNumber, carBalance, (CarType)carType-1);
                car.AddToParking(parking);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ввод некорректный! Баланс и тип авто необходимо вводить числами!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DeleteCarFromParking()
        {
            if (parking.Cars.Count == 0)
                Console.WriteLine("\nАвтомобилей в паркинге нет!");
            else
            {
                Car car=ChoiceCarByNumber();
                if (car != null)
                    car.RemoveFromParking(parking);
            }
        }

        private static void ReplenishmentAutoBalance()
        {
            if (parking.Cars.Count == 0)
                Console.WriteLine("\nАвтомобилей в паркинге нет!");
            else
            {
                try
                {
                    Car car = ChoiceCarByNumber();
                    if (car != null)
                    {
                        Console.WriteLine($"Текущий баланс {car.CarNumber} = {car.Balance}.");
                        Console.Write("Введите сумму для пополнения баланса:");
                        double charge = double.Parse(Console.ReadLine());
                        if (charge < 0)
                            Console.WriteLine("Вы не можете уменьшить баланс!");
                        else
                        {
                            car.Balance += charge;
                            Console.WriteLine($"Баланс после пополнения = {car.Balance}.");
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Некорректный ввод! Ожидалось число!");
                }
            }
        }

        private static Car ChoiceCarByNumber()
        {
            Console.WriteLine();
            parking.ShowAutosInParking();
            Console.Write("Введите номер авто:");
            string number = Console.ReadLine();
            Car car= parking.FindAutoByNumber(number);
            if(car == null)
                Console.WriteLine("Авто не было найдено!");
            return car;
        }

        private static void ShowTransactionStory()
        {
            parking.ShowTransactions();
        }

        private static void ShowParkingIncome()
        {
            Console.WriteLine($"\nДоход паркига составляет: {parking.Balance}");
        }

        private static void ShowFreePlaces()
        {
            Console.WriteLine($"\nКоличество свободных мест: {parking.CountFreePlaces}");
        }
    }
}

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
                Console.WriteLine("\nОжидаю нажатия...(Для повтороного открытия меню нажмите 7)");
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
                "0) Выход из программы\nНажмите нужную цифру на клавиатуре: ");
        }

        private static void AddCarToParking()
        {
            try
            {
                Console.Write("\nВведите номер автомобиля: ");
                string carNumber = Console.ReadLine();
                Console.Write("Введите баланс автомобиля: ");
                double carBalance = double.Parse(Console.ReadLine());
                int carType;
                do
                {
                    Console.Write("Выберите тип автомобиля:\n\t1)Легковой\n\t2)Грузовой\n\t3)Автобус\n\t4)Мотоцикл\nВведите нужное число: ");
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
            throw new NotImplementedException();
        }

        private static void ShowTransactionStory()
        {
            throw new NotImplementedException();
        }

        private static void ReplenishmentAutoBalance()
        {
            throw new NotImplementedException();
        }

        private static void ShowParkingIncome()
        {
            throw new NotImplementedException();
        }

        private static void ShowFreePlaces()
        {
            throw new NotImplementedException();
        }
    }
}

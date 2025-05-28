using System.Collections;
using System.Text.Json;

namespace AutoServiceApp
{
    public class Service
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Service(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    public class Order
    {
        public ArrayList SelectedServices { get; set; } = new ArrayList();

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (Service s in SelectedServices)
                {
                    total += s.Price;
                }
                return total;
            }
        }
    }

    public static class Logger
    {
        private static readonly string LogFilePath = "logs.txt";

        public static void LogError(Exception ex)
        {
            File.AppendAllText(LogFilePath, $"{DateTime.Now}: {ex}\n");
        }
    }

    public static class FileManager
    {
        public static void SaveOrderJson(Order order, string filePath)
        {
            try
            {
                var json = JsonSerializer.Serialize(order);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        public static Order LoadOrderJson(string filePath)
        {
            try
            {
                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Order>(json);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return new Order();
            }
        }
    }

    public static class ServiceMenu
    {
        public static ArrayList EngineServices { get; private set; }
        public static ArrayList MaintenanceServices { get; private set; }
        public static ArrayList TransmishionServices { get; private set; }

        // … (категорії послуг залишаються як є)

        public static void ShowMenu(Order order)
        {
            string[] options = { "1.Двигун", "2.Планове ТО", "3.Трансмісія", "4.Оформити замовлення", "_Вихід_" };
            int selected = 0;

            while (true)
            {
                Console.Clear();
                CenterWriteLine("АВТОСЕРВІС \"АвтоМайстер\"", ConsoleColor.Green);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                CenterWriteLine($"Загальна сума замовлення: {order.TotalPrice} грн");
                Console.ResetColor();

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    CenterWriteLine(options[i]);
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow) selected = (selected == 0) ? options.Length - 1 : selected - 1;
                else if (key.Key == ConsoleKey.DownArrow) selected = (selected + 1) % options.Length;
                else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                {
                    switch (selected)
                    {
                        case 0: ShowSubMenu("Двигун", EngineServices, order); break;
                        case 1: ShowSubMenu("Планове ТО", MaintenanceServices, order); break;
                        case 2: ShowSubMenu("Трансмісія", TransmishionServices, order); break;
                        case 3: ConfirmOrder(order); break;
                        case 4: return;
                    }
                }
            }
        }

        private static void ShowSubMenu(string category, ArrayList services, Order order)
        {
            int selected = 0;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                CenterWriteLine($"Категорія: {category}");
                Console.ResetColor();
                Console.WriteLine($"Поточна сума: {order.TotalPrice} грн\n");

                for (int i = 0; i < services.Count; i++)
                {
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Service s = (Service)services[i];
                    Console.WriteLine($" {s.Name} - {s.Price} грн");
                    Console.ResetColor();
                }

                Console.WriteLine("\n ↑ ↓ для навігації, Enter/Space — вибрати, Esc — назад");

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow) selected = (selected == 0) ? services.Count - 1 : selected - 1;
                else if (key.Key == ConsoleKey.DownArrow) selected = (selected + 1) % services.Count;
                else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                {
                    order.SelectedServices.Add(services[selected]);
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        private static void ConfirmOrder(Order order)
        {
            while (true)
            {
                Console.Clear();
                CenterWriteLine("🧾 Поточне замовлення", ConsoleColor.Cyan);
                if (order.SelectedServices.Count == 0)
                {
                    Console.WriteLine("Поки що не додано жодної послуги.");
                    Console.ReadKey();
                    return;
                }

                for (int i = 0; i < order.SelectedServices.Count; i++)
                {
                    Service s = (Service)order.SelectedServices[i];
                    Console.WriteLine($"{i + 1}. {s.Name} - {s.Price} грн");
                }

                Console.WriteLine($"\nЗагальна вартість: {order.TotalPrice} грн");
                Console.Write("\nВведіть номер для видалення послуги (або Enter для продовження): ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) break;

                if (int.TryParse(input, out int index) && index >= 1 && index <= order.SelectedServices.Count)
                {
                    order.SelectedServices.RemoveAt(index - 1);
                    Console.WriteLine("✅ Видалено.");
                }
                else
                {
                    Console.WriteLine("❌ Невірний номер.");
                }

                Console.WriteLine("Натисніть будь-яку клавішу...");
                Console.ReadKey();
            }

            string phone;
            while (true)
            {
                Console.Clear();
                Console.Write("Введіть номер телефону (+380XXXXXXXXX): ");
                phone = Console.ReadLine();

                try
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\+380\d{9}$"))
                        throw new FormatException("Невірний формат номера телефону.");

                    break;
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ " + ex.Message);
                    Console.ResetColor();
                    Console.WriteLine("Спробуйте ще раз...");
                    Console.ReadKey();
                }
            }

            FileManager.SaveOrderJson(order, "order.json");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Замовлення збережено. Ми зателефонуємо на номер: {phone}");
            Console.WriteLine($"Загальна сума: {order.TotalPrice} грн");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static void CenterWriteLine(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            int width = Console.WindowWidth;
            int padding = (width - text.Length) / 2;
            Console.WriteLine($"{new string(' ', Math.Max(padding, 0))}{text}");
            Console.ResetColor();
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Order currentOrder = new();
            try
            {
                ServiceMenu.ShowMenu(currentOrder);
                FileManager.SaveOrderJson(currentOrder, "order.json");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nЗагальна вартість: {currentOrder.TotalPrice} грн");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Сталася помилка. Подробиці записані у файл логів.");
                Console.ResetColor();
            }
        }
    }
}
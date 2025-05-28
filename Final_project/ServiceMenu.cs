using System.Collections;

namespace AutoServiceApp
{
    public static class ServiceMenu
    {

        public static ArrayList EngineServices = new ArrayList
        {
            new Service("1.Заміна прокладки ГБЦ", 5000),
            new Service("2.Заміна клапанів", 6000),
            new Service("3.Заміна сальників", 4000),
            new Service("4.Заміна поршневих кілець", 9000),
            new Service("5.Капітальний ремонт двигуна", 28000)
        };

        public static ArrayList MaintenanceServices = new ArrayList
        {
            new Service("1.Заміна масла двигуна", 400),
            new Service("2.Заміна передніх тормозних колодок", 600),
            new Service("3.Заміна задніх тормозних колодок", 600),
            new Service("4.Заміна колодок ручного гальма", 1000),
            new Service("5.Заміна ГРМ", 3500),
            new Service("6.Заміна щеплення", 4000),
            new Service("7.Заміна мастила КПП", 500),
            new Service("8.Заміна мастила АКПП", 800)
        };

        public static ArrayList TransmishionServices = new ArrayList
        {
            new Service("1.Заміна приводного валу", 600),
            new Service("2.Заміна мастила в Редукторі", 300),
            new Service("3.Заміна хрестовини карданного валу", 500),
            new Service("4.Заміна сальників напіввісей", 800),
            new Service("5.Зняття встановлення МКПП", 4000),
            new Service("6.Зняття встановлення АКПП", 6500)
        };

        public static void ShowMenu(Order order)
        {
            string[] options = { "1.Двигун", "2.Планове ТО", "3.Трансмісія", "4.Оформити замовлення", "_Вихід_" };
            int selected = 0;

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("==========================================");
                Console.WriteLine("        АВТОСЕРВІС \"АвтоМайстер\"");
                Console.WriteLine("==========================================\n");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"   Загальна сума замовлення: {order.TotalPrice} грн\n");
                Console.ResetColor();



                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine($"   {options[i]}");
                    Console.ResetColor();
                }
                Console.WriteLine("\n   ↑ ↓ для навігації, Enter/Space — вибрати");

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    selected = (selected == 0) ? options.Length - 1 : selected - 1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selected = (selected + 1) % options.Length;
                }
                else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                {
                    switch (selected)
                    {
                        case 0:
                            ShowSubMenu("Двигун", EngineServices, order);
                            break;
                        case 1:
                            ShowSubMenu("Планове ТО", MaintenanceServices, order);
                            break;
                        case 2:
                            ShowSubMenu("Трансмісія", TransmishionServices, order);
                            break;
                        case 3:
                            ConfirmOrder(order);
                            break;
                        case 4:
                            return;
                    }

                }
            }
        }

        private static void ShowSubMenu(string v, object transmishionServicess, Order order)
        {
            throw new NotImplementedException();
        }

        private static void ShowSubMenu(string category, ArrayList services, Order order)
        {
            int selected = 0;
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"========== Категорія: {category} ==========");
                Console.ResetColor();
                Console.WriteLine($"   Поточна сума: {order.TotalPrice} грн\n");

                for (int i = 0; i < services.Count; i++)
                {
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Service s = (Service)services[i];
                    Console.WriteLine($"   {s.Name} - {s.Price} грн");
                    Console.ResetColor();
                }
                Console.WriteLine("\n   ↑ ↓ для навігації, Enter/Space — вибрати, Esc — назад");

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    selected = (selected == 0) ? services.Count - 1 : selected - 1;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selected = (selected + 1) % services.Count;
                }
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
                Console.WriteLine("🧾 Поточне замовлення", ConsoleColor.Cyan);
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
    }
}

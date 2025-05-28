namespace AutoServiceApp
{
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

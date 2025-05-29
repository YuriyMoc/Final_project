namespace AutoServiceApp
{
    public  static class Logger // Клас для запису помилок у фаул "logs.txt" та відстежування багів і проблем
    {

        private static readonly string LogFilePath = "logs.txt";

        public static void LogError(Exception ex)
        {
            File.AppendAllText(LogFilePath, $"{DateTime.Now}: {ex}\n");
        }
    }
}

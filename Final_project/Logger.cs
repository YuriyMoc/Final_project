namespace AutoServiceApp
{
    public  static class Logger
    {

        private static readonly string LogFilePath = "logs.txt";

        public static void LogError(Exception ex)
        {
            File.AppendAllText(LogFilePath, $"{DateTime.Now}: {ex}\n");
        }
    }
}

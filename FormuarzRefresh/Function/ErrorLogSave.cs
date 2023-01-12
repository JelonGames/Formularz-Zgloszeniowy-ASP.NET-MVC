namespace FormuarzRefresh.Function
{
    public static class ErrorLogSave
    {
        private static string directory = @"c:\inetpub\logs\Formularz";
        private static string file = @"\Logs.txt";

        public static void Save(string message, string errorName)
        {
            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(directory + file))
            {
                FileStream fs = File.Create(directory + file);
                fs.Close();
            }

            StreamWriter sw = new StreamWriter(directory + file, true, System.Text.Encoding.UTF8);
            sw.WriteLine($"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}\t\t{errorName}\t{message}");
            sw.Close();
        }
    }
}

using System;
using System.IO;

namespace Services
{
    public class PersistenceManager
    {
        public string FullFilePath { get; private set; }
        public PersistenceManager(string fullFilePath)
        {
            FullFilePath = fullFilePath;
        }

        public void WriteToFile(string data, bool append)
        {
            Console.WriteLine($"{data}");

            using (StreamWriter outputFile = CreateStreamWriter(FullFilePath, append))
            {
                outputFile.WriteLine($"{data}");
            }
        }
        private StreamWriter CreateStreamWriter(string filePath, bool AppendText)
        {
            if (AppendText)
            {
                return File.AppendText(filePath);
            }
            else
            {
                return new StreamWriter(filePath);
            }
        }
    }
}

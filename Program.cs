using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTerminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            string url = "https://query1.finance.yahoo.com/v7/finance/download/%5EIXIC?period1=1633692001&period2=1665228001&interval=1d&events=history&includeAdjustedClose=";

            // download the file
            string fileName = "stock.csv";
            using (var client = new System.Net.WebClient())
            {
                client.DownloadFile(url, fileName);
            }

            // read the file
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // parse the 4th values seperated with , to list of ints
            List<int> values = new List<int>();
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                values.Add((int)double.Parse(parts[4]));
            }

            // draw the graph inside the limited height and width of the console window
            int max = values.Max();
            int min = values.Min();
            int range = max - min;
            int step = range / height;
            int offset = min;

            for (int i = 0; i < width; i++)
            {
                int value = values[i];
                int y = (value - offset) / step;
                Console.SetCursorPosition(i, height - y);
                Console.Write("*");
            }

            Console.ReadLine();
        }
    }
}

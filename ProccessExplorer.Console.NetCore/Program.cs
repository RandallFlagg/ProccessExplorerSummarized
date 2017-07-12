using ConsoleTables;
using ProccessExplorer.Core.NetCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProccessExplorerSummarized.Interface
{
    class Program
    {
        private static volatile bool exit = false;

        private static void Init() {
            Console.WindowWidth = 120;
        }

        private static void Main(string[] args)
        {
            IPESCoreConfiguration config = new PESCoreConfiguration(500000000);
            PESCore core = new PESCore().Init(config);
            var exitKey = ConsoleKey.X;

            Task.Run(() =>
            {
                ConsoleKeyInfo key;
                do
                {
                    key = Console.ReadKey();
                    if (key.Key == exitKey)
                    {
                        exit = true;
                    }
                } while (key.Key != exitKey);
            });

            Console.WriteLine("Press {0} to exit", exitKey);

            Init();

            var interval = 1000;
            do
            {
                var data = core.GetData();
                Console.Clear();
                ConsoleTable.From(data).Write(Format.Alternative);
                Console.WriteLine("Count: {0}", data.Count());
                ConsoleTable.From(data).Write(Format.MarkDown);
                Console.WriteLine("Count: {0}", data.Count());
                Thread.Sleep(interval);
                
            }
            while (!exit);
        }
    }
}

using DBM;
using InvMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvMan2
{
    class Program
    {
        static void Main(string[] args)
        {
            string INVMAN2_DIR = Environment.CurrentDirectory.ToString();

            string TickersPath = INVMAN2_DIR+@"\source\tickers.json";
            string errorPath = INVMAN2_DIR+@"\errors.txt";
            
            AUTOGRABBER ag = new AUTOGRABBER(TickersPath,errorPath);

            bool runningProgram = true;

            Console.WriteLine("Hi welcome in InvMan System.");
            while (runningProgram)
            {
                string[] input_command = Console.ReadLine().Split(' ');
                string command=input_command[0];
                switch (command.ToUpper())
                {
                    case "STOCKSDETAILS":
                        ag.updateStockDetails();
                        break;

                    case "AMORTIZATION":
                        ag.updateCashFlowPosition(command.ToUpper());
                        break;

                    case "TOTALASSETS":
                        ag.updateBalanceSheetPosition(command.ToUpper());
                        break;

                    case "EBIT":
                        ag.updateIncomeOutcomePosition(command.ToUpper());
                        break;

                    case "STOCKSQUOTES":
                        ag.updateStocksQuotes(input_command[1].ToUpper(), input_command[2].ToUpper());
                        break;

                   

                    case "EXIT":
                        runningProgram = false;
                        break;
                }
            }
        }

    }
}

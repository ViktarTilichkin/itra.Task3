using Game;
using System.Security.Cryptography;

namespace Game
{
    class Program
    {
        static bool CheckArguments(string[] arguments)
        {
            if (arguments.Length < 3 || arguments.Length % 2 == 0 || arguments.Length != arguments.Distinct().Count())
            {
                Console.WriteLine("Invalid arguments! arguments > 3, not an even number and not matching in value");
                return false;
            }

            return true;
        }


        static void Main(string[] arguments)
        {
            if (!CheckArguments(arguments))
            {
                return;
            }


            Encryption encryptionKey = new Encryption();
            HelpTable tableRules = new HelpTable(arguments);
            Rules referee = new Rules(arguments.Length);

            bool endGame = false;

            while (!endGame)
            {
                string key = encryptionKey.GenerateKey();
                Random rnd = new();
                int pcChoose = rnd.Next(0, arguments.Length);
                string hmac = encryptionKey.GenerateHMAC(key, arguments[pcChoose]);

                Console.WriteLine("HMAC: " + hmac);

                Console.WriteLine("Throws:");
                for (int i = 0; i < arguments.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {arguments[i]}");
                }
                Console.WriteLine("0 - exit");
                Console.WriteLine("? - help");

                Console.Write("Choose: ");
                var choose = Console.ReadLine();

                if (choose == "?")
                {
                    tableRules.Print();
                    Console.Write("\n");
                    continue;
                }

                if (choose == "0")
                {
                    endGame = true;
                    continue;
                }

                int numChoose = 0;

                if (!int.TryParse(choose, out numChoose) || numChoose > arguments.Length || numChoose <= 0)
                {
                    Console.Write("\n");
                    continue;
                }

                Console.WriteLine("Your move: " + arguments[numChoose - 1]);
                Console.WriteLine("Computer move: " + arguments[pcChoose]);

                switch (referee.Definition(pcChoose, numChoose - 1))
                {
                    case ResultList.win:
                        Console.WriteLine("You won!");
                        break;

                    case ResultList.lose:
                        Console.WriteLine("You lost!");
                        break;

                    default:
                        Console.WriteLine("Draw!");
                        break;
                }

                Console.WriteLine("HMAC key: " + key);
                Console.Write("\n");
            }
        }
    }
}

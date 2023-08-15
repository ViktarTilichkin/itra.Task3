using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace Game
{
    public class HelpTable
    {
        string[] Argumenst;
        public HelpTable(string[] arguments)
        {
            Argumenst = arguments;
        }

        public void Print()
        {
            var cell = Argumenst.Prepend("v PC \\ User >");
            var table = new ConsoleTable(cell.ToArray());

            var rules = new Rules(Argumenst.Length);

            for (int i = 0; i < Argumenst.Length; i++)
            {
                var line = new string[Argumenst.Length + 1];
                line[0] = Argumenst[i];

                for (int j = 0; j < Argumenst.Length; j++)
                {
                    line[j + 1] = Enum.GetName(typeof(ResultList), rules.Definition(j, i));
                }

                table.AddRow(line.ToArray());
            }
            table.Write(Format.Alternative);
        }
    }
}

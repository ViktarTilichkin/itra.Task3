using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Rules
    {
        int AmountThrows;

        public Rules(int amountThrows)
        {
            AmountThrows = amountThrows;
        }

        public ResultList Definition(int first, int second)
        {
            if (first == second)
            {
                return ResultList.draw;
            }

            if ((second > first && second - first <= AmountThrows / 2) || (second < first && first - second > AmountThrows / 2))
            {
                return ResultList.win;
            }

            return ResultList.lose;
        }
    }
}

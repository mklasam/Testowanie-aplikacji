using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLib
{
    public enum GAME_STATUS
    {
        IN_PROGRESS,
        PLAYER_ONE_WON,
        PLAYER_TWO_WON,
        TIE
    }

    public enum WIN_CONDITION
    {
        NONE,
        ROW,
        COLUMN,
        MAIN_DIAGONAL,
        OPP_DIAGONAL
    }

    public class GameStatus
    {
        public GAME_STATUS GameProgress
        {
            get;
            set;
        }

        public WIN_CONDITION WinCondition
        {
            get;
            set;
        }

        public int WinRowOrColumn
        {
            get;
            set;
        }
    }
}

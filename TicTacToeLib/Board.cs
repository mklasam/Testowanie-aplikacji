using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLib
{
    public class Board
    {
        private Field[,] _fields;

        public event EventHandler<GameStatus> GameEnd;

        public Board(Field[,] fields)
        {
            if (fields == null)
            {
                throw new ArgumentNullException("fields");
            }

            if (fields.GetLength(0) != fields.GetLength(1))
            {
                throw new ArgumentException("fields must be square matrix");
            }

            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    if (fields[i, j] == null)
                    {
                        throw new ArgumentNullException("no field can be null");
                    }
                }
            }

            _fields = fields;

            AddFieldEventListeners();
        }

        public Field[,] Fields
        {
            get
            {
                return _fields;
            }
        }

        private void AddFieldEventListeners()
        {
            for (int i = 0; i < _fields.GetLength(0); i++)
            {
                for (int j = 0; j < _fields.GetLength(1); j++)
                {
                    _fields[i, j].FieldStatusChanged += Board_FieldStatusChanged;
                }
            }
        }

        private void Board_FieldStatusChanged(object sender, EventArgs e)
        {
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            GameStatus gameStatus = new GameStatus() { GameProgress = GAME_STATUS.IN_PROGRESS };

            gameStatus = AllFieldsInRowWinCondition(gameStatus);
            if (gameStatus.GameProgress != GAME_STATUS.IN_PROGRESS)
            {
                OnGameEnd(gameStatus);
                return;
            }

            gameStatus = AllFieldsInColumnWinCondition(gameStatus);
            if (gameStatus.GameProgress != GAME_STATUS.IN_PROGRESS)
            {
                OnGameEnd(gameStatus);
                return;
            }

            gameStatus = AllFieldsInMainDiagonalWinCondition(gameStatus);
            if (gameStatus.GameProgress != GAME_STATUS.IN_PROGRESS)
            {
                OnGameEnd(gameStatus);
                return;
            }

            gameStatus = AllFieldsInOppositeDiagonalWinCondition(gameStatus);
            if (gameStatus.GameProgress != GAME_STATUS.IN_PROGRESS)
            {
                OnGameEnd(gameStatus);
                return;
            }

            gameStatus = AllFieldsOcupiedWinCondition(gameStatus);
            if (gameStatus.GameProgress != GAME_STATUS.IN_PROGRESS)
            {
                OnGameEnd(gameStatus);
            }
        }

        protected virtual void OnGameEnd(GameStatus status)
        {
            status.Raise(this, ref GameEnd);
        }

        // Handles cases
        // [X] [ ] [ ]      [ ] [X] [ ]     [ ] [ ] [X]
        // [X] [ ] [ ]      [ ] [X] [ ]     [ ] [ ] [X]
        // [X] [ ] [ ]      [ ] [X] [ ]     [ ] [ ] [X]
        //
        private GameStatus AllFieldsInRowWinCondition(GameStatus currentStatus = null)
        {
            if (currentStatus == null)
            {
                currentStatus = new GameStatus() { GameProgress = GAME_STATUS.IN_PROGRESS };
            }

            // Win condition - all same player fields are in same row, all Y are equal
            List<Field> rowFields = new List<Field>();

            for (int i = 0; i < _fields.GetLength(0); i++)
            {
                rowFields.Clear();
                for (int j = 0; j < _fields.GetLength(1); j++)
                {
                    rowFields.Add(_fields[i,j]);
                }

                if (rowFields.FindAll(t => t.FieldStatus == FIELD_STATUS.PLAYER1).Count == rowFields.Count)
                {
                    currentStatus.WinCondition = WIN_CONDITION.ROW;
                    currentStatus.GameProgress = GAME_STATUS.PLAYER_ONE_WON;
                    currentStatus.WinRowOrColumn = i;
                    break;
                }

                if (rowFields.FindAll(t => t.FieldStatus == FIELD_STATUS.PLAYER2).Count == rowFields.Count)
                {
                    currentStatus.WinCondition = WIN_CONDITION.ROW;
                    currentStatus.GameProgress = GAME_STATUS.PLAYER_TWO_WON;
                    currentStatus.WinRowOrColumn = i;
                    break;
                }
            }

            return currentStatus;
        }

        // Handles cases
        // [X] [X] [X]      [ ] [ ] [ ]     [ ] [ ] [ ]
        // [ ] [ ] [ ]      [X] [X] [X]     [ ] [ ] [ ]
        // [ ] [ ] [ ]      [ ] [ ] [ ]     [X] [X] [X]
        //
        private GameStatus AllFieldsInColumnWinCondition(GameStatus currentStatus = null)
        {
            if (currentStatus == null)
            {
                currentStatus = new GameStatus() { GameProgress = GAME_STATUS.IN_PROGRESS };
            }

            // Win condition - all same player fields are in same column, all X are equal
            List<Field> columnFields = new List<Field>();

            for (int i = 0; i < _fields.GetLength(0); i++)
            {
                columnFields.Clear();
                for (int j = 0; j < _fields.GetLength(1); j++)
                {
                    columnFields.Add(_fields[j, i]);
                }

                if (columnFields.FindAll(t => t.FieldStatus == FIELD_STATUS.PLAYER1).Count == columnFields.Count)
                {
                    currentStatus.WinCondition = WIN_CONDITION.COLUMN;
                    currentStatus.GameProgress = GAME_STATUS.PLAYER_ONE_WON;
                    currentStatus.WinRowOrColumn = i;
                }

                if (columnFields.FindAll(t => t.FieldStatus == FIELD_STATUS.PLAYER2).Count == columnFields.Count)
                {
                    currentStatus.WinCondition = WIN_CONDITION.COLUMN;
                    currentStatus.GameProgress = GAME_STATUS.PLAYER_TWO_WON;
                    currentStatus.WinRowOrColumn = i;
                }
            }

            return currentStatus;
        }

        // Handles case
        // [X] [ ] [ ]
        // [ ] [X] [ ]
        // [ ] [ ] [X]
        //
        private GameStatus AllFieldsInMainDiagonalWinCondition(GameStatus currentStatus = null)
        {
            if (currentStatus == null)
            {
                currentStatus = new GameStatus() { GameProgress = GAME_STATUS.IN_PROGRESS };
            }

            List<Field> diagonalFields = new List<Field>();

            for (int i = 0; i < _fields.GetLength(0); i++)
            {
                diagonalFields.Add(_fields[i, i]);
            }

            if (diagonalFields.FindAll(t => t.FieldStatus == FIELD_STATUS.PLAYER1).Count == diagonalFields.Count)
            {
                currentStatus.WinCondition = WIN_CONDITION.MAIN_DIAGONAL;
                currentStatus.GameProgress = GAME_STATUS.PLAYER_ONE_WON;
            }

            if (diagonalFields.FindAll(t => t.FieldStatus == FIELD_STATUS.PLAYER2).Count == diagonalFields.Count)
            {
                currentStatus.WinCondition = WIN_CONDITION.MAIN_DIAGONAL;
                currentStatus.GameProgress = GAME_STATUS.PLAYER_TWO_WON;
            }

            return currentStatus;
        }

        // Handles case
        // [ ] [ ] [X]
        // [ ] [X] [ ]
        // [X] [ ] [ ]
        //
        private GameStatus AllFieldsInOppositeDiagonalWinCondition(GameStatus currentStatus = null)
        {
            if (currentStatus == null)
            {
                currentStatus = new GameStatus() { GameProgress = GAME_STATUS.IN_PROGRESS };
            }

            List<Field> oppDiagonalFields = new List<Field>();

            for (int i = 0; i < _fields.GetLength(0); i++)
            {
                oppDiagonalFields.Add(_fields[i, _fields.GetLength(0) - i - 1]);
            }

            if (oppDiagonalFields.FindAll(t => t.FieldStatus == FIELD_STATUS.PLAYER1).Count == oppDiagonalFields.Count)
            {
                currentStatus.GameProgress = GAME_STATUS.PLAYER_ONE_WON;
                currentStatus.WinCondition = WIN_CONDITION.OPP_DIAGONAL;
            }

            return currentStatus;
        }

        // Handles any case where ALL fields have value - TIE
        private GameStatus AllFieldsOcupiedWinCondition(GameStatus currentStatus = null)
        {
            if (currentStatus == null)
            {
                currentStatus = new GameStatus() { GameProgress = GAME_STATUS.IN_PROGRESS };
            }

            // iterate through all fields and if any is empty, just return current status
            for (int i = 0; i < _fields.GetLength(0); i++)
            {
                for (int j = 0; j < _fields.GetLength(1); j++)
                {
                    if (_fields[i, j].FieldStatus == FIELD_STATUS.EMPTY) 
                    {
                        return currentStatus;
                    }
                }
            }

            // else, declare TIE
            currentStatus.GameProgress = GAME_STATUS.TIE;
            currentStatus.WinCondition = WIN_CONDITION.NONE;
            return currentStatus;
        }
    }
}
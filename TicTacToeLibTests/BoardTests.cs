using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToeLib;

namespace TicTacToeLibTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TryTestAllFieldsFilledTiedCondition()
        {
            Field[,] fields = new Field[3, 3];

            fields[0, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };

            Board board = new Board(fields);

            board.GameEnd += (sender, e) =>
            {
                Assert.AreEqual(GAME_STATUS.TIE, e.GameProgress);
                Assert.AreEqual(WIN_CONDITION.NONE, e.WinCondition);
            };
            fields[2, 1].FieldStatus = FIELD_STATUS.PLAYER1;
            fields[2, 2].FieldStatus = FIELD_STATUS.PLAYER2;
            fields[1, 1].FieldStatus = FIELD_STATUS.PLAYER1;
            fields[0, 1].FieldStatus = FIELD_STATUS.PLAYER2;
            // [0] [X] [X] 
            // [X] [0] [0]
            // [0] [0] [X]
            fields[1, 2].FieldStatus = FIELD_STATUS.PLAYER1;
            fields[1, 0].FieldStatus = FIELD_STATUS.PLAYER2;
            fields[0, 0].FieldStatus = FIELD_STATUS.PLAYER1;
            fields[0, 2].FieldStatus = FIELD_STATUS.PLAYER2;
            fields[2, 0].FieldStatus = FIELD_STATUS.PLAYER1;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FieldNull()
        {
            Board board = new Board(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FieldNotSquareMatrix()
        {
            Field[,] fields = new Field[2, 3];

            fields[0, 0] = new Field();
            fields[0, 1] = new Field();
            fields[0, 2] = new Field();
            fields[1, 0] = new Field();
            fields[1, 1] = new Field();
            fields[1, 2] = new Field();

            Board board = new Board(fields);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullFieldsInMatrix()
        {
            Field[,] fields = new Field[3, 3];

            fields[0, 0] = new Field();
            fields[0, 1] = new Field();
            fields[0, 2] = new Field();
            fields[1, 0] = new Field();
            fields[1, 1] = null;
            fields[1, 2] = new Field();
            fields[2, 2] = new Field();
            fields[2, 1] = new Field();
            fields[2, 0] = new Field();

            Board board = new Board(fields);
        }

        [TestMethod]
        public void TypicalCase()
        {
            Field[,] fields = new Field[3, 3];

            fields[0, 0] = new Field();
            fields[0, 1] = new Field();
            fields[0, 2] = new Field();
            fields[1, 0] = new Field();
            fields[1, 1] = new Field();
            fields[1, 2] = new Field();
            fields[2, 0] = new Field();
            fields[2, 1] = new Field();
            fields[2, 2] = new Field();

            Board board = new Board(fields);

            Assert.AreEqual(fields.GetLength(0), board.Fields.GetLength(0));
            Assert.AreEqual(fields.GetLength(1), board.Fields.GetLength(1));
        }

        [TestMethod]
        public void TestAllFieldsInRowWinCondition()
        {
            Field[,] fields = new Field[3, 3];

            fields[0, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };

            Board board = new Board(fields);

            board.GameEnd += (sender, e) =>
            {
                Assert.AreEqual(GAME_STATUS.PLAYER_ONE_WON, e.GameProgress);
                Assert.AreEqual(WIN_CONDITION.ROW, e.WinCondition);
                Assert.AreEqual(0, e.WinRowOrColumn);
            };

            fields[0, 0].FieldStatus = FIELD_STATUS.PLAYER1;
            // [X] [ ] [ ] 
            // [ ] [ ] [ ]
            // [ ] [ ] [ ]
            fields[1, 1].FieldStatus = FIELD_STATUS.PLAYER2;
            // [X] [ ] [ ] 
            // [ ] [0] [ ]
            // [ ] [ ] [ ]
            fields[0, 1].FieldStatus = FIELD_STATUS.PLAYER1;
            // [X] [X] [ ] 
            // [ ] [0] [ ]
            // [ ] [ ] [ ]
            fields[2, 2].FieldStatus = FIELD_STATUS.PLAYER2;
            // [X] [X] [ ] 
            // [ ] [0] [ ]
            // [ ] [ ] [0]
            fields[0, 2].FieldStatus = FIELD_STATUS.PLAYER1;
            // [X] [X] [X] 
            // [ ] [0] [ ]
            // [ ] [ ] [0]
        }

        [TestMethod]
        public void TestAllFieldsInColumnCondition()
        {
            Field[,] fields = new Field[3, 3];

            fields[0, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };

            Board board = new Board(fields);

            board.GameEnd += (sender, e) =>
                {
                    Assert.AreEqual(GAME_STATUS.PLAYER_ONE_WON, e.GameProgress);
                    Assert.AreEqual(WIN_CONDITION.COLUMN, e.WinCondition);
                    Assert.AreEqual(1, e.WinRowOrColumn);
                };

            fields[0, 1].FieldStatus = FIELD_STATUS.PLAYER1;
            // [ ] [X] [ ] 
            // [ ] [ ] [ ]
            // [ ] [ ] [ ]
            fields[0, 2].FieldStatus = FIELD_STATUS.PLAYER2;
            // [ ] [X] [0] 
            // [ ] [ ] [ ]
            // [ ] [ ] [ ]
            fields[2, 1].FieldStatus = FIELD_STATUS.PLAYER1;
            // [ ] [X] [0] 
            // [ ] [ ] [ ]
            // [ ] [X] [ ]
            fields[1, 0].FieldStatus = FIELD_STATUS.PLAYER2;
            // [ ] [X] [0] 
            // [0] [ ] [ ]
            // [ ] [X] [ ]
            fields[1, 1].FieldStatus = FIELD_STATUS.PLAYER1;
            // [ ] [X] [0] 
            // [0] [X] [ ]
            // [ ] [X] [ ]
        }

        [TestMethod]
        public void TestAllFieldsInMainDiagonalCondition()
        {
            Field[,] fields = new Field[3, 3];

            fields[0, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };

            Board board = new Board(fields);

            board.GameEnd += (sender, e) =>
                {
                    Assert.AreEqual(GAME_STATUS.PLAYER_TWO_WON, e.GameProgress);
                    Assert.AreEqual(WIN_CONDITION.MAIN_DIAGONAL, e.WinCondition);
                };

            fields[0, 2].FieldStatus = FIELD_STATUS.PLAYER1;
            // [ ] [ ] [X] 
            // [ ] [ ] [ ]
            // [ ] [ ] [ ]
            fields[0, 0].FieldStatus = FIELD_STATUS.PLAYER2;
            // [0] [ ] [X] 
            // [ ] [ ] [ ]
            // [ ] [ ] [ ]
            fields[1, 0].FieldStatus = FIELD_STATUS.PLAYER1;
            // [0] [ ] [X] 
            // [X] [ ] [ ]
            // [ ] [ ] [ ]
            fields[2, 2].FieldStatus = FIELD_STATUS.PLAYER2;
            // [0] [ ] [X] 
            // [X] [ ] [ ]
            // [ ] [ ] [0]
            fields[2, 1].FieldStatus = FIELD_STATUS.PLAYER1;
            // [0] [ ] [X] 
            // [X] [ ] [ ]
            // [ ] [X] [0]
            fields[1, 1].FieldStatus = FIELD_STATUS.PLAYER2;
            // [0] [ ] [X] 
            // [X] [0] [ ]
            // [ ] [X] [0]
        }

        [TestMethod]
        public void TestAllFieldsInOppositeDiagonalCondition()
        {
            Field[,] fields = new Field[3, 3];

            fields[0, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };

            Board board = new Board(fields);

            board.GameEnd += (sender, e) =>
                {
                    Assert.AreEqual(GAME_STATUS.PLAYER_TWO_WON, e.GameProgress);
                    Assert.AreEqual(WIN_CONDITION.OPP_DIAGONAL, e.WinCondition);
                };

            fields[0, 2].FieldStatus = FIELD_STATUS.PLAYER2;
            // [ ] [ ] [0] 
            // [ ] [ ] [ ]
            // [ ] [ ] [ ]
            fields[0, 1].FieldStatus = FIELD_STATUS.PLAYER1;
            // [ ] [X] [0] 
            // [ ] [ ] [ ]
            // [ ] [ ] [ ]
            fields[1, 1].FieldStatus = FIELD_STATUS.PLAYER2;
            // [ ] [X] [0] 
            // [ ] [0] [ ]
            // [ ] [ ] [ ]
            fields[1, 0].FieldStatus = FIELD_STATUS.PLAYER1;
            // [ ] [X] [0] 
            // [X] [0] [ ]
            // [ ] [ ] [ ]
            fields[2, 0].FieldStatus = FIELD_STATUS.PLAYER2;
            // [ ] [X] [0] 
            // [X] [0] [ ]
            // [0] [ ] [ ]
        }

        [TestMethod]
        public void AnotherTestAllFieldsFilledTiedCondition()
        {
            Field[,] fields = new Field[3, 3];

            fields[0, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[0, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[1, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 0] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 1] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };
            fields[2, 2] = new Field() { FieldStatus = FIELD_STATUS.EMPTY };

            Board board = new Board(fields);

            board.GameEnd += (sender, e) =>
                {
                    Assert.AreEqual(GAME_STATUS.TIE, e.GameProgress);
                    Assert.AreEqual(WIN_CONDITION.NONE, e.WinCondition);
                };
            fields[1, 1].FieldStatus = FIELD_STATUS.PLAYER1;
            // [ ] [ ] [ ] 
            // [ ] [X] [ ]
            // [ ] [ ] [ ]
            fields[1, 2].FieldStatus = FIELD_STATUS.PLAYER2;
            // [ ] [ ] [ ] 
            // [ ] [X] [0]
            // [ ] [ ] [ ]
            fields[2, 1].FieldStatus = FIELD_STATUS.PLAYER1;
            // [ ] [ ] [ ] 
            // [ ] [X] [0]
            // [ ] [X] [ ]
            fields[0, 1].FieldStatus = FIELD_STATUS.PLAYER2;
            // [ ] [0] [ ] 
            // [ ] [X] [0]
            // [ ] [X] [ ]
            fields[0, 2].FieldStatus = FIELD_STATUS.PLAYER1;
            // [ ] [0] [X] 
            // [ ] [X] [0]
            // [ ] [X] [ ]
            fields[2, 0].FieldStatus = FIELD_STATUS.PLAYER2;
            // [ ] [0] [X] 
            // [ ] [X] [0]
            // [0] [X] [ ]
            fields[2, 2].FieldStatus = FIELD_STATUS.PLAYER1;
            // [ ] [0] [X] 
            // [ ] [X] [0]
            // [0] [X] [X]
            fields[0, 0].FieldStatus = FIELD_STATUS.PLAYER2;
            // [0] [0] [X] 
            // [ ] [X] [0]
            // [0] [X] [X]
            fields[1, 0].FieldStatus = FIELD_STATUS.PLAYER1;
            // [0] [0] [X] 
            // [X] [X] [0]
            // [0] [X] [X]
        }
    }
}

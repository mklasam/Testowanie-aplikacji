using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeLib;

namespace TicTacToe
{
    public partial class FormMain : Form
    {
        #region private members

        private Board _board;
        private bool _user1Turn;

        #endregion

        #region constructor

        public FormMain()
        {
            InitializeComponent();
        }

        #endregion

        #region event handlers

        private void FormMain_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        void _board_GameEnd(object sender, GameStatus e)
        {
            DisableButtons();

            lblStatus.Text = e.GameProgress == GAME_STATUS.PLAYER_ONE_WON ? "Player one won" :
                e.GameProgress == GAME_STATUS.PLAYER_TWO_WON ? "Player two won" : "TIE";

            SetEndGameButtonsBackColor(e);
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            SetFieldStatus(0, 0, sender as Button);
        }

        private void btn01_Click(object sender, EventArgs e)
        {
            SetFieldStatus(0, 1, sender as Button);
        }

        private void btn02_Click(object sender, EventArgs e)
        {
            SetFieldStatus(0, 2, sender as Button);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            SetFieldStatus(1, 0, sender as Button);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            SetFieldStatus(1, 1, sender as Button);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            SetFieldStatus(1, 2, sender as Button);
        }

        private void btn20_Click(object sender, EventArgs e)
        {
            SetFieldStatus(2, 0, sender as Button);
        }

        private void btn21_Click(object sender, EventArgs e)
        {
            SetFieldStatus(2, 1, sender as Button);
        }

        private void btn22_Click(object sender, EventArgs e)
        {
            SetFieldStatus(2, 2, sender as Button);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region private methods

        private void StartGame()
        {
            InitBoard();
            _user1Turn = true;
            UpdateStatusLabel();
            ClearButtonsText();
            ResetButtonsForeColor();
            EnableButtons();
        }

        private void InitBoard()
        {
            if (_board == null)
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

                _board = new Board(fields);
                _board.GameEnd += _board_GameEnd;
            }
            else
            {
                _board.Fields[0, 0].FieldStatus = FIELD_STATUS.EMPTY;
                _board.Fields[0, 1].FieldStatus = FIELD_STATUS.EMPTY;
                _board.Fields[0, 2].FieldStatus = FIELD_STATUS.EMPTY;
                _board.Fields[1, 0].FieldStatus = FIELD_STATUS.EMPTY;
                _board.Fields[1, 1].FieldStatus = FIELD_STATUS.EMPTY;
                _board.Fields[1, 2].FieldStatus = FIELD_STATUS.EMPTY;
                _board.Fields[2, 0].FieldStatus = FIELD_STATUS.EMPTY;
                _board.Fields[2, 1].FieldStatus = FIELD_STATUS.EMPTY;
                _board.Fields[2, 2].FieldStatus = FIELD_STATUS.EMPTY;
            }
        }

        private void SetFieldStatus(int indexX, int indexY, Button sender)
        {
            if (_board.Fields[indexX, indexY].FieldStatus == FIELD_STATUS.EMPTY)
            {
                _board.Fields[indexX, indexY].FieldStatus = _user1Turn ? FIELD_STATUS.PLAYER1 : FIELD_STATUS.PLAYER2;
                if (sender != null)
                {
                    sender.Text = _user1Turn ? "X" : "O";
                    if (sender.Enabled)
                    {
                        // dont execute for restart game
                        _user1Turn = !_user1Turn;
                        UpdateStatusLabel();
                    }
                }
            }
        }

        #endregion

        #region set control properties

        private void SetEndGameButtonsBackColor(GameStatus endGameStatus)
        {
            if (endGameStatus != null && endGameStatus.GameProgress != GAME_STATUS.IN_PROGRESS)
            {
                if (endGameStatus.WinCondition == WIN_CONDITION.ROW)
                {
                    if (endGameStatus.WinRowOrColumn == 0)
                    {
                        btn00.BackColor = Color.Red;
                        btn01.BackColor = Color.Red;
                        btn02.BackColor = Color.Red;
                    }
                    else if (endGameStatus.WinRowOrColumn == 1)
                    {
                        btn10.BackColor = Color.Red;
                        btn11.BackColor = Color.Red;
                        btn12.BackColor = Color.Red;
                    }
                    else if (endGameStatus.WinRowOrColumn == 2)
                    {
                        btn20.BackColor = Color.Red;
                        btn21.BackColor = Color.Red;
                        btn22.BackColor = Color.Red;
                    }
                }
                else if (endGameStatus.WinCondition == WIN_CONDITION.COLUMN)
                {
                    if (endGameStatus.WinRowOrColumn == 0)
                    {
                        btn00.BackColor = Color.Red;
                        btn10.BackColor = Color.Red;
                        btn20.BackColor = Color.Red;
                    }
                    else if (endGameStatus.WinRowOrColumn == 1)
                    {
                        btn01.BackColor = Color.Red;
                        btn11.BackColor = Color.Red;
                        btn21.BackColor = Color.Red;
                    }
                    else if (endGameStatus.WinRowOrColumn == 2)
                    {
                        btn02.BackColor = Color.Red;
                        btn12.BackColor = Color.Red;
                        btn22.BackColor = Color.Red;
                    }
                }
                else if (endGameStatus.WinCondition == WIN_CONDITION.MAIN_DIAGONAL)
                {
                    btn00.BackColor = Color.Red;
                    btn11.BackColor = Color.Red;
                    btn22.BackColor = Color.Red;
                }
                else if (endGameStatus.WinCondition == WIN_CONDITION.OPP_DIAGONAL)
                {
                    btn20.BackColor = Color.Red;
                    btn11.BackColor = Color.Red;
                    btn02.BackColor = Color.Red;
                }
            }
        }

        private void UpdateStatusLabel()
        {
            lblStatus.Text = _user1Turn ? "Player one turn" : "Player two turn";
        }

        private void EnableButtons()
        {
            btn00.Enabled = true;
            btn01.Enabled = true;
            btn02.Enabled = true;
            btn10.Enabled = true;
            btn11.Enabled = true;
            btn12.Enabled = true;
            btn20.Enabled = true;
            btn21.Enabled = true;
            btn22.Enabled = true;
        }

        private void DisableButtons()
        {
            btn00.Enabled = false;
            btn01.Enabled = false;
            btn02.Enabled = false;
            btn10.Enabled = false;
            btn11.Enabled = false;
            btn12.Enabled = false;
            btn20.Enabled = false;
            btn21.Enabled = false;
            btn22.Enabled = false;
        }

        private void ClearButtonsText()
        {
            btn00.Text = string.Empty;
            btn01.Text = string.Empty;
            btn02.Text = string.Empty;
            btn10.Text = string.Empty;
            btn11.Text = string.Empty;
            btn12.Text = string.Empty;
            btn20.Text = string.Empty;
            btn21.Text = string.Empty;
            btn22.Text = string.Empty;
        }

        private void ResetButtonsForeColor()
        {
            btn00.BackColor = Control.DefaultBackColor;
            btn01.BackColor = Control.DefaultBackColor;
            btn02.BackColor = Control.DefaultBackColor;
            btn10.BackColor = Control.DefaultBackColor;
            btn11.BackColor = Control.DefaultBackColor;
            btn12.BackColor = Control.DefaultBackColor;
            btn20.BackColor = Control.DefaultBackColor;
            btn21.BackColor = Control.DefaultBackColor;
            btn22.BackColor = Control.DefaultBackColor;
        }

        #endregion
    }
}
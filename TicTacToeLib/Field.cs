using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLib
{
    public enum FIELD_STATUS
    {
        EMPTY,
        PLAYER1,
        PLAYER2
    }

    public class Field
    {
        private FIELD_STATUS _fieldStatus;

        public event EventHandler FieldStatusChanged;

        public Field()
        {
            _fieldStatus = FIELD_STATUS.EMPTY;
        }

        public FIELD_STATUS FieldStatus
        {
            get
            {
                return _fieldStatus;
            }

            set
            {
                if (value != _fieldStatus)
                {
                    _fieldStatus = value;
                    OnFieldStatusChanged();
                }
            }
        }

        private void OnFieldStatusChanged()
        {
            if (FieldStatusChanged != null)
            {
                FieldStatusChanged(this, null);
            }
        }
    }
}
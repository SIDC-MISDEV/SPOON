using System;

namespace Spoon.Inventory.Model
{
    public class DocumentLines
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _BaseLine;

        public int BaseLine
        {
            get { return _BaseLine; }
            set { _BaseLine = value; }
        }


        private Items _Item;

        public Items Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private Users _EncodeBy;

        public Users EncodeBy
        {
            get { return _EncodeBy; }
            set { _EncodeBy = value; }
        }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private string _WarehouseCode;

        public string WarehouseCode
        {
            get { return _WarehouseCode; }
            set { _WarehouseCode = value; }
        }

        private DateTime _DateSaved;

        public DateTime DateSaved
        {
            get { return _DateSaved; }
            set { _DateSaved = value; }
        }


        private int _GridIndex;

        public int GridIndex
        {
            get { return _GridIndex; }
            set { _GridIndex = value; }
        }

        public DocumentLines()
        {
            this._Item = new Items();
            this._EncodeBy = new Users();
        }
        public DocumentLines(int gridindex, int baseline)
        {
            this._Item = new Items();
            this._EncodeBy = new Users();
            this._GridIndex = gridindex;
            this._BaseLine = baseline;
        }
    }
}

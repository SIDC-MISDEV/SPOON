namespace Spoon.Inventory.Model
{
    public class Units
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _ItemCode;

        public string ItemCode
        {
            get { return _ItemCode == @"\N" ? null : _ItemCode; }
            set { _ItemCode = value; }
        }

        private string _Barcode;

        public string Barcode
        {
            get { return _Barcode == @"\N" ? null : _Barcode; }
            set { _Barcode = value; }
        }

        private decimal _Cost;

        public decimal Cost
        {
            get { return _Cost; }
            set { _Cost = value; }
        }

        private decimal _Selling;

        public decimal Selling
        {
            get { return _Selling; }
            set { _Selling = value; }
        }

        private decimal _MarkUp;

        public decimal MarkUp
        {
            get { return _MarkUp; }
            set { _MarkUp = value; }
        }

        private decimal _Conversion;

        public decimal Conversion
        {
            get { return _Conversion; }
            set { _Conversion = value; }
        }

        private string _CostingMethod;

        public string CostingMethod
        {
            get { return _CostingMethod == @"\N" ? null : _CostingMethod; }
            set { _CostingMethod = value; }
        }

        private string _Unit;

        public string Unit
        {
            get { return _Unit == @"\N" ? null : _Unit; }
            set { _Unit = value; }
        }

    }
}

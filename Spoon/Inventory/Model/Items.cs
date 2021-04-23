namespace Spoon.Inventory.Model
{
    public class Items
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

        private string _Barcode2;

        public string Barcode2
        {
            get { return _Barcode2; }
            set { _Barcode2 = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description == @"\N" ? null : _Description; }
            set { _Description = value; }
        }

        private string _Unit;

        public string Unit
        {
            get { return _Unit == @"\N" ? null : _Unit; }
            set { _Unit = value; }
        }

        private decimal _Price;

        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private decimal _Quantity;

        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private decimal _RemainingQuantity;

        public decimal RemainingQuantity
        {
            get { return _RemainingQuantity; }
            set { _RemainingQuantity = value; }
        }

        private string _Status;

        public string Status
        {
            get { return _Status == @"\N" ? null : _Status; }
            set { _Status = value; }
        }

        private string _Remarks;

        public string Remarks
        {
            get { return _Remarks == @"\N" ? null : _Remarks; }
            set { _Remarks = value; }
        }

        private bool _Discontinued;

        public bool Discountinued
        {
            get { return _Discontinued; }
            set { _Discontinued = value; }
        }

        private bool _Consignment;

        public bool Consignment
        {
            get { return _Consignment; }
            set { _Consignment = value; }
        }

        private bool _Taxable;

        public bool Taxable
        {
            get { return _Taxable; }
            set { _Taxable = value; }
        }

        private bool _Discountable;

        public bool Discountable
        {
            get { return _Discountable; }
            set { _Discountable = value; }
        }

        private string _Packaging;

        public string Packaging
        {
            get { return _Packaging == @"\N" ? null : _Packaging; }
            set { _Packaging = value; }
        }

        private decimal _Conversion;

        public decimal Conversion
        {
            get { return _Conversion; }
            set { _Conversion = value; }
        }



        #region POS1

        private string _s1Category;

        public string s1Category
        {
            get { return _s1Category == @"\N" ? null : _s1Category; }
            set { _s1Category = value; }
        }

        private string _s1Supplier;

        public string s1Supplier
        {
            get { return _s1Supplier == @"\N" ? null : _s1Supplier; }
            set { _s1Supplier = value; }
        }

        private string _s1CategoryID;

        public string s1CategoryID
        {
            get { return _s1CategoryID == @"\N" ? null : _s1CategoryID; }
            set { _s1CategoryID = value; }
        }

        private string _s1SubCategoryID;

        public string s1SubCategoryID
        {
            get { return _s1SubCategoryID == @"\N" ? null : _s1SubCategoryID; }
            set { _s1SubCategoryID = value; }
        }

        private string _s1SubCategory;

        public string s1SubCategory
        {
            get { return _s1SubCategory == @"\N" ? null : _s1SubCategory; }
            set { _s1SubCategory = value; }
        }


        private string _s1Brand;

        public string s1Brand
        {
            get { return _s1Brand == @"\N" ? null : _s1Brand; }
            set { _s1Brand = value; }
        }

        private string _s1BrandID;

        public string s1BrandID
        {
            get { return _s1BrandID == @"\N" ? null : _s1BrandID; }
            set { _s1BrandID = value; }
        }

        #endregion


        public Items()
        {

        }


    }
}

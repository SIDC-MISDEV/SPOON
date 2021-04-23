namespace Spoon.Inventory.Model
{
    public class DocumentTypes
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Code;

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private int _Series;

        public int Series
        {
            get { return _Series; }
            set { _Series = value; }
        }

        private bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        private bool _RequireCrossreference;

        public bool RequireCrossreference
        {
            get { return _RequireCrossreference; }
            set { _RequireCrossreference = value; }
        }

        private bool _ItemFromCrossReference;

        public bool ItemFromCrossReference
        {
            get { return _ItemFromCrossReference; }
            set { _ItemFromCrossReference = value; }
        }

        private string _Template;

        public string Template
        {
            get { return _Template; }
            set { _Template = value; }
        }

        private string _ObjectType;

        public string ObjectType
        {
            get { return _ObjectType; }
            set { _ObjectType = value; }
        }

        private string _AccountCode;

        public string AccountCode
        {
            get { return _AccountCode; }
            set { _AccountCode = value; }
        }





    }
}

namespace Spoon.Inventory.Model
{
    public class Users
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _MiddleName;

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }


        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _ConfirmPassword;

        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }

        private bool _IsAdmin;

        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }

        private bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        private string _Positition;

        public string Position
        {
            get { return _Positition; }
            set { _Positition = value; }
        }




    }
}

using System;
using System.Collections.Generic;
using System.Data;

namespace Spoon.Inventory.Model
{
    public class Document
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _DocNum;

        public int DocNum
        {
            get { return _DocNum; }
            set { _DocNum = value; }
        }

        private int _CrossDocNum;

        public int CrossDocNum
        {
            get { return _CrossDocNum; }
            set { _CrossDocNum = value; }
        }

        private DocumentTypes _DocumentType;

        public DocumentTypes DocumentType
        {
            get { return _DocumentType; }
            set { _DocumentType = value; }
        }


        private string _Code;

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private DateTime _PostedDate;

        public DateTime PostedDate
        {
            get { return _PostedDate; }
            set { _PostedDate = value; }
        }

        private string _Reference;

        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        private string _CrossReference;

        public string CrossReference
        {
            get { return _CrossReference; }
            set { _CrossReference = value; }
        }
        private int _CrossID;

        public int CrossID
        {
            get { return _CrossID; }
            set { _CrossID = value; }
        }

        private string _Remarks;

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private Boolean _Status;

        public Boolean Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private Boolean _ExtractedToSAP;

        public Boolean ExtractedToSAP
        {
            get { return _ExtractedToSAP; }
            set { _ExtractedToSAP = value; }
        }

        private Users _EncodeBy;

        public Users EncodeBy
        {
            get { return _EncodeBy; }
            set { _EncodeBy = value; }
        }



        private Users _AssignTo;

        public Users AssignTo
        {
            get { return _AssignTo; }
            set { _AssignTo = value; }
        }

        private List<DocumentLines> _DocumentLine;

        public List<DocumentLines> DocumentLine
        {
            get { return _DocumentLine; }
            set { _DocumentLine = value; }
        }

        private DataTable _DataLines;

        public DataTable DataLines
        {
            get { return _DataLines; }
            set { _DataLines = value; }
        }

        private bool _IsCancelled;

        public bool IsCancelled
        {
            get { return _IsCancelled; }
            set { _IsCancelled = value; }
        }


        private string _Warehouse;

        public string Warehouse
        {
            get { return _Warehouse; }
            set { _Warehouse = value; }
        }

        private string _BranchCode;

        public string BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }
        private string _BusinessSegment;

        public string BusinessSegment
        {
            get { return _BusinessSegment; }
            set { _BusinessSegment = value; }
        }

        private string _MainSegment;

        public string MainSegment
        {
            get { return _MainSegment; }
            set { _MainSegment = value; }
        }

        private string _PlateNumber;

        public string PlateNumber
        {
            get { return _PlateNumber; }
            set { _PlateNumber = value; }
        }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }


        public Document()
        {
            _EncodeBy = new Users();
            _AssignTo = new Users();
            _DocumentType = new DocumentTypes();
        }







    }
}

using Spoon.Inventory.Interface;
using Spoon.Inventory.Model;
using Spoon.Inventory.Service;
using System.Windows.Forms;

namespace Spoon.Inventory.Controller
{
    public class TransferController
    {
        ITransferView _view;
        ATransaction adocument;
        Document _document;
        Form _frm;
        public TransferController(ITransferView view)
        {
            _view = view;
            view.SetController(this);
        }

        public string Save()
        {

            this.adocument = new ATransaction();
            string response = null;


            if (string.IsNullOrWhiteSpace(_view.document.Code))
                response += "Code is required\n";


            if (_view.document.DocumentLine.Count == 0)
                response += "Transaction details is missing.\n";

            if (_view._item.Count == 0)
                response += "Item master data is missing.\n";


            if (_view._unit.Count == 0)
                response += "Item units is missing.\n";


            var ax = _view._item;
            var ax2 = _view._unit;


            if (string.IsNullOrWhiteSpace(response))
                response = this.adocument.Save(_view.document, _view._item, _view._unit);

            if (string.IsNullOrWhiteSpace(response))
                this._view.document = new Document();

            return response;
        }


    }
}

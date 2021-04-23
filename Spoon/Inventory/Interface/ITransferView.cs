using Spoon.Inventory.Controller;
using Spoon.Inventory.Model;
using System.Collections.Generic;

namespace Spoon.Inventory.Interface
{
    public interface ITransferView
    {
        void SetController(TransferController controller);
        Document document { get; set; }
        List<Items> _item { get; }
        List<Units> _unit { get; }
    }
}

using Manager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IItemService
    {
        List<Item> getItemList(string itemName, int _unitPrice, int _amount, bool amountCond, string email);
  
    }
}

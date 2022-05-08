using Business;
using Manager.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{   [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class ItemController
    {   
        [HttpGet]
        public List<Item> getItems(string itemName, int unitPrice, int amount, bool amountCond)
        {
            new ItemManager().getItemListx();
            return new ItemManager().getItemList( "1",  1,  1,  false, "");
        }
    }
}

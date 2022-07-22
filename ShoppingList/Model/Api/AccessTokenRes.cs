using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Model.Api; 

internal class AccessTokenRes
{
    public int expires_in{ get; set; }
    public string access_token { get; set; }
    public string token_type { get; set; }

}

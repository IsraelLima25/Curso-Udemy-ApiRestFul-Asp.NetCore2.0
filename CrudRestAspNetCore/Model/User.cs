using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Model
{
    public class User
    {
        public int? Id{ get; set; }
        public string login { get; set; }
        public string AcessKey { get; set; }
        

    }
}

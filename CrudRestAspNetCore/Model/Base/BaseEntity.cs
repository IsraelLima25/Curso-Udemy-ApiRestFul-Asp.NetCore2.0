using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CrudRestAspNetCore.Model.Base
{   //Contrato entre atributos e a estrura da tabela
    //[DataContract]
    public class BaseEntity
    {
        public int? id { get; set; }
        
    }
}

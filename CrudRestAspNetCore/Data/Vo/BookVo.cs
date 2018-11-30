using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Data.Vo
{
    [DataContract]
    public class BookVo
    {
        [DataMember(Order = 1, Name = "Codigo")]
        public int? id { get; set; }
        [DataMember(Order = 3, Name = "Titulo")]
        public string Title { get; set; }
        [DataMember(Order = 2, Name = "Autor")]
        public string Author { get; set; }
        [DataMember(Order = 5, Name = "Preço")]
        public decimal Price { get; set; }
        [DataMember(Order = 4, Name = "Ultimo Lançamento")]
        public DateTime LaunchDate { get; set; }
    }
}

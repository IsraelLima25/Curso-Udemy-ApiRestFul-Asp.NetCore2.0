using System.Collections.Generic;
using Tapioca.HATEOAS;

namespace CrudRestAspNetCore.Data.Vo
{
    public class PersonVo : ISupportsHyperMedia
    {
        public int? id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}

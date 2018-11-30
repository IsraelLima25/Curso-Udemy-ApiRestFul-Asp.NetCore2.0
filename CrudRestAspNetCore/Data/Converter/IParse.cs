using System.Collections.Generic;

namespace CrudRestAspNetCore.Data.Converter
{
    public interface IParse<O, D>
    {
        D Parse(O origin);
        List<D> ParserList(List<O> origin);

    }
}

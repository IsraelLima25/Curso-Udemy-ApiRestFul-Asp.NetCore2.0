using CrudRestAspNetCore.Data.Converter;
using CrudRestAspNetCore.Data.Vo;
using CrudRestAspNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Data.Converters
{
    public class PersonConverter : IParse<PersonVo, PersonVO>, IParse<PersonVO, PersonVo>

    {
        public PersonVo Parse(PersonVO origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                PersonVo person = new PersonVo();
                person.id = origin.id;
                person.FirstName = origin.FirstName;
                person.LastName = origin.LastName;
                person.Address = origin.Address;
                person.Gender = origin.Gender;
                return person;
            }

        }

        public PersonVO Parse(PersonVo origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                PersonVO person = new PersonVO();
                person.id = origin.id;
                person.FirstName = origin.FirstName;
                person.LastName = origin.LastName;
                person.Address = origin.Address;
                person.Gender = origin.Gender;
                return person;
            }
        }

        public List<PersonVo> ParserList(List<PersonVO> origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return origin.Select(item => Parse(item)).ToList();
            }
        }

        public List<PersonVO> ParserList(List<PersonVo> origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return origin.Select(item => Parse(item)).ToList();
            }
        }
    }
}

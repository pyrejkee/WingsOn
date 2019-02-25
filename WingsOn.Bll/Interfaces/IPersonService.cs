using System.Collections.Generic;
using WingsOn.Bll.Dtos;
using WingsOn.Bll.Helpers;

namespace WingsOn.Bll.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<PersonDto> GetAll(PersonResourceParameters personResourceParameters);

        IEnumerable<PersonDto> GetAll();

        PersonDto Get(int id);

        void Save(PersonDto personDto);
    }
}

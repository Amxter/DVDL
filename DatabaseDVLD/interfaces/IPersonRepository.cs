using DatabaseDVLD.Repositories;
using System.Data;

namespace DatabaseDVLD
{
    public interface IPersonRepository : IGeneralRepository<Person>
    {
        bool IsExistsNationalNo(string nationalNo);
        Person GetByNationalNo(string nationalNo);
    }
}

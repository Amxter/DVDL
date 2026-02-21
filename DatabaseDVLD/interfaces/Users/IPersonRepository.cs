using DatabaseDVLD.Repositories;
using System.Data;

namespace DatabaseDVLD
{
    public interface IPersonRepository : IGeneralRepository<Person>
    {
        bool IsExistsByNationalNo(string nationalNo);
        Person GetByNationalNo(string nationalNo);
        Person GetByID(int entityID);
        bool IsExistsByID(int entityID);
    }
}

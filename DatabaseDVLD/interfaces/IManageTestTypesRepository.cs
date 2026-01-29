using System.Data;

namespace DatabaseDVLD
{
    public interface IManageTestTypesRepository
    {

        DataTable GetAll();
        bool Update(Test test);
    }
}



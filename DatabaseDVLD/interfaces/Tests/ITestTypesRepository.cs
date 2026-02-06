using System.Data;

namespace DatabaseDVLD
{
    public interface ITestTypesRepository
    {

        DataTable GetAll();
        bool Update(TestType test);
        TestType GetByID(int TestTypeID);
    }
}



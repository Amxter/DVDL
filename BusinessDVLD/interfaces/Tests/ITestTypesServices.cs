using System.Data;

namespace BusinessDVLD
{
    public interface ITestTypesServices
    {
        DataTable GetAll();
        bool Upgrade(TestTypeDTO testDTO);
        TestTypeDTO GetByID(int testTypeID);

    }
}

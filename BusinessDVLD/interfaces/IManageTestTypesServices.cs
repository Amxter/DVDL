using System.Data;

namespace BusinessDVLD
{
    public interface IManageTestTypesServices
    {
        DataTable GetAll();
        bool Upgrade(TestDTO testDTO);

    }
}

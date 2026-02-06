using System.Data;

namespace BusinessDVLD
{
    public interface IDriverServices
    {
        int Add(DriverDTO driverDTO);
        DataTable GetAll();
        int IsExistByPersonID(int personID);
    }
}

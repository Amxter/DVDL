using System.Data;

namespace DatabaseDVLD
{
    public interface IDriverRepository
    {
        int Add(Driver driver);
        DataTable GetAll();
        int IsExistByPersonID(int personID);
    }


}




using System.Data;

namespace DatabaseDVLD
{
    public interface ICountryRepository
    {
        DataTable GetAll();
        string GetCountryNameByID(int country);
    }
}

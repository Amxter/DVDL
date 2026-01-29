using System.Data;

namespace BusinessDVLD
{
    public interface ICountryServices
    {
        DataTable GetAll();
        string GetCountryNameByID(int country);
    }
}

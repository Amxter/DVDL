using System.Data;

namespace BusinessDVLD
{
    public interface IGeneralServices<DTO>
    {
        int  Add(DTO dTO);
        bool  Update(DTO dTO);
        bool Delete(int dTO);
        DataTable  GetAll();
        DTO GetByID(int dTOID);
        bool IsExistsByID(int dTOID);

    }
}

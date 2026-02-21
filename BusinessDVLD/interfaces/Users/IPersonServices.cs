



namespace BusinessDVLD
{
    public interface IPersonServices : IGeneralServices<PersonDTO>
    {

        bool IsExistsByNationalNo(string nationalNo);
        PersonDTO GetByNationalNo(string  id);
        PersonDTO GetByID(int dTOID);
        bool IsExistsByID(int dTOID);
    }
}

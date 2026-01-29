



namespace BusinessDVLD
{
    public interface IPersonServices : IGeneralServices<PersonDTO>
    {

        bool IsExistsNationalNo(string nationalNo);
        PersonDTO GetByNationalNo(string  id);
    }
}

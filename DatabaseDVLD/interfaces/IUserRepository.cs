using DatabaseDVLD.Repositories;

namespace DatabaseDVLD
{
    public interface IUserRepository : IGeneralRepository<User>
    {

        bool IsExistsByPersonID(int personID);
        User IsExistsByUserName(string userName);

        bool IsExistsByUserNameExceptUserID(string userName, int userID);
    }
}

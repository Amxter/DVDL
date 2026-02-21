namespace BusinessDVLD
{
    public interface IUserServices : IGeneralServices <UserDTO>
    {
        bool IsExistsByPersonID(int personID);
        UserDTO GetByUserName(string username);
        bool IsExistsByUserNameExceptUserID(string userName, int userID);
        UserDTO GetByID(int dTOID);
        bool IsExistsByID(int dTOID);
    }

}

using DatabaseDVLD;
using System.Data;
using System.Runtime.CompilerServices;

namespace BusinessDVLD
{
    public class UserServices : IUserServices
    {
        IUserRepository _userRepository;



        public UserServices(IUserRepository repository)
        {

            _userRepository = repository;


        }
        public int Add(UserDTO dTO)
        {

            dTO.Password = PasswordHelper.HashPassword(dTO.Password);
            return _userRepository.Add(dTO.ToEntity());
        }
        public bool Delete(int userID) => _userRepository.Delete(userID);
        public bool IsExistsByID(int userID) => _userRepository.IsExistsByID(userID);
        public UserDTO GetByUserName(string username)
        {

            var entity = _userRepository.IsExistsByUserName(username);
            return entity == null ? null : entity.ToDTO();
        }
        public bool IsExistsByPersonID(int personID) => _userRepository.IsExistsByPersonID(personID);
        public bool IsExistsByUserNameExceptUserID(string username, int userID) => _userRepository.IsExistsByUserNameExceptUserID(username, userID);
        public DataTable GetAll() => _userRepository.GetAll();
        public UserDTO GetByID(int id)
        {
            var entity = _userRepository.GetByID(id);
            return entity == null ? null : entity.ToDTO();
        }

        public bool Update(UserDTO dTO)
        {


            dTO.Password = PasswordHelper.HashPassword(dTO.Password);
            return _userRepository.Update(dTO.ToEntity());
        }


    }
}



using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO
            {
                PersonID = user.PersonID,
                UserID = user.UserID,
                UserName = user.UserName,
                Password = user.PasswordHash,
                IsActive = user.IsActive 
            };
        }
        public static User ToEntity(this UserDTO userDTO)
        {
            return new User
            {
                PersonID = userDTO.PersonID,
                UserID = userDTO.UserID,
                UserName = userDTO.UserName,
                PasswordHash = userDTO.Password,
                IsActive = userDTO.IsActive
            };
        }

    }
}

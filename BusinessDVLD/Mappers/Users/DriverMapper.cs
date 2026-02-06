using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class DriverMapper
    {
       
        public static Driver ToEntity(this DriverDTO driverDTO)
        {

            return new Driver
            {
                PersonID = driverDTO.PersonID,
                DriverID = driverDTO.DriverID,
                CreatedByUserID = driverDTO.CreatedByUserID,
                CreatedDate = driverDTO.CreatedDate 
 
            };
        }

    }
}

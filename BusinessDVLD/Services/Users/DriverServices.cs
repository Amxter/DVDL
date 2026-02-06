using DatabaseDVLD;
using System.Data;

namespace BusinessDVLD
{
    public class DriverServices : IDriverServices
    {

        IDriverRepository _driverRepository;
        public DriverServices(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public int Add(DriverDTO driverDTO) => _driverRepository.Add(driverDTO.ToEntity());
        public DataTable GetAll () => _driverRepository.GetAll();
        public int IsExistByPersonID(int personID) => _driverRepository.IsExistByPersonID(personID);
    }
}

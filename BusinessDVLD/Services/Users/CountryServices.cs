using DatabaseDVLD;
using System.Data;

namespace BusinessDVLD
{
    public class CountryServices : ICountryServices
    {
        ICountryRepository _countryRepository;


        public CountryServices(ICountryRepository repository)
        {

            _countryRepository = repository;


        }


        public DataTable GetAll() => _countryRepository.GetAll();
        public string GetCountryNameByID(int country) => _countryRepository.GetCountryNameByID(country);
    }
}

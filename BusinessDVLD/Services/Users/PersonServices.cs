using DatabaseDVLD;
using System.Data;

namespace BusinessDVLD
{
    public class PersonServices : IPersonServices
    {
        readonly IPersonRepository _personRepository;
        public PersonServices(IPersonRepository repository)
        {
            _personRepository = repository;
        }
        public int Add(PersonDTO dTO) => _personRepository.Add(dTO.ToEntity());
        public bool Update(PersonDTO person) => _personRepository.Update(person.ToEntity());
        public bool Delete(int personID) => _personRepository.Delete(personID);
        public DataTable GetAll() => _personRepository.GetAll();
        public bool IsExistsByID(int personID) => _personRepository.IsExistsByID(personID);
        public bool IsExistsByNationalNo(string nationalNo) => _personRepository.IsExistsByNationalNo(nationalNo);
        public PersonDTO GetByID(int id) => _personRepository.GetByID(id)?.ToDTO();
        public PersonDTO GetByNationalNo(string nationalNo) => _personRepository.GetByNationalNo(nationalNo)?.ToDTO();
    }

}

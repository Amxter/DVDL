using DatabaseDVLD;
using System;
using System.Data;

namespace BusinessDVLD
{
    public class PersonServices : IPersonServices
    {
        IPersonRepository _personRepository;
        public PersonServices(IPersonRepository repository)
        {
            _personRepository = repository;
        }
        public int Add(PersonDTO dTO) => _personRepository.Add(dTO.ToEntity());
        public bool Delete(int personID) => _personRepository.Delete(personID);
        public bool IsExistsByID(int personID) => _personRepository.IsExistsByID(personID);
        public bool IsExistsNationalNo(string nationalNo) => _personRepository.IsExistsNationalNo(nationalNo);
        public DataTable GetAll() => _personRepository.GetAll();
        public PersonDTO GetByID(int id)
        {
            var entity = _personRepository.GetByID(id);
            return entity == null ? null : entity.ToDTO();
        }
        public PersonDTO GetByNationalNo(string id)
        {
            var entity = _personRepository.GetByNationalNo(id);
            return entity == null ? null : entity.ToDTO();
        }
        public bool Update(PersonDTO person) => _personRepository.Update(person.ToEntity());
    }

}

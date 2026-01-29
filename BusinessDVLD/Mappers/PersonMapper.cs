using DatabaseDVLD;
using System;
using System.Data;

namespace BusinessDVLD
{
    public static class PersonMapper
    {
        public static PersonDTO ToDTO(this Person person)
        {
            return new PersonDTO
            {
                PersonID = person.PersonID,
                NationalNo = person.NationalNo,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                ThirdName = person.ThirdName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                Gendor =  person.Gendor ,
                Address = person.Address,
                Phone = person.Phone,
                Email = person.Email,
                FullName = $"{person.FirstName} {person.SecondName} {person.ThirdName} {person.LastName}",
                Age = (short)(DateTime.Now.Year - person.DateOfBirth.Year) ,
                Nationality = person.NationalityCountryID,
                ImagePath = person.ImagePath
            };
        }
        public static Person ToEntity(this PersonDTO personDTO)
        {
           
            return new Person
            {
                PersonID = personDTO.PersonID,
                NationalNo = personDTO.NationalNo,
                FirstName = personDTO.FirstName,
                SecondName = personDTO.SecondName,
                ThirdName = personDTO.ThirdName,
                LastName = personDTO.LastName,
                DateOfBirth = personDTO.DateOfBirth,
                Gendor = personDTO.Gendor,
                Address = personDTO.Address,
                Phone = personDTO.Phone,
                Email = personDTO.Email,
                NationalityCountryID = personDTO.Nationality ,
                ImagePath = personDTO.ImagePath
            };
        }

    }
}

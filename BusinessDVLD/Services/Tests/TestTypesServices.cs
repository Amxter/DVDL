using DatabaseDVLD;
using System.Data;

namespace BusinessDVLD
{


    public class TestTypesServices : ITestTypesServices
    {
        ITestTypesRepository _manageTestTypes;
        public TestTypesServices(ITestTypesRepository testTypes)
        {
            _manageTestTypes = testTypes;
        }
        public DataTable GetAll() => _manageTestTypes.GetAll();
        public bool Upgrade(TestTypeDTO testDTO) => _manageTestTypes.Update(testDTO.ToEntity());
        public TestTypeDTO GetByID(int testTypeID) => _manageTestTypes.GetByID(testTypeID)?.ToDTO();
    }

    }

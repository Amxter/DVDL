using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class TestTypeMapper
    {
  

        public static TestTypeDTO ToDTO(this TestType application)
        {
            return new TestTypeDTO
            {
                TestTypeID = application.TestTypeID,
                TestTypeTitle = application.TestTypeTitle,
                TestTypeDescription = application.TestTypeDescription,
                TestTypeFees = application.TestTypeFees,
            };
        }


        public static TestType ToEntity(this TestTypeDTO application)
        {
            return new TestType
            {
                TestTypeID = application.TestTypeID,
                TestTypeTitle = application.TestTypeTitle,
                TestTypeDescription = application.TestTypeDescription,
                TestTypeFees = application.TestTypeFees,
            };
        }

    }
}

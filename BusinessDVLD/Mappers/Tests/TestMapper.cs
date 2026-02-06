using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class TestMapper

    {

        public static Test ToEntity(this TestDTO testDTO)
        {
            return new Test
            {
                TestID = testDTO.TestID,
                TestAppointmentID = testDTO.TestAppointmentID,
                TestResult = testDTO.TestResult,
                Notes = testDTO.Notes,
                CreatedByUserID = testDTO.CreatedByUserID
            };
        }

        public static TestDTO ToDTO(this Test testDTO)
        {
            return new TestDTO
            {
                TestID = testDTO.TestID,
                TestAppointmentID = testDTO.TestAppointmentID,
                TestResult = testDTO.TestResult,
                Notes = testDTO.Notes,
                CreatedByUserID = testDTO.CreatedByUserID
            };
        }
    }
}


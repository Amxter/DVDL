using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class ManageTestTypesMapper

    {

        public static Test ToEntity(this TestDTO testDTO)
        {
            return new Test
            {
                ID = testDTO.ID,
                Title = testDTO.Title,
                Description = testDTO.Description,
                Fees = testDTO.Fees,
            };
        }
    }
}

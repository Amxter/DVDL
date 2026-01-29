using DatabaseDVLD;

namespace BusinessDVLD
{
    public static partial class ApplicationTypesMapper
    {

        public static ApplicationTypes ToEntity(this ApplicationTypesDTO application)
        {
            return new ApplicationTypes
            {
                ID = application.ID,
                Title = application.Title,
                Fees = application.Fees,
            };
        }

        public static ApplicationTypesDTO ToDTO(this ApplicationTypes application)
        {
            return new ApplicationTypesDTO
            {
                ID = application.ID,
                Title = application.Title,
                Fees = application.Fees,
            };
        }
    }
}

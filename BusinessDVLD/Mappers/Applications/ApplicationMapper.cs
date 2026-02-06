using DatabaseDVLD;

namespace BusinessDVLD
{
    public static partial class ApplicationMapper
    {

        public static Application  ToEntity(this ApplicationDTO application)
        {
            return new Application
            {
                ApplicationID = application.ApplicationID,
                ApplicantPersonID = application.ApplicantPersonID,
                ApplicationTypeID = application.ApplicationTypeID,
                ApplicationDate = application.ApplicationDate,
                ApplicationStatus = application.ApplicationStatus,
                LastStatusDate = application.LastStatusDate,
                PaidFees = application.PaidFees,
                CreatedByUserID = application.CreatedByUserID
                
            };
        }

        public static ApplicationDTO ToDTO(this Application application)
        {
            return new ApplicationDTO
            {
                ApplicationID = application.ApplicationID,
                ApplicantPersonID = application.ApplicantPersonID,
                ApplicationTypeID = application.ApplicationTypeID,
                ApplicationDate = application.ApplicationDate,
                ApplicationStatus = application.ApplicationStatus,
                LastStatusDate = application.LastStatusDate,
                PaidFees = application.PaidFees,
                CreatedByUserID = application.CreatedByUserID

            };
        }
    }
}

using DatabaseDVLD;

namespace BusinessDVLD
{
 
        public static class LDLApplicationTypesMapper
        {

            public static LDLApplication ToEntity(this LDLApplicationDTO application)
            {
                return new LDLApplication
                {
                    LocalDrivingLicenseApplicationID = application.LocalDrivingLicenseApplicationID,
                    LicenseClassID = application.LicenseClassID,
                    Application = application.Application.ToEntity(),
                };
            }

        public static LDLApplicationDTO ToDTO(this LDLApplication application)
        {
            return new LDLApplicationDTO
            {
                LocalDrivingLicenseApplicationID = application.LocalDrivingLicenseApplicationID,
                LicenseClassID = application.LicenseClassID,
                Application = application.Application.ToDTO(),
            };
        }
    }
}

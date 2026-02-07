using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class InternationalLicenseDTOMapper
    {
        public static InternationalLicenseDTO ToDTO(this InternationalLicense internationalLicense)
        {
            if (internationalLicense == null) return null;
            return new InternationalLicenseDTO
            {
                InternationalLicenseID = internationalLicense.InternationalLicenseID,
                ApplicationID = internationalLicense.ApplicationID,
                DriverID = internationalLicense.DriverID,
                IssuedUsingLocalLicenseID = internationalLicense.IssuedUsingLocalLicenseID,
                IssueDate = internationalLicense.IssueDate,
                ExpirationDate = internationalLicense.ExpirationDate,
                IsActive = internationalLicense.IsActive,
                CreatedByUserID = internationalLicense.CreatedByUserID
            };
        }

        public static InternationalLicense ToEntity(this InternationalLicenseDTO internationalLicenseDTO)
        {
            if (internationalLicenseDTO == null) return null;
            return new InternationalLicense
            {
                InternationalLicenseID = internationalLicenseDTO.InternationalLicenseID,
                ApplicationID = internationalLicenseDTO.ApplicationID,
                DriverID = internationalLicenseDTO.DriverID,
                IssuedUsingLocalLicenseID = internationalLicenseDTO.IssuedUsingLocalLicenseID,
                IssueDate = internationalLicenseDTO.IssueDate,
                ExpirationDate = internationalLicenseDTO.ExpirationDate,
                IsActive = internationalLicenseDTO.IsActive,
                CreatedByUserID = internationalLicenseDTO.CreatedByUserID
            };
        }

    }

}

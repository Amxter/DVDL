using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class LicenseMapper
    {

        public static License ToEntity(this LicenseDTO license)
        {
            return new License
            {
                DriverID = license.DriverID,
                ApplicationID = license.ApplicationID,
                LicenseID = license.LicenseID,
                IssueDate = license.IssueDate,
                ExpirationDate = license.ExpirationDate,
                LicenseClass = license.LicenseClass,
                Notes = license.Notes,
                PaidFees = license.PaidFees,
                IsActive = license.IsActive,
                IssueReason = license.IssueReason,
                CreatedByUserID = license.CreatedByUserID

            };
        }
        public static LicenseDTO ToDTO(this License license)
        {
            return new LicenseDTO
            {
                DriverID = license.DriverID,
                ApplicationID = license.ApplicationID,
                LicenseID = license.LicenseID,
                IssueDate = license.IssueDate,
                ExpirationDate = license.ExpirationDate,
                LicenseClass = license.LicenseClass,
                Notes = license.Notes,
                PaidFees = license.PaidFees,
                IsActive = license.IsActive,
                IssueReason = license.IssueReason,
                CreatedByUserID = license.CreatedByUserID

            };
        }
    }

    }

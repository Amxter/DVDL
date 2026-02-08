using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class DetainedLicenseMapper
    {
        public static DetainedLicenseDTO ToDTO(this DetainedLicense detainedLicense) => new DetainedLicenseDTO
        {
            DetainID = detainedLicense.DetainID,
            LicenseID = detainedLicense.LicenseID,
            DetainDate = detainedLicense.DetainDate,
            FineFees = detainedLicense.FineFees,
            CreatedByUserID = detainedLicense.CreatedByUserID,
            IsReleased = detainedLicense.IsReleased,
            ReleaseDate = detainedLicense.ReleaseDate,
            ReleasedByUserID = detainedLicense.ReleasedByUserID,
            ReleaseApplicationID = detainedLicense.ReleaseApplicationID
        };
        public static DetainedLicense ToEntity(this DetainedLicenseDTO dto) => new DetainedLicense
        {
            DetainID = dto.DetainID,
            LicenseID = dto.LicenseID,
            DetainDate = dto.DetainDate,
            FineFees = dto.FineFees,
            CreatedByUserID = dto.CreatedByUserID,
            IsReleased = dto.IsReleased,
            ReleaseDate = dto.ReleaseDate,
            ReleasedByUserID = dto.ReleasedByUserID,
            ReleaseApplicationID = dto.ReleaseApplicationID
        };
    }
}

using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class LicenseClassMapper
    {

        public static LicenseClassDTO ToDTO(this LicenseClass licenseClass)
        {
            return new LicenseClassDTO
            {
                LicenseClassID = licenseClass.LicenseClassID,
                ClassName = licenseClass.ClassName,
                ClassDescription = licenseClass.ClassDescription,
                MinimumAllowedAge = licenseClass.MinimumAllowedAge,
                DefaultValidityLength = licenseClass.DefaultValidityLength,
                ClassFees = licenseClass.ClassFees,
            };
        }
    }
}

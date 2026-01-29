using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class ServiceFactory
    {
        public static IPersonServices CreatePersonServices()
        {
            return new PersonServices(new PersonRepository());
        }

        public static ICountryServices CreateCountryServices()
        {
            return new CountryServices(new CountryRepository());
        }

        public static IUserServices CreateUserServices()
        {
            return new UserServices(new UserRepository());
        }


        public static IApplicationTypesServices CreateApplicationTypesServices()
        {
            return new ApplicationTypesServices(new ApplicationTypesRepository());

        }

        //ManageTestTypesServices
        public static IManageTestTypesServices CreateManageTestTypesServices()
        {
            return new ManageTestTypesServices(new ManageTestTypesRepository());
        }

        public static ILDLApplicationServices CreateLDLApplicationServices()
        {
            return new LDlApplicationServices( new LDLApplicationRepository() );
        }

        public static ILicenseClassServices CreateLicenseClassServices()
        {
            return new LicenseClassServices( new LicenseClassRepository() );
        }

        public static IApplicationServices CreateApplicationServices()
        {
            return new ApplicationServices( new ApplicationRepository() );
        }
    }
}

using BusinessDVLD;
using DatabaseDVLD;
using DrivingVehicleLicenseDepartment;
using DrivingVehicleLicenseDepartment.Applications.ApplicationTypes;
using DrivingVehicleLicenseDepartment.Applications.Local_Driving_License;
using DrivingVehicleLicenseDepartment.Applications.ManageTestTypes;
using DrivingVehicleLicenseDepartment.Applications.Renew_Local_Driving_license;
using DrivingVehicleLicenseDepartment.Applications.ReplaceLostOrDamagedLicense;
using DrivingVehicleLicenseDepartment.Applications.Release_Detained_License;
using DrivingVehicleLicenseDepartment.Applications.Tests;
using DrivingVehicleLicenseDepartment.Drivers;
using DrivingVehicleLicenseDepartment.Licenses;
using DrivingVehicleLicenseDepartment.Licenses.International;
using DrivingVehicleLicenseDepartment.Licenses.Local_Licenses;
using DrivingVehicleLicenseDepartment.Login;
using DrivingVehicleLicenseDepartment.Users;
using System;
using Unity;




using DVLD = DrivingVehicleLicenseDepartment ;


namespace PresentationDVLD
{
    internal static class Program
    {
        public static IUnityContainer Container { get; private set; }

        
        [STAThread]

        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            Container = new UnityContainer();

            // Repositories
            Container.RegisterType<IPersonRepository, PersonRepository>();
            Container.RegisterType<IUserRepository, UserRepository>();
            Container.RegisterType<IApplicationRepository, ApplicationRepository>();
            Container.RegisterType<IApplicationTypesRepository, ApplicationTypesRepository>();
            Container.RegisterType<ICountryRepository, CountryRepository>();
            Container.RegisterType<ILicenseClassRepository, LicenseClassRepository>();
            Container.RegisterType<ITestTypesRepository, TestTypesRepository>();
            Container.RegisterType<ILDLApplicationRepository, LDLApplicationRepository>();
            Container.RegisterType<ILogger, EventLogs>();
            Container.RegisterType<ITestAppointmentRepository, TestAppointmentRepository>();
            Container.RegisterType<ITestRepository, TestRepository>();
            Container.RegisterType<ILicensesRepository, LicensesRepository>();
            Container.RegisterType<IDriverRepository, DriverRepository>();
            Container.RegisterType<IInternationalLicensesRepository , InternationalLicensesRepository>();
            Container.RegisterType<IDetainedLicenseRepository, DetainedLicenseRepository>();



            // Services
            Container.RegisterType<IPersonServices, PersonServices>();
            Container.RegisterType<IUserServices, UserServices>();
            Container.RegisterType<IApplicationServices, ApplicationServices>();
            Container.RegisterType<IApplicationTypesServices, ApplicationTypesServices>();
            Container.RegisterType<ICountryServices, CountryServices>();
            Container.RegisterType<ILicenseClassServices, LicenseClassServices>();
            Container.RegisterType<ITestTypesServices, TestTypesServices>();
            Container.RegisterType<ILDLApplicationServices, LDlApplicationServices>();
            Container.RegisterType<ITestAppointmentServices, TestAppointmentServices>();
            Container.RegisterType<ITestServices,  TestServices >();
            Container.RegisterType<ILicenseService, LicenseService>();
            Container.RegisterType<IDriverServices, DriverServices>();
            Container.RegisterType<IInternationalLicenseService, InternationalLicenseService>();
            Container.RegisterType<IDetainedLicenseServices, DetainedLicenseServices>();



            // Forms
            Container.RegisterType<LoginScreen>();
            Container.RegisterType<PeopleManagement>(); 
            Container.RegisterType<UpdateApplicationTypes>();
            Container.RegisterType<DVLD.ApplicationTypes>();
            Container.RegisterType<AddUpdateLocalDrivingLicenseApplication>();
            Container.RegisterType<ListLocalDrivingLicenseApplication>();
            Container.RegisterType<ManageTestTypes>();
            Container.RegisterType<AddAndUpdatePerson>();
            Container.RegisterType<PeopleManagement>();
            Container.RegisterType<AddUpdateUser>();
            Container.RegisterType<ChangePassword>();
            Container.RegisterType<ListUsers>();
            Container.RegisterType<ListTestsAppointments>();
            Container.RegisterType<ScheduleTestForm>();
            Container.RegisterType<TakeTest>();
            Container.RegisterType<LDLApplicationInfo>();
            Container.RegisterType<ShowPersonLicenseHistory>(); 
            Container.RegisterType<ShowLicensesInfo>(); 
            Container.RegisterType<ListDrivers>(); 
            Container.RegisterType<InternationalLicenseApplication>(); 
            Container.RegisterType<ShowInternationalLicense>(); 
            Container.RegisterType<ListInternationalLicenses>(); 
            Container.RegisterType<RenewLocalDrivingLicenseApplication>(); 
            Container.RegisterType<ReplaceLostOrDamagedLicense>(); 
            Container.RegisterType<DVLD.Applications.Release_Detained_License.DetainLicenseApplication>();
            Container.RegisterType<ReleaseDetainedLicenseApplication>(); 
            Container.RegisterType<ListDetainedLicenses>();
            Container.RegisterType<ShowDetailsPerson>();


            var login = Container.Resolve<LoginScreen>();
            System.Windows.Forms.Application.Run(login);
            

        }
    }

}

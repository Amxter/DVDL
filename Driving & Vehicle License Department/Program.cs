using BusinessDVLD;
using DatabaseDVLD;
using Driving___Vehicle_License_Department;
using Driving___Vehicle_License_Department.Applications.ApplicationTypes;
using Driving___Vehicle_License_Department.Applications.Local_Driving_License;
using Driving___Vehicle_License_Department.Applications.ManageTestTypes;
using Driving___Vehicle_License_Department.Applications.Renew_Local_Driving_license;
using Driving___Vehicle_License_Department.Applications.ReplaceLostOrDamagedLicense;
using Driving___Vehicle_License_Department.Applications.Tests;
using Driving___Vehicle_License_Department.Drivers;
using Driving___Vehicle_License_Department.Licenses;
using Driving___Vehicle_License_Department.Licenses.International;
using Driving___Vehicle_License_Department.Licenses.Local_Licenses;
using Driving___Vehicle_License_Department.Login;
using Driving___Vehicle_License_Department.People;
using Driving___Vehicle_License_Department.People.User_Controls;
using Driving___Vehicle_License_Department.Users;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

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
            Container.RegisterType<ILogger, FileLogger>();
            Container.RegisterType<ITestAppointmentRepository, TestAppointmentRepository>();
            Container.RegisterType<ITestRepository, TestRepository>();
            Container.RegisterType< ILicensesRepository, LicensesRepository>();
            Container.RegisterType<IDriverRepository, DriverRepository>();
            Container.RegisterType<IInternationalLicensesRepository , InternationalLicensesRepository>();




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




            // Forms
            Container.RegisterType<LoginScreen>();
            Container.RegisterType<PeopleManagement>(); 
            Container.RegisterType<UpdateApplicationTypes>();
            Container.RegisterType<Driving___Vehicle_License_Department.ApplicationTypes>();
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

            var login = Container.Resolve<LoginScreen>();
            System.Windows.Forms.Application.Run(login);
            

        }
    }

}

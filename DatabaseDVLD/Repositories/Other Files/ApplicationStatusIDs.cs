namespace DatabaseDVLD
{
    public class ApplicationStatusIDs
    {
        public static readonly int NewStatus = 1;
        public static readonly int CanceledStatus = 2;
        public static readonly int CompletedStatus = 3;
    }

    public class ApplicationTypesIDs
    {
        public static readonly int NewLocalDrivingLicenseService = 1;
        public static readonly int RenewalLocalDrivingLicenseService = 2;
        public static readonly int ReplacementLocalDrivingLicenseService = 3;
        public static readonly int NewInternationalDrivingLicenseService = 4;
        public static readonly int RenewalInternationalDrivingLicenseService = 5;
        public static readonly int ReplacementInternationalDrivingLicenseService = 6;
    }
}
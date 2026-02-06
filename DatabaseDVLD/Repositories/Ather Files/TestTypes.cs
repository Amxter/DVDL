namespace DatabaseDVLD
{
    public class TestTypes
    {
        public static readonly int VisionTestID =1  ;
        public static readonly int WrittenTestID = 2;
        public static readonly int PracticalTestID = 3;
    }

    public class IssueReason
    {
        
        public static readonly int FirstTime = 1;
        public static readonly int Renew = 2;
        public static readonly int ReplacementForDamaged = 3;
        public static readonly int ReplacementForLost = 4;

    }

    public class ApplicationStatus
    {
        public static readonly int NewStatus = 1;
        public static readonly int CanceledStatus = 2;
        public static readonly int CompletedStatus = 3;
    }
}
using DatabaseDVLD;

namespace BusinessDVLD
{
    public static class TestAppointmentMapper
    {

        public static TestAppointment ToEntity(this TestAppointmentDTO application)
        {
            return new TestAppointment
            {
                TestAppointmentID = application.TestAppointmentID,
                LocalDrivingLicenseApplicationID = application.LocalDrivingLicenseApplicationID,
                TestTypeID = application.TestTypeID,
                AppointmentDate = application.AppointmentDate,
                PaidFees = application.PaidFees,
                CreatedByUserID = application.CreatedByUserID,
                IsLocked = application.IsLocked,
                RetakeTestApplicationID = application.RetakeTestApplicationID,

            };
        }

        public static TestAppointmentDTO ToDTO(this TestAppointment application)
        {
            return new TestAppointmentDTO
            {
                TestAppointmentID = application.TestAppointmentID,
                LocalDrivingLicenseApplicationID = application.LocalDrivingLicenseApplicationID,
                TestTypeID = application.TestTypeID,
                AppointmentDate = application.AppointmentDate,
                PaidFees = application.PaidFees,
                CreatedByUserID = application.CreatedByUserID,
                IsLocked = application.IsLocked,
                RetakeTestApplicationID = application.RetakeTestApplicationID,

            };




        }
    }
}

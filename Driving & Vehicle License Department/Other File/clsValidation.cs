using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DrivingVehicleLicenseDepartment
{
   

    public static class clsValidations
    {
        // =======================
        // Validate Email
        // =======================
        public static bool ValidateEmail(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return false;

            string pattern =
                @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+" +
                @"@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            return Regex.IsMatch(emailAddress.Trim(), pattern);
        }

        // =======================
        // Validate Integer
        // =======================
        public static bool ValidateInteger(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return false;

            return int.TryParse(number.Trim(), out _);
        }

        // =======================
        // Validate Float / Double
        // =======================
        public static bool ValidateFloat(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return false;

            return double.TryParse(number.Trim(), out _);
        }

        // =======================
        // Is Number (Integer OR Float)
        // =======================
        public static bool IsNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return false;

            return double.TryParse(number.Trim(), out _);
        }
    }

}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingVehicleLicenseDepartment.Other_File
{
    internal class WindowsRegistry
    {

        public static void SavePasswordAndUserNameInWindowsRegistry(bool isSave , string username , string password )
        {

            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVDLApplication";


            if (isSave)
            {
                try
                {
                    // Write the value to the Registry
                    Registry.SetValue(keyPath, "UserName", username, RegistryValueKind.String);
                    Registry.SetValue(keyPath, "Password", password, RegistryValueKind.String);

                }
                catch
                {

                }

            }
            else
            {
                try
                {
                    // Write the value to the Registry
                    Registry.SetValue(keyPath, "UserName", "", RegistryValueKind.String);
                    Registry.SetValue(keyPath, "Password", "", RegistryValueKind.String);

                }
                catch
                {

                }

            }




        }
        public static void LoadUserNameAndPasswordFromWindowsRegistry(ref string username, ref string password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVDLApplication";



            try
            {

                  username = Registry.GetValue(keyPath, "UserName", null) as string;
                  password = Registry.GetValue(keyPath, "Password", null) as string;



            }
            catch
            {

            }
        }
    }
}

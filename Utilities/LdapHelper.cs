using System.DirectoryServices;
using VisitorManagementSystem.Models.Entities;

namespace VisitorManagementSystem.Utilities
{
    public class LdapHelper
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static bool IsValidDirectoryServiceUser(string userName, string password)
        {
            try
            {
                string path = "LDAP://Company.local:389/DC=Company,DC=local";

                DirectoryEntry entry = new DirectoryEntry(path, "Company.local\\" + userName, password);
                Object native = entry.NativeObject;

                UserName = userName;
                Password = password;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

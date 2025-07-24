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
                string path = "LDAP://sirketismi.local:389/DC=sirketismi,DC=local";

                DirectoryEntry entry = new DirectoryEntry(path, "sirketismi.local\\" + userName, password);
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

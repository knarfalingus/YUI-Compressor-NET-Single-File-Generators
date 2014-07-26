using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUICompressorTool
{
    /// <summary>
    /// stuff for the windows registry
    /// </summary>
    public static class ToolRegistration
    {

        public const string CSharpCategoryString = "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}";
        public const string VBCategoryString = "{164B10B9-B200-11D0-8C61-00A0C91E29D5}";
 
        public static Guid CSharpCategory = new Guid("{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}");
        public static Guid VBCategory = new Guid("{164B10B9-B200-11D0-8C61-00A0C91E29D5}");

        private const string KeyFormat32 = @"SOFTWARE\Microsoft\VisualStudio\{0}\Generators\{1}\{2}";
        private const string KeyFormat64 = @"SOFTWARE\Wow6432Node\Microsoft\VisualStudio\{0}\Generators\{1}\{2}";


        public static string RegSubKey(Version vsVersion, Guid categoryGuid, string CustomToolName)
        {
            string subKey;
            if (System.Environment.Is64BitOperatingSystem)
                subKey = String.Format(KeyFormat64, vsVersion, categoryGuid.ToString("B"), CustomToolName);
            else
                subKey = String.Format(KeyFormat32, vsVersion, categoryGuid.ToString("B"), CustomToolName);
            return subKey;
        }

        public static void Register(string CustomToolName, string ToolDescription, Guid CustomToolGuid, Version vsVersion)
        {
            Register(CustomToolName, ToolDescription, CustomToolGuid, vsVersion, VBCategory);
            Register(CustomToolName, ToolDescription, CustomToolGuid, vsVersion, CSharpCategory);
        }

        public static void Register(string CustomToolName, string ToolDescription, Guid CustomToolGuid, Version vsVersion, Guid categoryGuid)
        {
            string subKey = RegSubKey(vsVersion, categoryGuid, CustomToolName);

            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(subKey))
            {
                key.SetValue("", ToolDescription);
                key.SetValue("CLSID", CustomToolGuid.ToString("B"));
                key.SetValue("GeneratesDesignTimeSource", 1);
            }
        }

        public static void Unregister(string CustomToolName, Version vsVersion)
        {
            Unregister(CustomToolName, vsVersion, VBCategory);
            Unregister(CustomToolName, vsVersion, CSharpCategory);
        }

        public static void Unregister(string CustomToolName, Version vsVersion, Guid categoryGuid)
        {
            string subKey = RegSubKey(vsVersion, categoryGuid, CustomToolName);

            Registry.LocalMachine.DeleteSubKey(subKey, false);
        }

    }
}

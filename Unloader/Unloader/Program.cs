using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;

namespace Unloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "UNLOCKER";
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Starting unlocking your PC...");
                Thread.Sleep(500);
                RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);
                
                objRegistryKey.SetValue("DisableTaskMgr", "0");

                RegistryKey objRegistryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\\", true);
                objRegistryKey.SetValue("DisableCAD", "0");
  
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\\", true);
                File.Delete("C:\\Windows\\n0tepad.exe");
                registryKey.SetValue("Shell", "explorer.exe");
                Thread.Sleep(500);
                Console.WriteLine("Restart your PC!");
            }
            catch
            {
                //Ignore . . .
            }
        }
    }
}

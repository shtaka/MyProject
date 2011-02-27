using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace SPOProj
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           label1.Text = VideoCard();
        }
        private string VideoCard()
        {
            ManagementScope sc = new ManagementScope(@"\\.\root\cimv2", null);
            ManagementPath ph = new ManagementPath(@"Win32_VideoController");
            ManagementClass mc = new ManagementClass(sc, ph, null);
            string Vid = "null";
            foreach (ManagementObject ss in mc.GetInstances())
            {
                try
                {
                     Vid ="Name : " + ss.GetPropertyValue("Name").ToString() + '\n'
                   + "VideoProcessor : " + ss.GetPropertyValue("VideoProcessor").ToString() + '\n'
                   + "AdapterRAM : " + ss.GetPropertyValue("AdapterRAM").ToString() + '\n'
                   + "VideoModeDescription : " + ss.GetPropertyValue("VideoModeDescription").ToString() + '\n'
                   + "CurrentRefreshRate : " + ss.GetPropertyValue("CurrentRefreshRate").ToString();
                }
                catch (NullReferenceException ex)
                {
                }
            }
            return Vid;
        }
        private void button2_Click(object sender, EventArgs e)
        {
           label1.Text =  NetworkConnect();
        }
        private string NetworkConnect()
        {
            ManagementClass managementClass = new ManagementClass("Win32_NetworkConnection");
            ManagementObjectCollection managementObj = managementClass.GetInstances();
            string ret = "Null";
            foreach (ManagementObject mo in managementObj)
            {

               ret = "AccessMask: " + mo["AccessMask"].ToString() + '\n' +
               "AccessMask: " + mo["AccessMask"] + '\n' +
                "Caption: " + mo["Caption"] + '\n' +
                "Comment: " + mo["Comment"] + '\n' +
                "ConnectionState: " + mo["ConnectionState"] + '\n' +
                "ConnectionType: " + mo["ConnectionType"] + '\n' +
                "Description: " + mo["Description"] + '\n' +
                "DisplayType: " + mo["DisplayType"] + '\n' +
                "InstallDate: " + mo["InstallDate"] + '\n' +
                "LocalName: " + mo["LocalName"] + '\n' +
                "Name: " + mo["Name"] + '\n' +
                "Persistent: " + mo["Persistent"] + '\n' +
                "ProviderName: " + mo["ProviderName"] + '\n' +
                "RemoteName: " + mo["RemoteName"] + '\n' +
                "RemotePath: " + mo["RemotePath"] + '\n' +
                "ResourceType: " + mo["ResourceType"] + '\n' +
                "Status: " + mo["Status"] + '\n' +
                "UserName: " + mo["UserName"];
            }
            return ret;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = Ident();
        }
        private string Ident()
        {
            WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM Win32_ComputerSystemProduct");
            ManagementObjectSearcher find = new ManagementObjectSearcher(query);
            string ret = "null";
            foreach (ManagementObject mo in find.Get())
            {
                ret = "Description." + mo["Description"] + '\n' +
                "Identifying number (usually serial number)." + mo["IdentifyingNumber"] + '\n' +
                "Commonly used product name." + mo["Name"] + '\n' +
                "Universally Unique Identifier of  product." + mo["UUID"] + '\n' +
                "Vendor of product." + mo["Vendor"];
            }
            return ret;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = Boot();
        }
        private string Boot()
        {
            string ret = "null";
            WqlObjectQuery query = new WqlObjectQuery(
                "SELECT * FROM Win32_BootConfiguration");
            ManagementObjectSearcher find =
                new ManagementObjectSearcher(query);
            foreach (ManagementObject mo in find.Get())
            {
                ret = "Boot directory with files required for booting." + mo["BootDirectory"] + '\n' +
                "Description." + mo["Description"] + '\n' +
                "Directory with temporary files for booting." + mo["ScratchDirectory"] + '\n' +
                "Directory with temporary files." + mo["TempDirectory"];
            }
            return ret;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label1.Text = CompSys();
        }
        private string CompSys()
        {
            string ret="null";

            WqlObjectQuery query = new WqlObjectQuery("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectSearcher find = new ManagementObjectSearcher(query);
            foreach (ManagementObject mo in find.Get())
            {
                ret = "Computer belongs to domain " + mo["Domain"] + '\n' +
                "Computer manufacturer." + mo["Manufacturer"] + '\n' +
                "Model name given by manufacturer " + mo["Model"];
            }

            return ret;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label1.Text = CompSys2();
        }
        private string CompSys2()
        {
            string ret = "Null";
            string[] Roles = {
								 "Standalone Workstation", // 0
								 "Member Workstation",  // 1
								 "Standalone Server",  // 2
								 "Member Server",   // 3
								 "Backup Domain Controller", // 4
								 "Primary Domain Controller" // 5
							 };

            WqlObjectQuery query = new WqlObjectQuery(
                "SELECT * FROM Win32_ComputerSystem");
            ManagementObjectSearcher find =
                new ManagementObjectSearcher(query);
            foreach (ManagementObject mo in find.Get())
            {
                ret =Roles[Convert.ToInt32(mo["DomainRole"])];
            }
            return ret;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label1.Text = Desktop();
        }
        public string Desktop()
        {
            string ret = "Null";
            // Получить настройки рабочего стола
            WqlObjectQuery query = new WqlObjectQuery(
                "SELECT * FROM Win32_Desktop WHERE Name = '.Default'");
            ManagementObjectSearcher find =
                new ManagementObjectSearcher(query);
            foreach (ManagementObject mo in find.Get())
            {
                // Значения могут быть изменены 
                // в реестре "HKEY_CURRENT_USER\Control Panel\Desktop"
                ret = "Width of window borders." +  mo["BorderWidth"] + '\n' +
                "ALT+TAB task switching allowed." + mo["CoolSwitch"] + '\n' +
                // Значения в мс
                "Lenght of time between cursor blincks. " + mo["CursorBlinkRate"] + '\n' +
                "Show content of windows when are draged." + mo["DragFullWindows"] + '\n' +
                "Grid settings for dragging windows." + mo["GridGranularity"] + '\n' +
                "Grid settings for icon spacing. " + mo["IconSpacing"] + '\n' +
                "Font used for the names of icons." + mo["IconTitleFaceName"] + '\n' +
                "Icon ront size. " + mo["IconTitleSize"] + '\n' +
                "Wrapping of icon title." + mo["IconTitleWrap"] + '\n' +
               "Name of the desktop profile." + mo["Name"] + '\n' +
                "Screen saver is active." + mo["ScreenSaverActive"] + '\n' +
               "Name of the screen saver executable." + mo["ScreenSaverExecutable"] + '\n' +
                "Is screen saver protected with password." + mo["ScreenSaverSecure"] + '\n' +
                "Time to pass to activate screen saver." + mo["ScreenSaverTimeout"] + '\n' +
                "File name for desktop background." + mo["Wallpaper"] + '\n' +
                "Wallpaper fills entire screen." + mo["WallpaperStretched"] + '\n' +
                "Wallpaper is tiled." + mo["WallpaperTiled"];
            }
            return ret;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            label1.Text = DiskParts();
        }
        private string DiskParts()
        {
            string ret = "Null";
            WqlObjectQuery query = new WqlObjectQuery(
                "Select * from Win32_DiskPartition");
            ManagementObjectSearcher find =
                new ManagementObjectSearcher(query);
            foreach (ManagementObject mo in find.Get())
            {
                ret = "Block size." + mo["BlockSize"] + " Bytes" + '\n' +
                "Partition is labeled as bootable. " + mo["Bootable"] + '\n' +
                "Boot partition active. " + mo["BootPartition"] + '\n' +
                "Caption.." + mo["Caption"] + '\n' +
                "Description." + mo["Description"] + '\n' +
                "Unique identification of partition.." + mo["DeviceID"] + '\n' +
                "Index number of the disk with that partition." + mo["DiskIndex"] + '\n' +
                "Detailed description of error in LastErrorCode." + mo["ErrorDescription"] + '\n' +
                "Type of error detection and correction." + mo["ErrorMethodology"] + '\n' +
                "Hidden sectors in partition." + mo["HiddenSectors"] + '\n' +
                "Index number of the partition." + mo["Index"] + '\n' +
                "Last error by device." + mo["LastErrorCode"] + '\n' +
                "Total number of consecutive blocks." + mo["NumberOfBlocks"] + '\n' +
                "Partition labeled as primary." + mo["PrimaryPartition"] + '\n' +
                "Free description of media purpose. " + mo["Purpose"] + '\n' +
                "Total size of partition." + mo["Size"] + " bytes" + '\n' +
                "Starting offset of the partition " + mo["StartingOffset"] + '\n' +
                "Status." + mo["Status"] + '\n' +
                "Type of the partition." + mo["Type"];
            }
            return ret;
        }
    }

        
}

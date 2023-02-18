using System.Diagnostics;
using System.Drawing;
using System.Security;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace TrayLauncher11
{

    public partial class Form1 : Form
    {
        private IList<ToolStripMenuItem> AppsOptions = new List<ToolStripMenuItem>();

        private IList<ToolStripMenuItem> UtilitiesOptions = new List<ToolStripMenuItem>();

        private IList<ToolStripMenuItem> GamesOptions = new List<ToolStripMenuItem>();


        private NotifyIcon trayicon;

        public void GenerateGamesIcons()
        {
            Icon icon = new Icon(@"icons\G.ico");

            var contextStrip = new ContextMenuStrip();

            foreach (ToolStripMenuItem item in GamesOptions)
            {
                contextStrip.Items.Add(item);
            }


            trayicon = new NotifyIcon()
            {
                Icon = icon,
                ContextMenuStrip = contextStrip,
                Visible = true
            };
        }
        public void GenerateAppsIcons()
        {
            Icon icon = new Icon(@"icons\A.ico");

            var contextStrip = new ContextMenuStrip();

            foreach (ToolStripMenuItem item in AppsOptions)
            {
                contextStrip.Items.Add(item);
            }


            trayicon = new NotifyIcon()
            {
                Icon = icon,
                ContextMenuStrip = contextStrip,
                Visible = true
            };
        }

        public void GenerateUtilityIcons()
        {
            Icon icon = new Icon(@"icons\U.ico");

            var contextStrip = new ContextMenuStrip();

            foreach (ToolStripMenuItem item in UtilitiesOptions)
            {
                contextStrip.Items.Add(item);
            }


            trayicon = new NotifyIcon()
            {
                Icon = icon,
                ContextMenuStrip = contextStrip,
                Visible = true
            };
        }

        private void LaunchDIR(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", (sender as ToolStripMenuItem).Name);
        }

        private void Exit(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void checkGamesConfigs()
        {
            string[] appLines;
            try
            {
                appLines = File.ReadAllLines(@"games.csv");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"games.csv", @$"Steam,C:\Program Files (x86)\Steam\steam.exe");
                appLines = new string[] { @$"Steam,C:\Program Files (x86)\Steam\steam.exe" };
            }


            foreach (string line in appLines)
            {
                var lineSplit = line.Split(",");
                var item = new ToolStripMenuItem(lineSplit[0], null, new EventHandler(LaunchDIR), lineSplit[1]);
                GamesOptions.Add(item);
            }

            var exit = new ToolStripMenuItem("Exit", null, new EventHandler(Exit), "Exit");
            GamesOptions.Add(exit);
        }

        private void checkAppConfigs()
        {
            string[] appLines;
            try
            {
                appLines = File.ReadAllLines(@"apps.csv");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"apps.csv", @$"Desktop,C:\users\{Environment.UserName}\Desktop");
                appLines = new string[] { @$"Desktop,C:\users\{Environment.UserName}\Desktop"};
            }


            foreach (string line in appLines)
            {
                var lineSplit = line.Split(",");
                var item = new ToolStripMenuItem(lineSplit[0], null, new EventHandler(LaunchDIR), lineSplit[1]);
                AppsOptions.Add(item);
            }

            var exit = new ToolStripMenuItem("Exit", null, new EventHandler(Exit), "Exit");
            AppsOptions.Add(exit);
        }

        private void checkUtilitiesConfigs()
        {
            string[] appLines;
            try
            {
                appLines = File.ReadAllLines(@"utilities.csv");
            }
            catch (Exception ex)
            {
                string[] lines =  {@"Chrome,C:\Program Files\Google\Chrome\Application\chrome.exe",
                    @"Telegram,C:\Users\xboxa\AppData\Roaming\Telegram Desktop\Telegram.exe"};

                File.WriteAllLines(@"utilities.csv", lines);
                appLines = new string[] {@"Chrome,C:\Program Files\Google\Chrome\Application\chrome.exe",
                    @"Telegram,C:\Users\xboxa\AppData\Roaming\Telegram Desktop\Telegram.exe"};
            }


            foreach (string line in appLines)
            {
                var lineSplit = line.Split(",");
                var item = new ToolStripMenuItem(lineSplit[0], null, new EventHandler(LaunchDIR), lineSplit[1]);
                UtilitiesOptions.Add(item);
            }

            var exit = new ToolStripMenuItem("Exit", null, new EventHandler(Exit), "Exit");
            UtilitiesOptions.Add(exit);
        }


        public Form1()
        {
            checkGamesConfigs();
            checkAppConfigs();
            checkUtilitiesConfigs();

            GenerateGamesIcons();
            GenerateAppsIcons();
            GenerateUtilityIcons();
            InitializeComponent();

            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }
    }
}
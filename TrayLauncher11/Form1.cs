using System.Diagnostics;
using System.Drawing;
using System.Security;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace TrayLauncher11
{
    /// <summary>
    /// The main form of the system tray launcher program.
    /// </summary>
    public partial class TrayLauncher11 : Form
    {
        private IList<ToolStripMenuItem> AppsOptions = new List<ToolStripMenuItem>();
        private IList<ToolStripMenuItem> UtilitiesOptions = new List<ToolStripMenuItem>();
        private IList<ToolStripMenuItem> GamesOptions = new List<ToolStripMenuItem>();

        private IList<NotifyIcon> trayIcons = new List<NotifyIcon>();
        private NotifyIcon processTrayIcon;

        /// <summary>
        /// Generates a process tray icon with a context menu containing "Open Config" and "Exit" options.
        /// </summary>
        /// <param name="iconClass">The NotifyIcon object to generate the icon from.</param>
        /// <returns>The generated NotifyIcon object.</returns>
        private NotifyIcon GenerateProcessIcon(NotifyIcon iconClass)
        {
            Icon icon = new Icon(@"icons\spanner.ico");
            var contextStrip = new ContextMenuStrip();
            contextStrip.Items.Add(new ToolStripMenuItem("Open Config", null, new EventHandler(openConfigUtility), "Open Config"));
            contextStrip.Items.Add(new ToolStripMenuItem("Exit", null, new EventHandler(ProcessUtilities.Exit), "Exit"));
            return new NotifyIcon()
            {
                Icon = icon,
                ContextMenuStrip = contextStrip,
                Visible = true
            };
        }

        /// <summary>
        /// Disposes of all the tray icons created by the program.
        /// </summary>
        private void DestroyIcons()
        {
            foreach (NotifyIcon icon in trayIcons)
            {
                icon.Dispose();
            }
        }

        /// <summary>
        /// Initializes the icons and their associated context menus for the different types of applications, games, and utilities.
        /// </summary>
        private void initializeIcons()
        {
            ConfigUtility.generateConfigs(@"games.csv", GamesOptions);
            ConfigUtility.generateConfigs(@"apps.csv", AppsOptions);
            ConfigUtility.generateConfigs(@"utilities.csv", UtilitiesOptions);

            processTrayIcon = GenerateProcessIcon(processTrayIcon);

            IconGenerator.GenerateIcons(GamesOptions, @"icons\G.ico", trayIcons);
            IconGenerator.GenerateIcons(AppsOptions, @"icons\A.ico", trayIcons);
            IconGenerator.GenerateIcons(UtilitiesOptions, @"icons\U.ico", trayIcons);
        }

        /// <summary>
        /// Changes the state of the TrayLauncher11 window to FormWindowState.Normal.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        public void openConfigUtility(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Initializes a new instance of the TrayLauncher11 class.
        /// </summary>
        public TrayLauncher11()
        {
            InitializeComponent();

            initializeIcons();
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Disposes of the tray icons and the process tray icon when the program is closed.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DestroyIcons();
            processTrayIcon.Dispose();
            base.OnFormClosing(e);
        }
    }
}

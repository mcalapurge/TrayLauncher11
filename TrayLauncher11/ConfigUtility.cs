using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace TrayLauncher11
{
    internal static class ConfigUtility
    {
        /// <summary>
        /// Generates a list of ToolStripMenuItems based on the contents of a CSV configuration file.
        /// </summary>
        /// <param name="configLocation">The location of the CSV configuration file.</param>
        /// <param name="options">The list of ToolStripMenuItems to generate.</param>
        public static void generateConfigs(string configLocation, IList<ToolStripMenuItem> options)
        {
            string[] appLines = new string[] { };
            try
            {
                appLines = File.ReadAllLines(configLocation);
            }
            catch (Exception)
            {
                // If the file is not found, generate a default configuration file.
                switch (configLocation)
                {
                    case @"games.csv":
                        File.WriteAllText(@"games.csv", @$"Steam,C:\Program Files (x86)\Steam\steam.exe");
                        appLines = new string[] { @$"Steam,C:\Program Files (x86)\Steam\steam.exe" };
                        break;
                    case @"apps.csv":
                        File.WriteAllText(@"apps.csv", @$"Desktop,C:\users\{Environment.UserName}\Desktop");
                        appLines = new string[] { @$"Desktop,C:\users\{Environment.UserName}\Desktop" };
                        break;
                    case @"utilities.csv":
                        appLines = new string[] {@"Chrome,C:\Program Files\Google\Chrome\Application\chrome.exe",
                        @"Telegram,C:\Users\xboxa\AppData\Roaming\Telegram Desktop\Telegram.exe"};
                        File.WriteAllLines(@"utilities.csv", appLines);
                        break;
                }
            }

            // Create a ToolStripMenuItem for each line in the configuration file.
            foreach (string line in appLines)
            {
                var lineSplit = line.Split(",");
                var item = new ToolStripMenuItem(lineSplit[0], null, new EventHandler(ProcessUtilities.LaunchDIR), lineSplit[1]);
                options.Add(item);
            }
        }
    }
}

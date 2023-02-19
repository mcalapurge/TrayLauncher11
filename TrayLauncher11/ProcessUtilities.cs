using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayLauncher11
{
    internal static class ProcessUtilities
    {
        /// <summary>
        /// Exits the application when the "Exit" option is selected from the context menu.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        public static void Exit(object sender, EventArgs e)
        {
            Program.ExitApp();
        }

        /// <summary>
        /// Launches the Windows file explorer in the specified directory.
        /// Can also launch files if the argument is a file. Default Mime type app will be used.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        public static void LaunchDIR(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", (sender as ToolStripMenuItem).Name);
        }
    }
}

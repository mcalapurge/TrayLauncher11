using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayLauncher11
{
    internal static class IconGenerator
    {
        /// <summary>
        /// Generates a list of NotifyIcons with the specified options and icon location.
        /// </summary>
        /// <param name="options">A list of ToolStripMenuItems to be added to the ContextMenuStrip for each NotifyIcon.</param>
        /// <param name="iconLocation">The file path for the icon file to be used for the NotifyIcons.</param>
        /// <param name="iconList">A list of NotifyIcons that will be generated.</param>
        public static void GenerateIcons(IList<ToolStripMenuItem> options, string iconLocation, IList<NotifyIcon> iconList)
        {
            Icon icon = new Icon(iconLocation);

            var contextStrip = new ContextMenuStrip();

            foreach (ToolStripMenuItem item in options)
            {
                contextStrip.Items.Add(item);
            }

            iconList.Add(new NotifyIcon()
            {
                Icon = icon,
                ContextMenuStrip = contextStrip,
                Visible = true
            });
        }
    }
}

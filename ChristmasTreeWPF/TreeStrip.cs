using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace ChristmasTreeWPF
{
    public class TreeStrip : IDisposable
    {
        public NotifyIcon notifyIcon;
        private Window window;
        int nowYear = DateTime.Now.Year;


        ~TreeStrip()
        {
            Dispose();
        }

        public TreeStrip(Window window)
        {
            this.window = window;
            string nameOfIcon = "tree.ico";
            string nameOfApp = System.Windows.Forms.Application.ProductName;
            System.Windows.Resources.StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri(@"/" + nameOfApp + ";component/" + nameOfIcon, UriKind.RelativeOrAbsolute));
            Icon icon = new Icon(sri.Stream);

            ContextMenuStrip menu = CreateMenu();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = icon;
            notifyIcon.Text = "Christmas Tree";
            notifyIcon.ContextMenuStrip = menu;
            notifyIcon.Visible = true;

            notifyIcon.BalloonTipTitle = "Christmas Tree";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.BalloonTipText = $"Merry Chistmas {nowYear} \n& Happy New Year\nŁukasz Józefczyk";

            notifyIcon.DoubleClick += (s, e) =>
            {

                notifyIcon.ShowBalloonTip(3000);
            };

            window.MouseRightButtonDown += (s, e) =>
            {
                System.Windows.Point point = window.PointToScreen(e.GetPosition(window));
                menu.Show((int)point.X, (int)point.Y);
            };
        }

        private ContextMenuStrip CreateMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            ToolStripMenuItem hideToolStripMenuItem = new ToolStripMenuItem("Hide");
            hideToolStripMenuItem.Click += (s, e) => { window.Hide(); };
            menu.Items.Add(hideToolStripMenuItem);

            ToolStripMenuItem showToolStripMenuItem = new ToolStripMenuItem("Show");
            showToolStripMenuItem.Click += (s, e) => { window.Show(); };
            menu.Items.Add(showToolStripMenuItem);

            ToolStripMenuItem closeToolStripMenuItem = new ToolStripMenuItem("Close");
            closeToolStripMenuItem.Click += (s, e) => { window.Close(); };
            menu.Items.Add(closeToolStripMenuItem);

            return menu;
        }


        public bool VisibilityOfIcon
        {
            get { return notifyIcon.Visible; }
            set { notifyIcon.Visible = value; }
        }
        
        public void Dispose()
        {
            notifyIcon.Visible = false;
            notifyIcon = null;
        }
    }
}

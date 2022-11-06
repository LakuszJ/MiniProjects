using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChristmasTreeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region WindowMove
        private bool isMoving = false;
        private Cursor cursor = null;
        private Point startPoint;

        private void chTree_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == Mouse.LeftButton)
            {
                isMoving = true;
                cursor = this.Cursor;
                Cursor = Cursors.Hand;
                startPoint = e.GetPosition(this);
            }
        }

        private void chTree_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                Vector vectoreMove = e.GetPosition(this) - startPoint;
                Left += vectoreMove.X;
                Top += vectoreMove.Y;
            }
        }

        private void chTree_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isMoving)
            {
                Cursor = cursor;
                isMoving = false;
            }
        }
        #endregion

        private void chTree_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void chTree_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        private TreeStrip treeStrip;

        public MainWindow()
        {
            InitializeComponent();

            treeStrip = new TreeStrip(this);
            treeStrip.notifyIcon.ShowBalloonTip(3000);

            Storyboard storyboard = this.Resources["scenChangeTree"] as Storyboard;
            storyboard.Begin();
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

        private bool endingAnimationDisapering = false;

        private void chTree_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void chTree_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!endingAnimationDisapering)
            {
                Storyboard storyboard = this.Resources["scenClosingWindow"] as Storyboard;
                storyboard.Begin();
                e.Cancel = true;
            }
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            endingAnimationDisapering = true;
            Close();
            treeStrip.Dispose();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            ChangeImage();
            Storyboard storyboard = this.Resources["scenChangeTree"] as Storyboard;
            storyboard.Begin();
        }

        private int treeNo = 2;


        private void ChangeImage()
        {
            string filename = "pngegg" + treeNo + ".png";
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(filename, UriKind.Relative);
            image.EndInit();
            img.Source = image;
            treeNo++;
            if (treeNo > 4)
            {
                treeNo = 1;
            }
        }

    }
}

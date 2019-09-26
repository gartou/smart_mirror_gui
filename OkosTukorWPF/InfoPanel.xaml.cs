using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZedGraph;
using Location = Xceed.Wpf.Toolkit.Location;

namespace OkosTukorWPF
{
    /// <summary>
    /// Interaction logic for InfoPanel.xaml
    /// </summary>
    public partial class InfoPanel : Window
    {
        public InfoPanel()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.MaximizeToSecondaryMonitor();
        }

        public void MaximizeToSecondaryMonitor()
        {
            var secondaryScreen = Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();

            if (secondaryScreen != null)
            {
                var workingArea = secondaryScreen.WorkingArea;
                this.Left = workingArea.Left;
                this.Top = workingArea.Top;
                this.Width = workingArea.Width;
                this.Height = workingArea.Height;

                if (this.IsLoaded)
                {
                    this.WindowState = WindowState.Maximized;
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

    }
}

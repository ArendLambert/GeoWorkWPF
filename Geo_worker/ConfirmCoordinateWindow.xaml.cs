using System.Windows;

namespace Geo_worker
{
    public partial class ConfirmCoordinateWindow : Window
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public ConfirmCoordinateWindow(double x, double y)
        {
            InitializeComponent();
            X = x;
            Y = y;
            txtX.Text = x.ToString();
            txtY.Text = y.ToString();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
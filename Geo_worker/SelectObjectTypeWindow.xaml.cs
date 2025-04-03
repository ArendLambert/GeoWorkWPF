using System.Windows;
using System.Windows.Controls;

namespace Geo_worker
{
    public partial class SelectObjectTypeWindow : Window
    {
        public string SelectedType { get; private set; }

        public SelectObjectTypeWindow()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbObjectType.SelectedItem is ComboBoxItem selectedItem)
            {
                SelectedType = selectedItem.Content.ToString();
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите тип объекта.");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
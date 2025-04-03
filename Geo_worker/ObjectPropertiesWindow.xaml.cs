using System;
using System.Collections.Generic;
using System.Windows;
using Core.Abstractions;
using Core.Models;

namespace Geo_worker
{
    public partial class ObjectPropertiesWindow : Window
    {
        public string ObjectName { get; private set; }
        public int? SelectedParentId { get; private set; }

        public ObjectPropertiesWindow(string title, List<BaseModel> parentItems)
        {
            InitializeComponent();
            Title = title;
            cbParent.ItemsSource = parentItems; // Список родительских объектов
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя объекта.");
                return;
            }
            if (cbParent.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите родительский объект.");
                return;
            }

            ObjectName = txtName.Text;
            SelectedParentId = (cbParent.SelectedItem as BaseModel)?.Id;
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
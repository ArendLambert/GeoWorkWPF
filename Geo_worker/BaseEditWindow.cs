using System.Windows;
using System.Windows.Controls;

namespace Geo_worker
{
    public abstract class BaseEditWindow : Window
    {
        protected Button btnSave;
        protected Button btnCancel;

        public BaseEditWindow()
        {
            // Настройка окна
            Width = 400;
            Height = 300;
            Title = "Редактирование";

            // Основной контейнер
            Grid mainGrid = new Grid { Margin = new Thickness(10) };
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Область контента (будет заполнена в наследниках)
            ContentControl contentArea = new ContentControl();
            Grid.SetRow(contentArea, 0);
            mainGrid.Children.Add(contentArea);

            // Панель кнопок
            StackPanel buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            btnSave = new Button
            {
                Content = "Сохранить",
                Width = 80,
                Margin = new Thickness(0, 0, 10, 0)
            };
            btnSave.Click += BtnSave_Click;
            btnCancel = new Button
            {
                Content = "Отменить",
                Width = 80
            };
            btnCancel.Click += BtnCancel_Click;
            buttonPanel.Children.Add(btnSave);
            buttonPanel.Children.Add(btnCancel);
            Grid.SetRow(buttonPanel, 1);
            mainGrid.Children.Add(buttonPanel);

            // Установка содержимого окна
            Content = mainGrid;
        }

        protected abstract void BtnSave_Click(object sender, RoutedEventArgs e);

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
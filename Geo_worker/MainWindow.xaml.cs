using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Geo_worker.Core.Models;

namespace Geo_worker;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public ImageBrush LockBrush { get; set; }
    public ImageBrush EyeBrush { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        string mainimgsrc = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "1.jpg");
        mainImage.Source = new BitmapImage(new Uri(mainimgsrc, UriKind.Absolute));
        LockBrush = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "lock.png"), UriKind.Absolute)));
        EyeBrush = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "eye.png"), UriKind.Absolute)));
        DataContext = this;
        IRepository<AccessLevel> someRepo = new AccessLevelRepository(new FinallyBoryakin2207g2_GravitySurveyContext());
        //someRepo.Create("test", "test");
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }
}
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
using Core.DataCreate;
using Core.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Geo_worker.Controllers;
using Geo_worker.Models;

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

        //DBAdding().GetAwaiter();
        //DBClear().GetAwaiter();
        Draw();

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }

    private async Task DBAdding()
    {
        await SyntheticData.CreateAll();
    }

    private async Task DBClear()
    {
        await SyntheticData.DeleteAll();
    }

    private void Draw()
    {
        LayerBuilder builder = new LayerBuilder();
        LayerDrawer layerDrawer = new LayerDrawer(DrawingCanvas);

        builder.BuildRectangle(new PointDraw(90, 90), 300, 300, 1, Brushes.Gray, Brushes.Black);
        Layer layer = builder.GetResult();

        builder.CreateLayer();
        builder.BuildEllipse(new PointDraw(100, 100), 100, 100, 10, Brushes.Green, Brushes.Black);
        builder.BuildText("Hello", new PointDraw(200, 200), Brushes.Black, 12);
        builder.BuildZigZag(new List<PointDraw> { new PointDraw(100, 100), new PointDraw(200, 200), new PointDraw(300, 100), new PointDraw(400, 300) }, 4, Brushes.Black);
        Layer layer2 = builder.GetResult();

        layerDrawer.DrawLayer(layer);
        layerDrawer.DrawLayer(layer2);
    }
}
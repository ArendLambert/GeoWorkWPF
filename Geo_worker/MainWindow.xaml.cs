using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public int UserId { get; private set; } = 0;
    public Project SelectedProject { get; private set; }
    public ObservableCollection<DisplayPoint> PointsInPicket { get; private set; } = new ObservableCollection<DisplayPoint>();
    public ObservableCollection<Layer> Layers { get; private set; } = new ObservableCollection<Layer>();

    private UnitOfWork _unitOfWork;
    public LayerDrawer MainLayerDrawer { get; private set; }
    public MainWindow(int userId)
    {
        InitializeComponent();
        MainLayerDrawer = new LayerDrawer(DrawingCanvas);
        MessageBox.Show(MainLayerDrawer.ToString());
        string mainimgsrc = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "1.jpg");
        mainImage.Source = new BitmapImage(new Uri(mainimgsrc, UriKind.Absolute));
        LockBrush = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "lock.png"), UriKind.Absolute)));
        EyeBrush = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "eye.png"), UriKind.Absolute)));
        UserId = userId;
        DataContext = this;

        _unitOfWork = new UnitOfWork(new GravitySurveyOnDeleteNoAction(), UserId);

        //ProjectInitialize();
        //DBAdding().GetAwaiter();
        //DBClear().GetAwaiter();
        //Draw();

    }

    public MainWindow()
    {
        InitializeComponent();
        MainLayerDrawer = new LayerDrawer(DrawingCanvas);
        string mainimgsrc = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "1.jpg");
        mainImage.Source = new BitmapImage(new Uri(mainimgsrc, UriKind.Absolute));
        LockBrush = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "lock.png"), UriKind.Absolute)));
        EyeBrush = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "eye.png"), UriKind.Absolute)));
        DataContext = this;

        _unitOfWork = new UnitOfWork(new GravitySurveyOnDeleteNoAction(), UserId);

        //ProjectInitialize();
        //DBAdding().GetAwaiter();
        //DBClear().GetAwaiter();
        //Draw();

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
        //LayerBuilder builder = new LayerBuilder();
        //LayerDrawer layerDrawer = new LayerDrawer(DrawingCanvas);

        //builder.BuildRectangle(new PointDraw(90, 90), 300, 300, 1, Brushes.Gray, Brushes.Black);
        //Layer layer = builder.GetResult();

        //builder.CreateLayer();
        //builder.BuildEllipse(new PointDraw(100, 100), 100, 100, 10, Brushes.Green, Brushes.Black);
        //builder.BuildText("Hello", new PointDraw(200, 200), Brushes.Black, 12);
        //builder.BuildZigZag(new List<PointDraw> { new PointDraw(100, 100), new PointDraw(200, 200), new PointDraw(300, 100), new PointDraw(400, 300) }, 4, Brushes.Black);
        //Layer layer2 = builder.GetResult();

        //layerDrawer.DrawLayer(layer);
        //layerDrawer.DrawLayer(layer2);
    }

    private async void ProjectInitialize()
    {
        await PointTableInitialize(0);
    }

    private async Task PointTableInitialize(int picketId)
    {
        // Binding to DataGrid not working good!!!
        PointsInPicket.Clear();
        List<Core.Models.Point> points = await _unitOfWork.PointRepository.GetAll();
        List<DisplayPoint> displayPoints = new List<DisplayPoint>();
        foreach (Core.Models.Point point in points)
        {
            Equipment equipment = await _unitOfWork.EquipmentRepository.GetById(point.IdEquipment);
            Employee employee = await _unitOfWork.EmployeeRepository.GetById(point.IdOperator);
            displayPoints.Add(new DisplayPoint(point, equipment.Name, employee.Passport));
        }
        PointsInPicket = new ObservableCollection<DisplayPoint>(displayPoints.Where(points => points.IdPicket == picketId));

        PointsDataGrid.ItemsSource = PointsInPicket;
    }

    private async void ImportProjectButton_Click(object sender, RoutedEventArgs e)
    {
        ImportProjectWindow importProjectWindow = new ImportProjectWindow(UserId);
        if (importProjectWindow.ShowDialog() == true)
        {
            await ClearFields();
            Project? project = importProjectWindow.SelectedProject;
            SelectedProject = project;
            ProjectNameLabel.Content = project.Name;
            await LoadProjectsInTree();
            await DrawSquares();
        }
        else
        {
            MessageBox.Show("No project selected");
        }
    }

    private async Task LoadProjectsInTree()
    {
        List<Square> squares = await _unitOfWork.SquareRepository.GetAll();
        ObservableCollection<Square> squaresCollection = new ObservableCollection<Square>(squares.Where(s => s.IdProject == SelectedProject.Id));
        List<Profile> profiles = await _unitOfWork.ProfileRepository.GetAll();
        ObservableCollection<Profile> profilesCollection = new ObservableCollection<Profile>(profiles.Where(p => squaresCollection.Any(s => s.Id == p.IdSquare)));
        List<Picket> pickets = await _unitOfWork.PicketRepository.GetAll();
        ObservableCollection<Picket> picketsCollection = new ObservableCollection<Picket>(pickets.Where(p => profilesCollection.Any(pr => pr.Id == p.IdProfile)));
        List<Core.Models.Point> points = await _unitOfWork.PointRepository.GetAll();
        ObservableCollection<Core.Models.Point> pointsCollection = new ObservableCollection<Core.Models.Point>(points.Where(p => picketsCollection.Any(pi => pi.Id == p.IdPicket)));

        ProjectTreeView.Items.Clear();
        TreeViewItem projectsView = new TreeViewItem();
        projectsView.Header = SelectedProject.Name;
        projectsView.Tag = "Project";
        foreach (Square square in squaresCollection)
        {
            TreeViewItem squareView = new TreeViewItem();
            squareView.Header = square.Name;
            squareView.Tag = "Square " + square.Id;
            projectsView.Items.Add(squareView);
            foreach(Profile profile in profilesCollection.Where(p => p.IdSquare == square.Id))
            {
                TreeViewItem profileView = new TreeViewItem();
                profileView.Header = profile.Name;
                profileView.Tag = "Profile " + profile.Id;
                squareView.Items.Add(profileView);
                foreach (Picket picket in picketsCollection.Where(p => p.IdProfile == profile.Id))
                {
                    TreeViewItem picketView = new TreeViewItem();
                    picketView.Header = picket.Name;
                    picketView.Tag = "Picket " + picket.Id;
                    profileView.Items.Add(picketView);
                    foreach (Core.Models.Point point in pointsCollection.Where(p => p.IdPicket == picket.Id))
                    {
                        TreeViewItem pointView = new TreeViewItem();
                        pointView.Header = $"Точка {point.Id}";
                        pointView.Tag = "Point " + point.Id;
                        picketView.Items.Add(pointView);
                    }
                }
            }
        }
        ProjectTreeView.Items.Add(projectsView);
    }

    private async void ProjectTreeView_MouseClick(object sender, MouseButtonEventArgs e)
    {
        if (ProjectTreeView.SelectedItem as TreeViewItem is TreeViewItem item)
        {
            if(item.Tag.ToString().Contains("Picket"))
            {
                int idPicket = int.Parse(item.Tag.ToString().Split(' ')[1]);
                Picket? picket = await _unitOfWork.PicketRepository.GetById(idPicket);
                PicketNameTextBox.Text = picket.Name;
                await WriteSquareNameInTreeView(ProjectTreeView, (item.Parent as TreeViewItem).Parent as TreeViewItem);
                await PointTableInitialize(picket.Id);
            }
        }
        await WriteSquareNameInTreeView(ProjectTreeView, ProjectTreeView.SelectedItem as TreeViewItem);
    }

    private async Task WriteSquareNameInTreeView(TreeView treeView, TreeViewItem treeViewItem)
    {
        if (treeViewItem is TreeViewItem item2)
        {
            if (item2.Tag.ToString().Contains("Square"))
            {
                int idSquare = int.Parse(item2.Tag.ToString().Split(' ')[1]);
                Square? square = await _unitOfWork.SquareRepository.GetById(idSquare);
                SquareNameTextBox.Text = square.Name;
            }
        }
    }

    private async Task ClearFields()
    {
        PicketNameTextBox.Text = "";
        SquareNameTextBox.Text = "";
        ProjectTreeView.Items.Clear();
        PointsDataGrid.ItemsSource = null;
        ProjectNameLabel.Content = "";
        Layers.Clear();
        MainLayerDrawer.ClearCanvas();
        await Task.CompletedTask;

    }

    public class DisplayPoint : Core.Models.Point
    {
        public string? EquipmentName { get; private set; }
        public string? EmployeeName { get; private set; }

        public DisplayPoint(Core.Models.Point point, string? equipmentName, string? employeeName) : base(point.Id, point.X, point.Y, point.Gravity, point.GravityAnomaly, point.Amendments, point.Datetime, point.IdOperator, point.IdEquipment, point.IdPicket)
        {
            Id = point.Id;
            X = point.X;
            Y = point.Y;
            Gravity = point.Gravity;
            GravityAnomaly = point.GravityAnomaly;
            Amendments = point.Amendments;
            Datetime = point.Datetime;
            IdOperator = point.IdOperator;
            IdEquipment = point.IdEquipment;
            IdPicket = point.IdPicket;
            EquipmentName = equipmentName;
            EmployeeName = employeeName;
        }
    }

    private void SearchTermTextBlock_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (ProjectTreeView == null)
        {
            return;
        }

        string searchText = SearchTermTextBox.Text.ToLower();

        foreach (var item in ProjectTreeView.Items)
        {
            if (item is TreeViewItem treeViewItem)
            {
                FilterTreeViewItem(treeViewItem, searchText);
            }
        }
    }

    private void FilterTreeViewItem(TreeViewItem item, string searchText)
    {
        bool isVisible = string.IsNullOrEmpty(searchText) ||
                         item.Header.ToString().ToLower().Contains(searchText);

        foreach (var child in item.Items)
        {
            if (child is TreeViewItem childItem)
            {
                FilterTreeViewItem(childItem, searchText);
                if (childItem.Visibility == Visibility.Visible)
                {
                    isVisible = true;
                }
            }
        }

        item.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    private async Task DrawSquares()
    {
        Debug.WriteLine("DrawSquares");
        
        LayerBuilder builder = new LayerBuilder();
        builder.OnShapeClicked = async(shape, e) =>
        {
            if (shape is Shape s)
            {
                if (s is Polygon polygon)
                {
                    await DrawProfiles(int.Parse(polygon.Tag.ToString()));
                }
            }
        };
        ObservableCollection<Square> squares = new ObservableCollection<Square>(await _unitOfWork.SquareRepository.GetAll());
        ObservableCollection<Square> squaresCollection = new ObservableCollection<Square>(squares.Where(s => s.IdProject == SelectedProject.Id));
        ObservableCollection<AreaCoordinate> areaCoordinates = new ObservableCollection<AreaCoordinate>(await _unitOfWork.AreaCoordinateRepository.GetAll());
        foreach (var square in squaresCollection)
        {
            //MessageBox.Show(square.Name);
            var squareCoordinates = areaCoordinates.Where(ac => ac.IdSquare == square.Id);
            List<PointDraw> points = new List<PointDraw>();
            foreach (var squareCoordinate in squareCoordinates)
            {
                points.Add(new PointDraw(squareCoordinate.X, squareCoordinate.Y));
            }
            builder.BuildPolygon(points, 4, 4, Brushes.Gray, Brushes.Black, square.Id.ToString());
        }
        Layer layer = builder.GetResult();
        Layers.Add(layer);
        MainLayerDrawer.DrawLayer(layer);
    }

    private async Task DrawProfiles(int idSquare, bool hideOther = false)
    {
        if (hideOther)
        {
            foreach (var otherLayer in Layers)
            {
                //MessageBox.Show("HIDE");
                MainLayerDrawer.HideLayer(otherLayer);
            }
        }
        LayerBuilder builder = new LayerBuilder();
        builder.OnShapeClicked = async(shape, e) =>
        {
            if (shape is Shape s)
            {
                if (s is Polygon polygon)
                {
                    await DrawPickets(int.Parse(polygon.Tag.ToString()));

                }
            }
        };
        ObservableCollection<Profile> profiles = new ObservableCollection<Profile>(await _unitOfWork.ProfileRepository.GetAll());
        ObservableCollection<Profile> profilesCollection = new ObservableCollection<Profile>(profiles.Where(p => p.IdSquare == idSquare));
        ObservableCollection<ProfileCoordinate> profileCoordinates = new ObservableCollection<ProfileCoordinate>(await _unitOfWork.ProfileCoordinateRepository.GetAll());
        foreach (var profile in profilesCollection)
        {
            var profileCoordinatesFilter = profileCoordinates.Where(ac => ac.IdProfile == profile.Id);
            List<PointDraw> points = new List<PointDraw>();
            foreach (var profileCoordinate in profileCoordinatesFilter)
            {
                points.Add(new PointDraw(profileCoordinate.X, profileCoordinate.Y));
            }
            builder.BuildPolygon(points, 4, 1, Brushes.Silver, Brushes.Black, profile.Id.ToString());
        }
        Layer layer = builder.GetResult();
        Layers.Add(layer);
        //MessageBox.Show("DRAW");
        MainLayerDrawer.DrawLayer(layer);
    }

    private async Task DrawPickets(int idProfile)
    {       
        LayerBuilder builder = new LayerBuilder();
        builder.OnShapeClicked = async (shape, e) =>
        {
            if (shape is Shape s)
            {
                if (s is Line line)
                {
                    await DrawPonts(int.Parse(line.Tag.ToString()));
                    await PointTableInitialize(int.Parse(line.Tag.ToString()));
                }
            }
        };
        ObservableCollection<Picket> pickets = new ObservableCollection<Picket>(await _unitOfWork.PicketRepository.GetAll());
        ObservableCollection<Picket> picketsCollection = new ObservableCollection<Picket>(pickets.Where(p => p.IdProfile == idProfile));
        ObservableCollection<PicketCoordinate> picketCoordinates = new ObservableCollection<PicketCoordinate>(await _unitOfWork.PicketCoordinateRepository.GetAll());
        foreach (var picket in picketsCollection)
        {
            var picketCoordinatesFilter = picketCoordinates.Where(ac => ac.IdPicket == picket.Id);
            List<PointDraw> points = new List<PointDraw>();
            foreach (var picketCoordinate in picketCoordinatesFilter)
            {
                points.Add(new PointDraw(picketCoordinate.X, picketCoordinate.Y));
            }
            builder.BuildZigZag(points, 4, Brushes.Black, picket.Id.ToString());
        }
        Layer layer = builder.GetResult();
        Layers.Add(layer);
        MainLayerDrawer.DrawLayer(layer);
    }

    private async Task DrawPonts(int idPicket, bool hide = true)
    {
        if (hide)
        {
            bool find = false;
            List<UIElement> elements = new List<UIElement>();
            foreach (var otherLayer in Layers)
            {
                find = false;
                foreach (var part in otherLayer.Parts)
                {
                    if (part as Ellipse is Ellipse ellipse)
                    {
                        find = true;
                        elements.Add(ellipse);
                    }
                }
                if (find)
                {
                    MainLayerDrawer.HideLayer(otherLayer);
                    foreach (var element in elements)
                    {
                        otherLayer.RemovePart(element);
                    }
                    MainLayerDrawer.DrawLayer(otherLayer);
                }
            }
        }
        LayerBuilder builder = new LayerBuilder();
        builder.OnShapeClicked = (shape, e) =>
        {
            if (shape is Shape s)
            {
                if (s is Ellipse ellipse)
                {
                    MessageBox.Show("Точка " + ellipse.Tag.ToString());
                }
            }
        };
        ObservableCollection<Core.Models.Point> points = new ObservableCollection<Core.Models.Point>(await _unitOfWork.PointRepository.GetAll());
        ObservableCollection<Core.Models.Point> pointsCollection = new ObservableCollection<Core.Models.Point>(points.Where(p => p.IdPicket == idPicket));
        foreach (var point in pointsCollection)
        {
            builder.BuildEllipse(new PointDraw(point.X, point.Y), 5, 5, 1, Brushes.Red, Brushes.Black, point.Id.ToString());
        }
        Layer layer = builder.GetResult();
        Layers.Add(layer);
        MainLayerDrawer.DrawLayer(layer);
    }
    private async void ReDrawButton_Click(object sender, RoutedEventArgs e)
    {
        MainLayerDrawer.ClearCanvas();
        await DrawSquares();
    }

    private async void ProjectTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (ProjectTreeView.SelectedItem as TreeViewItem is TreeViewItem item)
        {
            string tag = item.Tag.ToString();

            if (tag.Contains("Project"))
            {
                await DrawSquares();
            }
            else if (tag.Contains("Square"))
            {
                int idSquare = int.Parse(tag.Split(' ')[1]);
                await DrawSquares();
                await DrawProfiles(idSquare, hideOther: false);
            }
            else if (tag.Contains("Profile"))
            {
                int idProfile = int.Parse(tag.Split(' ')[1]);
                int idSquare = int.Parse((item.Parent as TreeViewItem).Tag.ToString().Split(' ')[1]);
                await DrawSquares();
                await DrawProfiles(idSquare, hideOther: false);
                await DrawPickets(idProfile);
            }
            else if (tag.Contains("Picket"))
            {
                int idPicket = int.Parse(tag.Split(' ')[1]);
                int idProfile = int.Parse((item.Parent as TreeViewItem).Tag.ToString().Split(' ')[1]);
                int idSquare = int.Parse(((item.Parent as TreeViewItem).Parent as TreeViewItem).Tag.ToString().Split(' ')[1]);
                await DrawSquares();
                await DrawProfiles(idSquare, hideOther: false);
                await DrawPickets(idProfile);
                await DrawPonts(idPicket);
                Picket? picket = await _unitOfWork.PicketRepository.GetById(idPicket);
                PicketNameTextBox.Text = picket.Name;
                await WriteSquareNameInTreeView(ProjectTreeView, (item.Parent as TreeViewItem).Parent as TreeViewItem);
                await PointTableInitialize(picket.Id);
            }
            else if (tag.Contains("Point"))
            {
                int idPoint = int.Parse(tag.Split(' ')[1]);
                int idPicket = int.Parse((item.Parent as TreeViewItem).Tag.ToString().Split(' ')[1]);
                int idProfile = int.Parse(((item.Parent as TreeViewItem).Parent as TreeViewItem).Tag.ToString().Split(' ')[1]);
                int idSquare = int.Parse((((item.Parent as TreeViewItem).Parent as TreeViewItem).Parent as TreeViewItem).Tag.ToString().Split(' ')[1]);
                await DrawSquares();
                await DrawProfiles(idSquare, hideOther: false);
                await DrawPickets(idProfile);
                await DrawPonts(idPicket);
            }

            await WriteSquareNameInTreeView(ProjectTreeView, item);
        }
    }


}
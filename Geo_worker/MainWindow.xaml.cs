﻿using System.Collections.ObjectModel;
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
using Core.Abstractions;
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

    private List<PointDraw> tempCoordinates = new List<PointDraw>();
    public LayerDrawer MainLayerDrawer { get; private set; }
    public MainWindow(int userId)
    {
        InitializeComponent();
        ClearFields().GetAwaiter();
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
        ClearFields().GetAwaiter();

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
            MessageBox.Show("Проект не выбран");
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
        if(SelectedProject == null)
        {
            return;
        }
        Debug.WriteLine("DrawSquares");
        
        LayerBuilder builder = new LayerBuilder();
        builder.OnShapeClicked = async(shape, e) =>
        {
            if (shape is Shape s)
            {
                if (s is Polygon polygon)
                {
                    //MessageBox.Show(polygon.Tag.ToString());
                    await DrawProfiles(int.Parse(polygon.Tag.ToString()));
                }
            }
            //if(shape is Polygon polygon)
            //{
            //    await DrawProfiles(int.Parse(polygon.Tag.ToString()));
            //}
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
        if (ProjectTreeView.SelectedItem is TreeViewItem item)
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

    private void DrawingCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Получаем позицию клика на канвасе
        System.Windows.Point clickPosition = e.GetPosition(DrawingCanvas);

        // Создаем контекстное меню
        ContextMenu contextMenu = new ContextMenu();

        // Пункт для создания точки
        MenuItem createPointItem = new MenuItem { Header = "Создать точку" };
        createPointItem.Click += (s, args) => CreatePoint(clickPosition);

        // Пункт для добавления координаты
        MenuItem addCoordinateItem = new MenuItem { Header = "Добавить координату" };
        addCoordinateItem.Click += (s, args) => AddCoordinate(clickPosition);

        // Добавляем опцию завершения, если есть временные координаты
        if (tempCoordinates.Count > 0)
        {
            MenuItem finishCoordinatesItem = new MenuItem { Header = "Завершить объект" };
            finishCoordinatesItem.Click += (s, args) => FinishCoordinates();
            contextMenu.Items.Add(finishCoordinatesItem);
        }

        // Добавляем пункты в меню
        contextMenu.Items.Add(createPointItem);
        contextMenu.Items.Add(addCoordinateItem);

        // Показываем контекстное меню в месте клика
        contextMenu.IsOpen = true;
        contextMenu.PlacementTarget = DrawingCanvas;
        contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
    }
    private void CreatePoint(System.Windows.Point position)
    {
        // Создаем новую точку с координатами клика
        Core.Models.Point newPoint = Core.Models.Point.Create(
            idPoint: 0, // ID будет назначен базой данных
            x: position.X,
            y: position.Y,
            gravity: 0, // Значения по умолчанию, можно изменить в окне
            gravityAnomaly: 0,
            amendments: 0,
            datetime: DateTime.Now,
            idOperator: null,
            idEquipment: null,
            idPicket: null // Можно привязать к текущему пикету, если он выбран
        );

        // Открываем окно для редактирования точки
        AddPointsWindow addPointsWindow = new AddPointsWindow(UserId, SelectedProject.Id, newPoint );
        if (addPointsWindow.ShowDialog() == true)
        {
            Core.Models.Point createdPoint = addPointsWindow.GetPoint();

            // Сохраняем точку в базе данных
            _unitOfWork.PointRepository.Create(createdPoint).GetAwaiter();

            // Отрисовываем точку на канвасе
            DrawPoint(createdPoint);
        }
    }

    private void AddCoordinate(System.Windows.Point position)
    {
        // Открываем окно подтверждения
        ConfirmCoordinateWindow confirmWindow = new ConfirmCoordinateWindow(position.X, position.Y);
        if (confirmWindow.ShowDialog() == true)
        {
            // Если пользователь подтвердил, добавляем координату
            tempCoordinates.Add(new PointDraw(confirmWindow.X, confirmWindow.Y));
            // Отрисовываем временную координату
            DrawTempCoordinate(new System.Windows.Point(confirmWindow.X, confirmWindow.Y));
        }
        else
        {
            // Если пользователь отменил, выводим сообщение
            MessageBox.Show("Добавление координаты отменено.");
        }
    }

    private async void FinishCoordinates()
    {
        if (tempCoordinates.Count < 2)
        {
            MessageBox.Show("Недостаточно координат для создания объекта. Требуется минимум 2 точки.");
            return;
        }

        // Открываем окно выбора типа объекта
        SelectObjectTypeWindow selectTypeWindow = new SelectObjectTypeWindow();
        if (selectTypeWindow.ShowDialog() == true)
        {
            string selectedType = selectTypeWindow.SelectedType;

            // В зависимости от выбранного типа, сохраняем и отрисовываем объект
            switch (selectedType)
            {
                case "Square":
                    await SaveAndDrawSquare();
                    break;
                case "Profile":
                    await SaveAndDrawProfile();
                    break;
                case "Picket":
                    await SaveAndDrawPicket();
                    break;
                default:
                    MessageBox.Show("Неизвестный тип объекта.");
                    break;
            }

            // Очищаем временные координаты
            tempCoordinates.Clear();
        }
        else
        {
            MessageBox.Show("Сохранение объекта отменено.");
        }
    }
    private async Task SaveAndDrawSquare()
    {
        if (SelectedProject == null)
        {
            MessageBox.Show("Выберите проект перед созданием объекта.");
            return;
        }

        // Создаем новый Square
        Square newSquare = Square.Create(
            id: 0, // ID будет назначен базой данных
            name: "New Square",
            alitude: 0, // Можно добавить поле ввода для высоты
            idProject: SelectedProject.Id
        );

        // Сохраняем Square в базе данных
        await _unitOfWork.SquareRepository.Create(newSquare);

        // Получаем созданный Square с актуальным ID
        var savedSquare = (await _unitOfWork.SquareRepository.GetAll())
            .OrderByDescending(s => s.Id)
            .FirstOrDefault(s => s.Name == newSquare.Name && s.IdProject == newSquare.IdProject);
        if (savedSquare == null)
        {
            MessageBox.Show("Ошибка при сохранении Square.");
            return;
        }
        int squareId = savedSquare.Id;

        // Сохраняем координаты
        foreach (var coord in tempCoordinates)
        {
            AreaCoordinate areaCoord = AreaCoordinate.Create(0, squareId, coord.X, coord.Y);
            await _unitOfWork.AreaCoordinateRepository.Create(areaCoord);
        }

        // Отрисовываем Square
        LayerBuilder builder = new LayerBuilder();
        builder.BuildPolygon(tempCoordinates, 4, 4, Brushes.Gray, Brushes.Black, squareId.ToString());
        Layer layer = builder.GetResult();
        Layers.Add(layer);
        MainLayerDrawer.DrawLayer(layer);
    }
    private async Task SaveAndDrawProfile()
    {
        if (SelectedProject == null)
        {
            MessageBox.Show("Выберите проект перед созданием объекта.");
            return;
        }

        // Получаем список доступных Square для текущего проекта
        var squares = await _unitOfWork.SquareRepository.GetAll();
        var projectSquares = squares.Where(s => s.IdProject == SelectedProject.Id).ToList();

        if (!projectSquares.Any())
        {
            MessageBox.Show("Нет доступных Square для привязки Profile.");
            return;
        }

        // Открываем окно настройки свойств
        ObjectPropertiesWindow propertiesWindow = new ObjectPropertiesWindow(
        "Настройки Profile",
        projectSquares.Cast<BaseModel>().ToList()
        );

        if (propertiesWindow.ShowDialog() == true)
        {
            string profileName = propertiesWindow.ObjectName;
            int? idSquare = propertiesWindow.SelectedParentId;

            // Создаем новый Profile
            Profile newProfile = Profile.Create(
                id: 0, // ID будет назначен базой данных
                name: profileName,
                idSquare: idSquare
            );

            // Сохраняем Profile в базе данных
            await _unitOfWork.ProfileRepository.Create(newProfile);

            // Получаем созданный Profile с актуальным ID
            var savedProfile = (await _unitOfWork.ProfileRepository.GetAll())
                .OrderByDescending(p => p.Id)
                .FirstOrDefault(p => p.Name == newProfile.Name && p.IdSquare == newProfile.IdSquare);
            if (savedProfile == null)
            {
                MessageBox.Show("Ошибка при сохранении Profile.");
                return;
            }
            int profileId = savedProfile.Id;

            // Сохраняем координаты в таблицу ProfileCoordinate
            foreach (var coord in tempCoordinates)
            {
                ProfileCoordinate profileCoord = ProfileCoordinate.Create(0, profileId, coord.X, coord.Y);
                await _unitOfWork.ProfileCoordinateRepository.Create(profileCoord);
            }

            // Отрисовываем Profile
            LayerBuilder builder = new LayerBuilder();
            builder.BuildPolygon(tempCoordinates, 4, 1, Brushes.Silver, Brushes.Black, profileId.ToString());
            Layer layer = builder.GetResult();
            Layers.Add(layer);
            MainLayerDrawer.DrawLayer(layer);
        }
        else
        {
            MessageBox.Show("Создание Profile отменено.");
        }
    }

    private async Task SaveAndDrawPicket()
    {
        if (SelectedProject == null)
        {
            MessageBox.Show("Выберите проект перед созданием объекта.");
            return;
        }

        // Получаем список доступных Profile
        var profiles = await _unitOfWork.ProfileRepository.GetAll();
        if (!profiles.Any())
        {
            MessageBox.Show("Нет доступных Profile для привязки Picket.");
            return;
        }

        // Открываем окно настройки свойств
        ObjectPropertiesWindow propertiesWindow = new ObjectPropertiesWindow(
            "Настройки Picket",
            profiles.Cast<BaseModel>().ToList()
        );

        if (propertiesWindow.ShowDialog() == true)
        {
            string picketName = propertiesWindow.ObjectName;
            int? idProfile = propertiesWindow.SelectedParentId;

            // Создаем новый Picket
            Picket newPicket = Picket.Create(
                id: 0, // ID будет назначен базой данных
                name: picketName,
                idProfile: idProfile
            );

            // Сохраняем Picket в базе данных
            await _unitOfWork.PicketRepository.Create(newPicket);

            // Получаем созданный Picket с актуальным ID
            var savedPicket = (await _unitOfWork.PicketRepository.GetAll())
                .OrderByDescending(p => p.Id)
                .FirstOrDefault(p => p.Name == newPicket.Name && p.IdProfile == newPicket.IdProfile);
            if (savedPicket == null)
            {
                MessageBox.Show("Ошибка при сохранении Picket.");
                return;
            }
            int picketId = savedPicket.Id;

            // Сохраняем координаты в таблицу PicketCoordinate
            foreach (var coord in tempCoordinates)
            {
                PicketCoordinate picketCoord = PicketCoordinate.Create(0, picketId, coord.X, coord.Y);
                await _unitOfWork.PicketCoordinateRepository.Create(picketCoord);
            }

            // Отрисовываем Picket как зигзаг
            LayerBuilder builder = new LayerBuilder();
            builder.BuildZigZag(tempCoordinates, 4, Brushes.Black, picketId.ToString());
            Layer layer = builder.GetResult();
            Layers.Add(layer);
            MainLayerDrawer.DrawLayer(layer);
        }
        else
        {
            MessageBox.Show("Создание Picket отменено.");
        }
    }

    private void DrawPoint(Core.Models.Point point)
    {
        LayerBuilder builder = new LayerBuilder();
        builder.BuildEllipse(new PointDraw(point.X, point.Y), 5, 5, 1, Brushes.Red, Brushes.Black, point.Id.ToString());
        Layer layer = builder.GetResult();
        Layers.Add(layer);
        MainLayerDrawer.DrawLayer(layer);
    }

    private void DrawTempCoordinate(System.Windows.Point position)
    {
        // Отрисовываем временный маркер
        Ellipse tempEllipse = new Ellipse
        {
            Width = 5,
            Height = 5,
            Fill = Brushes.Yellow,
            Stroke = Brushes.Black,
            StrokeThickness = 1
        };
        Canvas.SetLeft(tempEllipse, position.X - 2.5);
        Canvas.SetTop(tempEllipse, position.Y - 2.5);
        DrawingCanvas.Children.Add(tempEllipse);

        // Отрисовываем линии между точками, если их больше одной
        if (tempCoordinates.Count >= 2)
        {
            Line tempLine = new Line
            {
                X1 = tempCoordinates[tempCoordinates.Count - 2].X,
                Y1 = tempCoordinates[tempCoordinates.Count - 2].Y,
                X2 = tempCoordinates[tempCoordinates.Count - 1].X,
                Y2 = tempCoordinates[tempCoordinates.Count - 1].Y,
                Stroke = Brushes.Yellow,
                StrokeThickness = 1
            };
            DrawingCanvas.Children.Add(tempLine);
        }
    }

    private async void LoadSyntheticDataButton_Click(object sender, RoutedEventArgs e)
    {
        await DBAdding();
        MessageBox.Show("Данные загружены");

    }

    private async void DeleteSyntheticDataButton_Click(object sender, RoutedEventArgs e)
    {
        await DBClear();
        MessageBox.Show("Данные удалены!!");
    }
}
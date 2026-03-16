using MoneyRecords.Models;
using MoneyRecords.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyRecords
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICollectionView UsersView;
        private User _currentUser;
        private AppDbContext? db;
        public MainWindow(User user)
        {
            InitializeComponent();

            _currentUser = user;
            UserNameTextBlock.Text = _currentUser.Name;
            DataContext = new MainViewModel();
            AppDbContext db = new AppDbContext();
            GetLocalTimeDate();
            
            string firstLetter = _currentUser.Name.Substring(0, 1).ToUpper();
            UserImage_TextBlock.Text = firstLetter;
            UserNameTextBox.Text = user.Name;
            UserImage_TextBlock.Foreground = ColourRandomiser.GetRandomColor().Foreground;

            using (var database = new AppDbContext())
            {
                var users = database.Users.ToList();

                UsersView = CollectionViewSource.GetDefaultView(users);
                UsersList.ItemsSource = UsersView;
            }
        }
        public void GetLocalTimeDate()
        {
            string Time = DateTime.Now.ToString("t");
            string Date = DateTime.Now.ToString("dddd dd, yyyy");

            DateTime_TextBlock.Text = $"{Date} {"|"} {Time}";
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void ToUsersGrid_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.Users);
        }
        private void ToHomeGrid_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.Home);
        }

        private void ShowScreen(ScreenType type)
        {
            HomeGrid.Visibility = Visibility.Hidden;
            UsersGrid.Visibility = Visibility.Hidden;

            switch (type)
            {
                case ScreenType.Home: HomeGrid.Visibility = Visibility.Visible; break;
                case ScreenType.Users: UsersGrid.Visibility = Visibility.Visible; break;
            }
        }
        public enum ScreenType
        {
            Home,
            Users
        }

        private void AddNewUserWinBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUser window = new AddUser();
            window.Owner = this;
            window.ShowDialog();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchBox.Text.ToLower();

            UsersView.Filter = obj =>
            {
                User user = obj as User;

                if (user == null)
                    return false;

                return user.Name.ToLower().Contains(search);
            };
        }
        //private void LoadData()
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        var collections = new (ItemsControl list, IEnumerable<object> data)[]
        //        {
        //            (UsersList, db.Users)
        //        };
        //        foreach (var (list, data) in collections)
        //            list.ItemsSource = data.ToList();
        //    }
        //}
    }
}
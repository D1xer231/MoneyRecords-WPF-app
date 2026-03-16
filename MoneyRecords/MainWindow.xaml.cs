using MoneyRecords.Models;
using MoneyRecords.ViewModels;
using System.Collections.ObjectModel;
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

namespace MoneyRecords
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            UsersList.ItemsSource = db.Users.ToList();
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

        
    }
}
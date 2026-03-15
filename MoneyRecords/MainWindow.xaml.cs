using MoneyRecords.Models;
using MoneyRecords.ViewModels;
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
            UserImage_TextBlock.Foreground = ColourRandomiser.GetRandomColor().Foreground;
        }

        public void GetLocalTimeDate()
        {
            string Time = DateTime.Now.ToString("t");
            string Date = DateTime.Now.ToString("dddd dd, yyyy");

            DateTime_TextBlock.Text = $"{Date} {"|"} {Time}";
        }
    }
}
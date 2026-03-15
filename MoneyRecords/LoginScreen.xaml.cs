using MoneyRecords.Models;
using MoneyRecords.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoneyRecords
{
    /// <summary>
    /// Логика взаимодействия для LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        private AppDbContext _db;
        public LoginScreen()
        {
            InitializeComponent();
            _db = new AppDbContext();
        }

        private void RegistrBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = RegEmailField.Text.Trim();
            string password = RegPasswordField.Password.Trim();
            string login = RegLoginField.Text.Trim();

            var hashedPassword = Password.Hash(password);

            // позже расписать оишбки для каждых полей
            if (login.Equals("") || !email.Contains("@") || password.Length < 3)
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }

            using (var db = new AppDbContext())
            {
                User user = new User
                {
                    Email = email,
                    Password = hashedPassword,
                    Name = login
                };

                db.Users.Add(user);
                db.SaveChanges();
            }
            MessageBox.Show("User registered!");
            RegEmailField.Text = "";
            RegPasswordField.Password = "";
            RegLoginField.Text = "";
            ShowScreen(ScreenType.Login);
        }

        private void ToLoginGrid_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.Login);
        }
        private void ToRegistrGrid_Click(object sender, RoutedEventArgs e)
        {
            ShowScreen(ScreenType.Registration);
        }

        private void ShowScreen(ScreenType type)
        {
            Welcomescreen_grid.Visibility = Visibility.Hidden;
            Registrationscreen_grid.Visibility = Visibility.Hidden;
            Loginscreen_grid.Visibility = Visibility.Hidden;

            switch (type)
            {
                case ScreenType.Welcome: Welcomescreen_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Registration: Registrationscreen_grid.Visibility = Visibility.Visible; break;
                case ScreenType.Login: Loginscreen_grid.Visibility = Visibility.Visible; break;
            }
        }
        public enum ScreenType
        {
            Welcome,
            Registration,
            Login
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LogEmailorLoginField.Text.Trim();
            string password = LogPasswordField.Password.Trim();

            if (login.Equals("") || password.Length < 3)
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }

            var hashedPassword = Password.Hash(password);
            User AuthUser = null;
            using (AppDbContext db = new AppDbContext())
            {
                AuthUser = db.Users.Where(user => (user.Name == login || user.Email == login) && user.Password == hashedPassword).FirstOrDefault();
            }
            if (AuthUser == null)
            {
                MessageBox.Show("This user doesnt exist!");
            }
            else
            {
                MessageBox.Show("User logged in!");

                var mainWindow = new MainWindow(AuthUser);
                mainWindow.Show();

                this.Close();
            }
        }
    }
}

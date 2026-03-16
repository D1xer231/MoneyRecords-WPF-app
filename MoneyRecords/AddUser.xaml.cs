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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void RegistrBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = RegEmailField.Text.Trim();
            string password = RegPasswordField.Password.Trim();
            string login = RegLoginField.Text.Trim();

            var hashedPassword = Password.Hash(password);

            if (login.Equals(" ") || email.Contains(" ") || password.Contains(" "))
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }
            if (!email.Contains("@") || email.Length < 5)
            {
                MessageBox.Show("Invalid Email!");
                return;
            }
            if (login.Length < 1 || password.Length < 4)
            {
                MessageBox.Show("Login or password too short!");
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
            var main = (MainWindow)Owner;

            using (var db = new AppDbContext())
            {
                DataLoader.Load(main.UsersList, db.Users);
            }
        }

        private void CloseWinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

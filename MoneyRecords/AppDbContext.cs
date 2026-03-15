using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MoneyRecords.Models;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace MoneyRecords
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultDb");
            
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString));

            //try
            //{
            //    options.UseMySql(
            //    connectionString,
            //    ServerVersion.AutoDetect(connectionString)
            //);
            //}
            //catch (MySqlConnector.MySqlException ex)
            //{
            //    MessageBox.Show("Error during connection to db. Check connection settings.\n" + ex.Message, "Money Records - Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    string user_input = Interaction.InputBox(
            //        "Tell us if u find any problems, Thank You!",
            //        "Money Records - Report",
            //        ""
            //    );
            //    if (user_input.Length < 5 || user_input == null || user_input == "")
            //    {
            //        MessageBox.Show("No message was sent", "Money Records - Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    }
            //else
            //{
            //    Process.Start(new ProcessStartInfo
            //    {
            //        FileName = $"mailto:aalexandr397@gmail.com?subject=AppReport&body={user_input}",
            //        UseShellExecute = true
            //    });
            //}
            //MessageBox.Show("Application will be closed.", "Money Records - Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            //Environment.Exit(0);
            }
        }
    }
//}

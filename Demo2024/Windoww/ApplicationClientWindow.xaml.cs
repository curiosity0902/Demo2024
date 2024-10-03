using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Demo2024.DB;

namespace Demo2024.Windoww
{
    /// <summary>
    /// Логика взаимодействия для ApplicationClientWindow.xaml
    /// </summary>
    public partial class ApplicationClientWindow : Window
    {
        public static List<Service> services { get; set; }
        public static List<Client> clients { get; set; }
        public static List<ClientService> clientServices { get; set; }
        public ApplicationClientWindow()
        {
            InitializeComponent(); 
            Refresh();
        }

        public async void Refresh() 
        {
            while (true)
            {
                DateTime today = DateTime.Now;
                DateTime tomorrow = today.AddDays(1);

                services = new List<Service>(DBConnection.schoolPractice.Service.ToList());
                clients = new List<Client>(DBConnection.schoolPractice.Client.ToList());
                clientServices = new List<ClientService>(DBConnection.schoolPractice.ClientService.
                Where(cs => (DateTime)cs.StartTime >= today && (DateTime)cs.StartTime <= tomorrow).
                OrderBy(cs => (DateTime)cs.StartTime).ToList());

                ApplicationLv.ItemsSource = clientServices;
                await Task.Delay(30000);
            }
        }
    }
}

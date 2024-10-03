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
using Demo2024.DB;

namespace Demo2024.Windoww
{
    /// <summary>
    /// Логика взаимодействия для AddClientService.xaml
    /// </summary>
    public partial class AddClientService : Window
    {
        public static List<Service> services { get; set; }
        public static List<Client> clients { get; set; }
        public static List<ClientService> clientServices { get; set; }

        public static ClientService clientService = new ClientService();

        Service contextService;
        public AddClientService(Service service)
        {
            InitializeComponent();
            contextService = service;

            services = new List<Service>(DBConnection.schoolPractice.Service.ToList());
            clients = new List<Client>(DBConnection.schoolPractice.Client.ToList());
            clientServices = new List<ClientService>(DBConnection.schoolPractice.ClientService.ToList());

            NameServiceTbl.Text = $"Название услуги: " + $"{contextService.Title}";
            DurationTbl.Text = $"Длительность: " + $"{contextService.DurationInMinutes}";

            this.DataContext = this;
        }

        private void ClientsCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddClientServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                if (ClientsCb.SelectedIndex == -1 || DateServiceDp.SelectedDate == null)

                {
                    error.AppendLine("Заполните все поля!");
                }

                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    //service.Title = NameTb.Text.Trim();
                    //service.Description = DescriptionTb.Text.Trim();
                    //service.DurationInSeconds = int.Parse(DurationTb.Text.Trim());
                    //service.Discount = int.Parse(SaleTb.Text.Trim());
                    //service.Cost = int.Parse(CostTb.Text.Trim());

                    //DBConnection.schoolPractice.Service.Add(service);
                    //DBConnection.schoolPractice.SaveChanges();
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }
    }
}

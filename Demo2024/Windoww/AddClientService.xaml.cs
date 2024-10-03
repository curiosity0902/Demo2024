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
using System.Windows.Media.Media3D;
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

        Service contextService;
        public AddClientService(Service service)
        {
            InitializeComponent();
            contextService = service;

            services = new List<Service>(DBConnection.schoolPractice.Service.ToList());
            clients = new List<Client>(DBConnection.schoolPractice.Client.ToList());
            clientServices = new List<ClientService>(DBConnection.schoolPractice.ClientService.ToList());

            DateServiceDp.DisplayDateStart = DateTime.Now;

            NameServiceTbl.Text = $"Название услуги: " + $"{contextService.Title}";
            DurationTbl.Text = $"Длительность: " + $"{contextService.DurationInSeconds / 60}" + " минут";

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

                if (ClientsCb.SelectedIndex == -1 || DateServiceDp.SelectedDate == null || TimeTbx.Text.Trim() == "")
                {
                    error.AppendLine("Заполните все поля!");
                }

                int hour = -1;
                int minute = -1;

                if (TimeTbx.Text.Length == 5 && TimeTbx.Text[2] == ':' &&
                    int.TryParse(TimeTbx.Text.Substring(0, 2), out hour) &&
                    int.TryParse(TimeTbx.Text.Substring(3, 2), out minute))
                {
                    if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
                    {
                        error.AppendLine("Введите корректное время в формате HH:mm!");
                    }
                }
                else
                {
                    error.AppendLine("Введите корректное время в формате HH:mm!");
                }

                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    ClientService clientService = new ClientService();

                    DateTime selectedDate = (DateTime)DateServiceDp.SelectedDate;
                    DateTime startTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, hour, minute, 0);

                    clientService.StartTime = startTime;
                    clientService.ServiceID = contextService.ID;

                    var selectedClient = ClientsCb.SelectedItem as Client;
                    clientService.ClientID = selectedClient.ID;

                    DateTime endTime = startTime.AddSeconds(contextService.DurationInSeconds);

                    DBConnection.schoolPractice.ClientService.Add(clientService);
                    DBConnection.schoolPractice.SaveChanges();
                    MessageBox.Show($"Услуга будет оказана с {startTime} до {endTime}");
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }

        private void TimeTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ":")
            {
                e.Handled = true;
            }
        }

        private void TimeTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TimeTbx.Text.Length == 5 && TimeTbx.Text[2] == ':')
            {
                int hour, minute;
                if (int.TryParse(TimeTbx.Text.Substring(0, 2), out hour) && int.TryParse(TimeTbx.Text.Substring(3, 2), out minute))
                {
                    if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
                    {
                        MessageBox.Show("Введите корректное время в формате часы:минуты!");
                        TimeTbx.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное время в формате HH:mm!");
                    TimeTbx.Text = string.Empty;
                }
            }
        }

    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Demo2024.DB;
using Demo2024.Windoww;

namespace Demo2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllServicesPage.xaml
    /// </summary>
    public partial class AllServicesPage : Page
    {
        public static List<Service> services { get; set; }
        public static Client loggedClient;
        public AllServicesPage()
        {
            InitializeComponent();

            loggedClient = DBConnection.loginedClient;


            services = new List<Service>(DBConnection.schoolPractice.Service.ToList());
            ProductLV.ItemsSource = services;
            this.DataContext = this;

            FiltrCb.Items.Add("По умолчанию");
            FiltrCb.Items.Add("По убыванию");
            FiltrCb.Items.Add("По возрастанию");

            FiltrSaleCb.Items.Add("от 0 до 5%");
            FiltrSaleCb.Items.Add("от 5% до 15%");
            FiltrSaleCb.Items.Add("от 15% до 30%");
            FiltrSaleCb.Items.Add("от 30% до 70%");
            FiltrSaleCb.Items.Add("от 70% до 100%");
            FiltrSaleCb.Items.Add("Все");
        }

        private void Refresh()
        {
            List<Service> services = new List<Service>(DBConnection.schoolPractice.Service.ToList());

            {
                services = services.Where(i => i.Title.ToLower().StartsWith(SearchTbx.Text.Trim().ToLower())
                   || i.Description.ToLower().StartsWith(SearchTbx.Text.Trim().ToLower())).ToList();
            }

            ProductLV.ItemsSource = services;

            if (FiltrCb.SelectedIndex == 0)
            {
                ProductLV.ItemsSource = services;
            }
            if (FiltrCb.SelectedIndex == 1)
            {
                ProductLV.ItemsSource = services.OrderByDescending(x => x.Cost).ToList();
            }
            if (FiltrCb.SelectedIndex == 2)
            {
                ProductLV.ItemsSource = services.OrderBy(x => x.Cost).ToList();
            }

            if (FiltrSaleCb.SelectedIndex == 0)
            {
                ProductLV.ItemsSource = services.Where(x => x.Discount >= 0 && x.Discount < 5).ToList();
            }
            if (FiltrSaleCb.SelectedIndex == 1)
            {
                ProductLV.ItemsSource = services.Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
            }
            if (FiltrSaleCb.SelectedIndex == 2)
            {
                ProductLV.ItemsSource = services.Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
            }
            if (FiltrSaleCb.SelectedIndex == 3)
            {
                ProductLV.ItemsSource = services.Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
            }
            if (FiltrSaleCb.SelectedIndex == 4)
            {
                ProductLV.ItemsSource = services.Where(x => x.Discount >= 70 && x.Discount < 100).ToList();
            }
            if (FiltrSaleCb.SelectedIndex == 5)
            {
                ProductLV.ItemsSource = services;
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Service service)
            {
                Service selectedService = service;

                EditServiceWindow editServiceWindow = new EditServiceWindow(selectedService);
                editServiceWindow.ShowDialog();

                Refresh();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Service service)
            {
                DBConnection.schoolPractice.Service.Remove(service);
                DBConnection.schoolPractice.SaveChanges();

                Refresh();
            }
        }

        private void SearchTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void FiltrSaleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void FiltrCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();

            
        }

        private void AddServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            AddServiceWindow addServiceWindow = new AddServiceWindow();
            addServiceWindow.ShowDialog();

            Refresh();
        }
    }
}

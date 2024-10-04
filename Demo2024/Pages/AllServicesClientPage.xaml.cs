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

namespace Demo2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllServicesClientPage.xaml
    /// </summary>
    public partial class AllServicesClientPage : Page
    {
        public static List<Service> services { get; set; }
        public static List<ClientService> clientServices { get; set; }
        public AllServicesClientPage()
        {
            InitializeComponent();
            Refresh(0);
            FiltrSaleCb.SelectedIndex = 0;


            services = new List<Service>(DBConnection.schoolPractice.Service.ToList());
            clientServices = new List<ClientService>(DBConnection.schoolPractice.ClientService.ToList());
            ProductLV.ItemsSource = services;
            this.DataContext = this;
        }

        private void Refresh(int i)
        {
            var filtred = DBConnection.schoolPractice.Service.ToList();
            var allService = DBConnection.schoolPractice.Service.ToList();
            var searchText = SearchTbx.Text.ToLower();
            if (FiltrSaleCb != null && FiltrSaleCb.SelectedIndex == 1)
                filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount0).ToList();
            if (FiltrSaleCb != null && FiltrSaleCb.SelectedIndex == 2)
                filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount5).ToList();
            if (FiltrSaleCb != null && FiltrSaleCb.SelectedIndex == 3)
                filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount15).ToList();
            if (FiltrSaleCb != null && FiltrSaleCb.SelectedIndex == 4)
                filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount30).ToList();
            if (FiltrSaleCb != null && FiltrSaleCb.SelectedIndex == 5)
                filtred = filtred.Where(f => Convert.ToString(f.Discount.Value) == f.Discount70).ToList();
            if (string.IsNullOrWhiteSpace(searchText) == false)
                filtred = filtred.Where(f => f.Title.ToLower().Contains(searchText) || (f.Description != null && f.Description.ToLower().Contains(searchText))).ToList();
            if (FiltrCb.SelectedIndex == 2)
                filtred = filtred.OrderBy(f => f.NewCost).ToList();
            if (FiltrCb.SelectedIndex == 1)
                filtred = filtred.OrderByDescending(f => f.NewCost).ToList();
            ProductLV.ItemsSource = filtred.ToList();
            CountProductTbl.Text = $"{filtred.Count} из {allService.Count}";
        }

        private void Refresh1()
        {
            ProductLV.ItemsSource = DBConnection.schoolPractice.Service.ToList();
        }

        private void SearchTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh(0);
        }

        private void FiltrSaleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh(0);
        }

        private void FiltrCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh(0);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BeginPage());
        }
    }
}

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

namespace Demo2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для BeginPage.xaml
    /// </summary>
    public partial class BeginPage : Page
    {
        public BeginPage()
        {
            InitializeComponent();
        }

        private void AdminBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WelcomePage());
        }

        private void UserBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllServicesClientPage());
        }

        private void AdminBtn_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WelcomePage());
        }

        private void UserBtn_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllServicesClientPage());
        }
    }
}

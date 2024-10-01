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
    /// Логика взаимодействия для WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void authBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = EmailTb.Text.Trim();
                int password = int.Parse(PasswordTb.Password);

                if (email == "0000" && password == 0)
                {
                    MessageBox.Show("Добро пожаловать, Администратор!", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new AllServicesPage());
                }

                else
                {
                    MessageBox.Show("Неверный логин или пароль! Попробуйте снова. ", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка подключения. ");
            }
        }
    }
}

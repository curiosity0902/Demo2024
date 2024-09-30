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
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        public static List<Service> services { get; set; }

        public static Service service = new Service();
        public AddServiceWindow()
        {
            InitializeComponent();
            services = new List<Service>(DBConnection.schoolPractice.Service.ToList());
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {
                StringBuilder error = new StringBuilder();
                if (string.IsNullOrWhiteSpace(NameTb.Text) || string.IsNullOrWhiteSpace(DescriptionTb.Text))

                {
                    error.AppendLine("Заполните все поля!");
                }

                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    service.Title = NameTb.Text.Trim();
                    service.Description = DescriptionTb.Text.Trim();
                    service.DurationInSeconds = int.Parse(DurationTb.Text.Trim());
                    service.Discount = int.Parse(SaleTb.Text.Trim());
                    service.Cost = int.Parse(CostTb.Text.Trim());

                    DBConnection.schoolPractice.Service.Add(service);
                    DBConnection.schoolPractice.SaveChanges();
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

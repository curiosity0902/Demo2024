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
    /// Логика взаимодействия для EditServiceWindow.xaml
    /// </summary>
    public partial class EditServiceWindow : Window
    {
        public static List<Service> services { get; set; }

        Service contextService;
        public EditServiceWindow(Service service)
        {
            InitializeComponent();

            contextService = service;
            services = new List<Service>(DBConnection.schoolPractice.Service.ToList());

            NameTb.Text = contextService.Title;
            DescriptionTb.Text = contextService.Description;
            CostTb.Text = (contextService.NewCost).ToString();
            SaleTb.Text = (contextService.Discount).ToString();
            DurationTb.Text = (contextService.DurationInSeconds).ToString();
        }

        private void RedactBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                Service service = contextService;
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
                    service.Title = NameTb.Text;
                    service.Description = DescriptionTb.Text;
                    service.Cost = int.Parse(CostTb.Text);
                    service.Discount = int.Parse(SaleTb.Text);
                    service.DurationInSeconds = int.Parse(DurationTb.Text);
                    DBConnection.schoolPractice.SaveChanges();

                    NameTb.Text = String.Empty;
                    SaleTb.Text = String.Empty;
                    DurationTb.Text = String.Empty;
                    DescriptionTb.Text = String.Empty;
                    CostTb.Text = String.Empty;

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

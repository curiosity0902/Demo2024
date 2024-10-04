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

                string title = DBConnection.schoolPractice.Service.ToString();

                if (string.IsNullOrWhiteSpace(NameTb.Text) || (DurationTb.Text.Trim() == null) || 
                    (CostTb.Text.Trim() == null))

                {
                    error.AppendLine("Заполните все поля!");
                }

                if (int.Parse(DurationTb.Text) > 240)
                {
                    MessageBox.Show("Длительность не может быть больше 4 часов!");
                }

                //if (int.Parse(NameTb.Text == DBConnection)
                //{
                //    MessageBox.Show("Длительность не может быть больше 4 часов!");
                //}

                if (int.Parse(DurationTb.Text) <= 0)
                {
                    MessageBox.Show("Длительность не может быть отрицательной!");
                }

                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    service.Title = NameTb.Text.Trim();
                    service.Description = DescriptionTb.Text.Trim();
                    service.DurationInSeconds = int.Parse(DurationTb.Text) * 60;
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

        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);

        }
        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);

        }

        private void AddPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = $"/Услуги школы/{openFileDialog.SafeFileName}";

                PhotoService.Source = new BitmapImage(new Uri(selectedImagePath, UriKind.Relative));

                service.MainImagePath = selectedImagePath;

                
            }
            DBConnection.schoolPractice.SaveChanges();
           
        }
    }
}

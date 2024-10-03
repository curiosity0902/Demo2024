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

            PhotoService.Source =  new BitmapImage(new Uri(service.MainImagePath, UriKind.Relative));
            NameTb.Text = contextService.Title;
            DescriptionTb.Text = contextService.Description;
            CostTb.Text = (contextService.NewCost).ToString();
            SaleTb.Text = (contextService.Discount).ToString();
            DurationTb.Text = (contextService.DurationInSeconds / 60).ToString();
        }

        private void RedactBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();
                Service service = contextService;
                if (string.IsNullOrWhiteSpace(NameTb.Text))
             
                {
                    error.AppendLine("Заполните все поля!");
                }

                if (error.Length > 0)
                {
                    MessageBox.Show(error.ToString());
                }

                if (int.Parse(DurationTb.Text) > 240)
                {
                    MessageBox.Show("Длительность не может быть больше 4 часов!");
                }

                if (int.Parse(SaleTb.Text) > 100)
                {
                    MessageBox.Show("Скидка не может быть больше 100!");
                }

                else
                {
                    service.Title = NameTb.Text;
                    service.Description = DescriptionTb.Text;
                    service.Cost = int.Parse(CostTb.Text);
                    service.Discount = int.Parse(SaleTb.Text);
                    service.DurationInSeconds = int.Parse(DurationTb.Text) * 60;
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
            // Создаем диалог для выбора файла
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"; // Фильтр для изображений

            // Показываем диалог и проверяем, выбрал ли пользователь файл
            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем путь к выбранному файлу
                string selectedImagePath = $"/Услуги школы/{openFileDialog.SafeFileName}";

                // Устанавливаем источник изображения в элемент Image
                PhotoService.Source = new BitmapImage(new Uri(selectedImagePath, UriKind.Relative));

                // Обновляем MainImagePath объекта Service
                contextService.MainImagePath = selectedImagePath;

                // Сохраняем изменения в базе данных
                DBConnection.schoolPractice.SaveChanges();
            }
        }
    }
    
}

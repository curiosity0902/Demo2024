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
    /// Логика взаимодействия для AllPhotoServiceWindow.xaml
    /// </summary>
    public partial class AllPhotoServiceWindow : Window
    {

        public static List<Service> services { get; set; }
        public static List<ServicePhoto> servicePhotos { get; set; }
        public static ServicePhoto servicePhoto = new ServicePhoto();
        Service contextService;

        // Камилла, если ты это читаешь, знай, что ты лох:)
        public AllPhotoServiceWindow(Service service)
        {
            InitializeComponent();
            contextService = service;
            services = new List<Service>(DBConnection.schoolPractice.Service.ToList());
            servicePhotos = new List<ServicePhoto>(DBConnection.schoolPractice.ServicePhoto
                .Where(p => p.ServiceID == contextService.ID)
                .ToList());

            this.DataContext = this;
        }
        private void LoadAdditionalPhotos()
        {
            servicePhotos = DBConnection.schoolPractice.ServicePhoto.Where(p => p.ServiceID == contextService.ID)
                .Select(p => new ServicePhoto { PhotoPath = p.PhotoPath }).ToList();

  
            ServicePhotoLv.ItemsSource = null;
            ServicePhotoLv.ItemsSource = servicePhotos;
        }
        private void AlsoPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = $"/Услуги школы/{openFileDialog.SafeFileName}";

                var newPhoto = new ServicePhoto
                {
                    ServiceID = contextService.ID,
                    PhotoPath = selectedImagePath
                };
                DBConnection.schoolPractice.ServicePhoto.Add(newPhoto);
                DBConnection.schoolPractice.SaveChanges();

                servicePhotos.Add(newPhoto);
                ServicePhotoLv.ItemsSource = null; 
                ServicePhotoLv.ItemsSource = servicePhotos; 
            }
        }

        public void Refresh()
        {
            ServicePhotoLv.ItemsSource = DBConnection.schoolPractice.ServicePhoto.ToList();
        }
        private void DeletePhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ServicePhotoLv.SelectedItem is ServicePhoto servicePhoto)
            {
                DBConnection.schoolPractice.ServicePhoto.Remove(servicePhoto);
                DBConnection.schoolPractice.SaveChanges();
            }
            Refresh();
            
        }
    }
}

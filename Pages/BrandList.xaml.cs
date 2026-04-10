using prakt15_Leshukov_TRPO.Models;
using prakt15_Leshukov_TRPO.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace prakt15_Leshukov_TRPO.Pages
{
    /// <summary>
    /// Логика взаимодействия для CategoriesList.xaml
    /// </summary>
    public partial class BrandList : Page
    {
        private BrandService service { get; set; } = new BrandService();
        public ObservableCollection<Brand> brands { get; set; } = new ObservableCollection<Brand>();
        public Brand brand { get; set; } = new Brand();
        public Brand selectedBrand { get; set; }
        public BrandList()
        {
            InitializeComponent();
            foreach (var brand in service.Brands)
            {
                brands.Add(brand);
            }
            DataContext = this;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (selectedBrand != null)
            {
                brand = selectedBrand;
                DataContext = null;
                DataContext = this;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(brandName))
            {
                MessageBox.Show("Заполните поля верно");
                return;
            }
            if (selectedBrand != null)
            {
                if (!string.IsNullOrEmpty(brand.Name))
                    service.Commit();
                else
                {
                    MessageBox.Show("Заполните поля");

                    return;
                }
            }
            else
            {
                if(!string.IsNullOrEmpty(brand.Name))
                    service.Add(brand);
                else
                {
                    MessageBox.Show("Заполните поля" );
                    return;
                }
            }
            NavigationService.GoBack();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            selectedBrand = null;
            brand = new Brand();
            DataContext = null;
            DataContext = this;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            
            NavigationService.GoBack();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительное хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            if (selectedBrand != null)
            {
                service.Remove(selectedBrand);
                brands.Remove(selectedBrand);
                DataContext = null;
                DataContext = this;
            }
        }
    }
}

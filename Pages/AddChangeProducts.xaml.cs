using MaterialDesignThemes.Wpf;
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
    /// Логика взаимодействия для AddChangeProducts.xaml
    /// </summary>
    public partial class AddChangeProducts : Page
    {
        public Product product { get; set; } = new Product();
        private ProductService service { get; set; } = new ProductService();
        public Category selectedCategory { get; set; }
        public Brand selectedBrand { get; set; }
        private CategoryService Categoryservice { get; set; } = new CategoryService();
        private BrandService Brandservice { get; set; } = new BrandService();
        public ObservableCollection<Category> categories { get; set; } = new ObservableCollection<Category>();
        
        public ObservableCollection<Brand> brands { get; set; } = new ObservableCollection<Brand>();
        bool isEdit = false;
        public AddChangeProducts(Product _product)
        {
            InitializeComponent();

            foreach(var category in Categoryservice.Categories)
            {
                categories.Add(category);
               
            }
            foreach(var brand in Brandservice.Brands)
            {
                brands.Add(brand);
                
            }

            if(_product != null)
            {
                product = _product;
                selectedBrand = product.Brand;
                selectedCategory = product.Category;
                isEdit = true;
            }
           
            DataContext = this;
            
        }

        

        private void Save(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(name) ||
               Validation.GetHasError(desc) ||
               Validation.GetHasError(price) ||
               Validation.GetHasError(count) ||
               Validation.GetHasError(rate) ||
                 Validation.GetHasError(date) ||
               Validation.GetHasError(category)||
                Validation.GetHasError(brand)
               )
            {
                MessageBox.Show("Заполните поля верно");
                return;
            }
            //if (string.IsNullOrWhiteSpace(product.Name) ||
            //    string.IsNullOrWhiteSpace(product.Description) ||
            //    product.Price <= 0 ||  
            //    product.Raiting < 0 || product.Raiting > 5 || 
            //    product.Brand == null ||
            //    product.Category == null ||
            //    product.Stock < 0)
            //{
            //    MessageBox.Show("Заполните поля верно");
            //    return;
            //}
            if (selectedCategory != null)
                product.CategoryId = selectedCategory.Id;

            if (selectedBrand != null)
                product.BrandId = selectedBrand.Id;
            if (isEdit)
            {
                service.Commit();
            }
            else
            {
                service.Add(product);
            }
               
            NavigationService.GoBack();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

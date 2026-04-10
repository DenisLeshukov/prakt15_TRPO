using prakt15_Leshukov_TRPO.Models;
using prakt15_Leshukov_TRPO.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    /// Логика взаимодействия для BrandsList.xaml
    /// </summary>
    public partial class CategoryList : Page
    {
        private CategoryService service { get; set; } = new CategoryService();
        public ObservableCollection<Category> categories { get; set; } = new ObservableCollection<Category>();
        public Category category { get; set; } = new Category();
        public Category selectedCategory { get; set; }
        public CategoryList()
        {
            InitializeComponent();
            foreach (var category in service.Categories)
            {
                categories.Add(category);
            }
            DataContext = this;
            
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (selectedCategory != null)
            {
                category = selectedCategory;
                DataContext = null;
                DataContext = this;
            }
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            selectedCategory = null;
            category = new Category();
            DataContext = null;
            DataContext = this;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(categoryName))
            {
                MessageBox.Show("Заполните поля верно");
                return;
            }
            if(selectedCategory != null)
            {
                service.Commit();
               
            }
            else {
                
                service.Add(category);
               

            }
            NavigationService.GoBack();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительное хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            if (selectedCategory != null)
            {
                service.Remove(selectedCategory);
                categories.Remove(selectedCategory);
                
            }
        }
    }
}

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
using static MaterialDesignThemes.Wpf.Theme;

namespace prakt15_Leshukov_TRPO.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddTegToProduct.xaml
    /// </summary>
    public partial class AddTegToProduct : Page
    {
        public TagService TagService { get; set; } = new TagService();
        public Tag selectedTag { get; set; }
        public Tag tagForProduct { get; set; }
        public Product product { get; set; } = new Product();
        public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();
        public ObservableCollection<Tag> remainingTags { get; set; } = new ObservableCollection<Tag>();
        public AddTegToProduct(Product _product)
        {
            InitializeComponent();
            foreach (Tag tag in _product.Tags)
            {
                Tags.Add(tag);
            }
           
            foreach (var tag in TagService.Tags)
            {
                if(!Tags.Contains(tag))
                    remainingTags.Add(tag);
            }
            if(_product !=null)
                product = _product;
            DataContext = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as System.Windows.Controls.ComboBox;
            if (comboBox != null)
                comboBox.SelectionChanged -= ComboBox_SelectionChanged;
            if (tagForProduct != null)
            {
                Tags.Add(tagForProduct);
                product.Tags.Add(tagForProduct);
                
                TagService.Commit();
            }
            remainingTags.Clear();
          
            foreach (var tag in TagService.Tags)
            {
                if (!Tags.Contains(tag))
                    remainingTags.Add(tag);
            }
            if (comboBox != null)
                comboBox.SelectionChanged += ComboBox_SelectionChanged;

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительное хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            if (selectedTag != null)
            {
                product.Tags.Remove(selectedTag);
                Tags.Remove(selectedTag);
            }
        }
    }
}

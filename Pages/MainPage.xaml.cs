using Microsoft.IdentityModel.Tokens;
using prakt15_Leshukov_TRPO.Models;
using prakt15_Leshukov_TRPO.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage :Page
    {
       
        public ObservableCollection<Product> products { get; set; } = new();
        public ProductService service { get; set; } = new();
        public ICollectionView userView { get; set; }
        public string searchQuery { get; set; } = null!;
        public string filterPriceFrom { get; set; } = null!;
        public string filterPriceTo { get; set; } = null!;

        public ComboBoxItem selectedCb { get; set; } = new ComboBoxItem(); 

        public MainPage ()
        {
           
            InitializeComponent( );
            foreach (var product in service.Products)
            {
                products.Add(product);
            }
            
            userView = CollectionViewSource.GetDefaultView(products);
            userView.Filter = FilterForms;
            DataContext = this;
        }
        public bool FilterForms(object obj)
        {
            if (obj is not Product)
                return false;
            var product = (Product)obj;
           
           
            if (!filterPriceFrom.IsNullOrEmpty() && Convert.ToInt32(filterPriceFrom) > product.Price)
                return false;
            
            if (!filterPriceTo.IsNullOrEmpty() && Convert.ToInt32(filterPriceTo) < product.Price)
                return false;
            if (!string.IsNullOrEmpty(searchQuery))
            {
                bool matches = false;

                switch (selectedCb.Tag?.ToString())
                {
                    case "Brand":
                        matches = product.Brand.Name.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase);
                        break;
                    case "Category":
                        matches = product.Category.Name.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase);
                       
                        break;
                    default:
                        matches = product.Name.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase);
                        break;
                }

                if (!matches)
                    return false;
            }

            return true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userView.Refresh();
        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if(selectedCb.Tag != null)
            {
                if (selectedCb.Tag.ToString() == "Price")
                {
                    userView.Refresh();
                }
                else
                {
                    MessageBox.Show("Сначала выберите фильтр по цене!");
                    tb.Clear();
                    return;
                }
            }
            else
            {
                
                MessageBox.Show("Сначала выберите фильтр по цене!");
                tb.Clear();
                return;
            }
                
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userView.SortDescriptions.Clear();
            var cb = (ComboBox)sender;
            var selected = (ComboBoxItem)cb.SelectedItem;
            switch (selected.Tag)
            {
                case "Name":
                    userView.SortDescriptions.Add(new SortDescription("Name",
                    ListSortDirection.Ascending));
                    break;
                case "PriceAscend":
                    userView.SortDescriptions.Add(new SortDescription("Price",
                    ListSortDirection.Ascending));
                    break;
                case "CountAscend":
                    userView.SortDescriptions.Add(new SortDescription("Stock",
                    ListSortDirection.Ascending));
                    break;
                case "CountDescend":
                    userView.SortDescriptions.Add(new SortDescription("Stock",
                    ListSortDirection.Descending));
                    break;
                case "PriceDescend":
                    userView.SortDescriptions.Add(new SortDescription("Price",
                    ListSortDirection.Descending));
                    break;
            }
            userView.Refresh();
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            searchQuery = "";
            filterPriceFrom = "";
            filterPriceTo = "";
            selectedCb = null;
        }
        // кнопка сбросить, менеджер и его CRUD, если товара меньше 10 - обводка цветом
    }
}

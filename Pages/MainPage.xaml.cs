using Microsoft.IdentityModel.Tokens;
using prakt15_Leshukov_TRPO.Models;
using prakt15_Leshukov_TRPO.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class MainPage :Page, INotifyPropertyChanged
    {
        private string _selectedSearchType;
        private bool _isCategoryVisible;
        private bool _isBrandVisible;
        private bool _isPriceVisible;
        private string _filterPriceFrom;
        private string _filterPriceTo;
        public string filterPriceFrom
        {
            get => _filterPriceFrom;
            set
            {
                _filterPriceFrom = value;
                OnPropertyChanged();
                userView?.Refresh();
            }
        }

       
        public string filterPriceTo
        {
            get => _filterPriceTo;
            set
            {
                _filterPriceTo = value;
                OnPropertyChanged();
                userView?.Refresh();
            }
        }
        public string categoryValue { get; set; } = null;
        public string brandValue { get; set; } = null;
        public string SelectedSearchType
        {
            get => _selectedSearchType;
            set
            {
                _selectedSearchType = value;
                OnPropertyChanged();
                UpdateVisibility();
                categoryValue = "";
                brandValue = "";
                filterPriceFrom = "";
                filterPriceTo = "";

                userView?.Refresh();
            }
        }

        public bool IsCategoryVisible
        {
            get => _isCategoryVisible;
            set { _isCategoryVisible = value; OnPropertyChanged(); }
        }

        public bool IsBrandVisible
        {
            get => _isBrandVisible;
            set { _isBrandVisible = value; OnPropertyChanged(); }
        }

        public bool IsPriceVisible
        {
            get => _isPriceVisible;
            set { _isPriceVisible = value; OnPropertyChanged(); }
        }
       
        private bool isManger;
        public bool IsManger
        {
            get => isManger;
            set
            {
                isManger = value;
            }
        }
        public Visibility ButtonVisibility => isManger ? Visibility.Visible : Visibility.Collapsed;
        public ObservableCollection<Product> products { get; set; } = new();
        public ProductService service { get; set; } = new();
        public ICollectionView userView { get; set; }
        public string searchQuery { get; set; } = null!;
       
        public Product selectedItem { get; set; }

        public ComboBoxItem selectedCb { get; set; } = new ComboBoxItem();

        private void UpdateVisibility()
        {
            IsCategoryVisible = false;
            IsBrandVisible = false;
            IsPriceVisible = false;

            if (SelectedSearchType == "Category")
            {
                IsCategoryVisible = true;
                userView?.Refresh(); 
            }
            else if (SelectedSearchType == "Brand")
            {
                IsBrandVisible = true;
                userView?.Refresh(); 
            }
            else if (SelectedSearchType == "Price")
            {
                IsPriceVisible = true;
                userView?.Refresh(); 
            }
        }

        public MainPage (bool _isManager)
        {
           
            InitializeComponent( );
            foreach (var product in service.Products)
            {
                products.Add(product);
            }
            if (_isManager == false)
                isManger = false;
            else 
                isManger = true;

            userView = CollectionViewSource.GetDefaultView(products);
            
            userView.Filter = FilterForms;
            DataContext = this;
        }
        public bool FilterForms(object obj)
        {
            if (obj is not Product product)
                return false;

           
            if (!string.IsNullOrEmpty(searchQuery))
            {
                if (!product.Name.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase))
                    return false;
            }

           
            if (SelectedSearchType == "Category" && !string.IsNullOrEmpty(categoryValue))
            {
                if (!product.Category.Name.Contains(categoryValue, StringComparison.CurrentCultureIgnoreCase))
                    return false;
            }

           
            if (SelectedSearchType == "Brand" && !string.IsNullOrEmpty(brandValue))
            {
                if (!product.Brand.Name.Contains(brandValue, StringComparison.CurrentCultureIgnoreCase))
                    return false;
            }

           
            if (SelectedSearchType == "Price")
            {
                if (!string.IsNullOrEmpty(filterPriceFrom))
                {
                    if (decimal.TryParse(filterPriceFrom, out decimal from))
                    {
                        if (product.Price < from)
                            return false;
                    }
                }

                if (!string.IsNullOrEmpty(filterPriceTo))
                {
                    if (decimal.TryParse(filterPriceTo, out decimal to))
                    {
                        if (product.Price > to)
                            return false;
                    }
                }
            }

            return true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userView.Refresh();
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
            categoryValue = "";
            brandValue = "";
            filterPriceFrom = "";
            filterPriceTo = "";
            DataContext = null;
            DataContext = this;
            comboBoxI.SelectedIndex = 0;
            userView.SortDescriptions.Clear( );
            selectedCb = null;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(isManger == true)
            {
                if(selectedItem !=null)
                    NavigationService.Navigate(new AddChangeProducts(selectedItem));
                else
                    NavigationService.Navigate(new AddChangeProducts(null));
            }
        }

        private void goCategory(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoryList());
        }

        private void goProduct(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddChangeProducts(null));
        }

        private void goBrand(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BrandList());
        }

        private void goTag(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TagList());
        }

        private void goTagToProdutc(object sender, RoutedEventArgs e)
        {
            if (selectedItem != null){
                NavigationService.Navigate(new AddTegToProduct(selectedItem));
            }
            

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CategoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                categoryValue = textBox.Text;
                userView?.Refresh();
            }
        }

        private void BrandTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                brandValue = textBox.Text;
                userView?.Refresh();
            }
        }

        private void PriceToTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox1 = sender as TextBox;
            if (textBox1 != null)
            {
                filterPriceFrom = textBox1.Text;
                userView?.Refresh();
            }
        }

        private void PriceFromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                filterPriceTo = textBox.Text;
                userView?.Refresh();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительное хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            if(selectedItem != null)
            {
                service.Remove(selectedItem);
                userView?.Refresh();
            }
        }
        // дизайн по заданию
    }
}

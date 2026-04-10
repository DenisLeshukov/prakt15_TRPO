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
    /// Логика взаимодействия для TagList.xaml
    /// </summary>
    public partial class TagList : Page
    {
        private TagService service { get; set; } = new TagService();
        public ObservableCollection<Tag> tags { get; set; } = new ObservableCollection<Tag>();
        public Tag tag { get; set; } = new Tag();
        public Tag selectedTag { get; set; }
        public TagList()
        {
            InitializeComponent();
            foreach (var tag in service.Tags)
            {
                tags.Add(tag);
            }
            DataContext = this;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (selectedTag != null)
            {
                tag = selectedTag;
                DataContext = null;
                DataContext = this;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(brand))
            {
                MessageBox.Show("Заполните поля верно");
                return;
            }
            if (selectedTag != null)
            {
                if (!string.IsNullOrEmpty(tag.Name))
                {
                    service.Commit( );
                }
                else
                {
                    MessageBox.Show("Заполните поля верно");
                    return;
                }
                
            }
            else
            {
                if (!string.IsNullOrEmpty(tag.Name)) 
                    service.Add(tag);
                else
                {
                    MessageBox.Show("Заполните поля верно");
                    return;
                }
            }
            NavigationService.GoBack();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            selectedTag = null;
            tag = new Tag();
            DataContext = null;
            DataContext = this;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

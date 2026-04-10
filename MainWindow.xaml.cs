using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using prakt15_Leshukov_TRPO.Pages;

namespace prakt15_Leshukov_TRPO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {
        public MainWindow ()
        {
            
            InitializeComponent( );
            MainFrame.Navigate( new SignInPage());
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as Page;

            if (page != null)
            {
                
                if (page is MainPage)
                    this.Title = "Главная страница";
                else if (page is CategoryList)
                    this.Title = "Категории";
                else if (page is BrandList)
                    this.Title = "Бренды";
                else if (page is TagList)
                    this.Title = "Теги";
                else if (page is AddChangeProducts)
                    this.Title = "Редактирование/Добавление продукта";
                else if (page is AddTegToProduct)
                    this.Title = "Добавление тегов к продукту";
                else
                    this.Title = "Вход";
            }
        }
    }
}
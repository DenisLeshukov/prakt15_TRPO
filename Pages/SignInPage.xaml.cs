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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prakt15_Leshukov_TRPO.Pages
{
    /// <summary>
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage :Page
    {
        public int password { get; set; }
        public SignInPage ()
        {
            InitializeComponent( );
            DataContext = this;
        }

        private void SignManager (object sender, RoutedEventArgs e)
        {
            if(password == 1234)
            {
                NavigationService.Navigate( new MainPage());
            }
        }

        private void SignUser (object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
        }
    }
}

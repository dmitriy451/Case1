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

namespace Vending
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
            Terminal.frame = frame;
        }

        private void TextBlock_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            frame.Navigate(new AdminFunc.Moneta());
            while (frame.NavigationService.RemoveBackEntry() != null) ;
        }

        private void TextBlock_MouseEnter_1(object sender, MouseButtonEventArgs e)
        {
            frame.Navigate(new AdminFunc.Drinks());
            while (frame.NavigationService.RemoveBackEntry() != null) ;
        }

        private void TextBlock_MouseEnter_2(object sender, MouseButtonEventArgs e)
        {
            frame.Navigate(new AdminFunc.Report());
            while (frame.NavigationService.RemoveBackEntry() != null) ;
        }
        
    }
}

using HelloApp1.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HelloApp1.Views
{
    public partial class GreetingPage : Page
    {
        public GreetingPage()
        {
            InitializeComponent();
            DataContext = new GreetingPageViewModel();
        }
    }
}

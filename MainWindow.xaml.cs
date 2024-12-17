using HelloApp1.Views;
using System.Windows;

namespace HelloApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GreetingFrame.Content = new GreetingPage();
        }
    }
}
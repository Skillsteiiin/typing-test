using HelloApp1.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HelloApp1.Views
{
    public partial class UserLearning2 : Page
    {
        public UserLearning2()
        {
            InitializeComponent();
            DataContext = new UserLearningViewModel(3);
        }
    }
}
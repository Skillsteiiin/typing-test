using HelloApp1.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HelloApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для UserLearning1.xaml
    /// </summary>
    public partial class UserLearning1 : Page
    {
        public UserLearning1()
        {
            InitializeComponent();
            DataContext = new UserLearningViewModel(3);
        }
    }
}

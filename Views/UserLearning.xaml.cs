using HelloApp1.ViewModels;
using System.Windows.Controls;

namespace HelloApp1.Views
{
    public partial class UserLearning : Page
    {
        public UserLearning()
        {
            InitializeComponent();
            DataContext = new UserLearningViewModel(2);
        }
    }
}

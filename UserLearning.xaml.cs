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

namespace HelloApp1
{
    public partial class UserLearning : Page
    {
        public UserLearning()
        {
            InitializeComponent();
        }

        private void GoFirstUserLearning(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserLearningPages.UserLearning1());
        }
        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("UserLearningPages/UserProgress.xaml", UriKind.Relative));
        }

        private void UserEducation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("UserLearningPages/UserEducation.xaml", UriKind.Relative));
        }
    }
}

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

namespace HelloApp1.UserLearningPages
{
    public partial class UserLearning2 : Page
    {
        public UserLearning2()
        {
            InitializeComponent();
        }

        private void GoToUserPractise(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserLearningPages.UserEducation());
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
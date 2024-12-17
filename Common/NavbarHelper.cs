using HelloApp1.Views;
using System.Windows.Input;
using System.Windows.Navigation;

namespace HelloApp1.Common
{
    public static class NavbarHelper
    {
        public static ICommand UserLearningCommandOne()
        {
            return new RelayCommand<NavigationService>(
                execute: (parameter) =>
                {
                    if (parameter is NavigationService navigationService)
                    {
                        navigationService?.Navigate(new Uri("../Views/UserLearning1.xaml", UriKind.Relative));
                    }
                });
        }

        public static ICommand UserLearningCommandTwo()
        {
            return new RelayCommand<NavigationService>(
                execute: (parameter) =>
                {
                    if (parameter is NavigationService navigationService)
                    {
                        navigationService?.Navigate(new Uri("../Views/UserLearning2.xaml", UriKind.Relative));
                    }
                });
        }

        public static ICommand UserLearningCommand()
        {
            return new RelayCommand<NavigationService>(
                execute: (parameter) =>
                {
                    if (parameter is NavigationService navigationService)
                    {
                        navigationService.Navigate(new UserLearning());
                    }
                });
        }

        public static ICommand ProgressButtonCommand()
        {
            return new RelayCommand<NavigationService>(
                execute: (parameter) =>
                {
                    if (parameter is NavigationService navigationService)
                    {
                        navigationService.Navigate(new UserProgress());
                    }
                });
        }

        public static ICommand UserEducationCommand()
        {
            return new RelayCommand<NavigationService>(
                execute: (parameter) =>
                {
                    if (parameter is NavigationService navigationService)
                    {
                        navigationService.Navigate(new UserEducation());
                    }
                });
        }
    }
}

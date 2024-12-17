using HelloApp1.Common;
using HelloApp1.Views;
using System.Windows.Input;
using System.Windows.Navigation;

namespace HelloApp1.ViewModels
{
    public class GreetingPageViewModel
    {
        public ICommand GoToUserLearningCommand { get; }

        public GreetingPageViewModel()
        {
            GoToUserLearningCommand = new RelayCommand<NavigationService>(
                execute: (parameter) =>
                {
                    if (parameter is NavigationService navigationService)
                    {
                        navigationService.Navigate(new UserLearning());
                    }
                },
                canExecute: (parameter) => true 
            );
        }
    }
}

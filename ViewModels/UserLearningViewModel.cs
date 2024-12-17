using HelloApp1.Common;
using HelloApp1.Views;
using System.Windows.Input;
using System.Windows.Navigation;

namespace HelloApp1.ViewModels
{
    public class UserLearningViewModel
    {
        public ICommand PushUserLearningCommand { get; }
        public ICommand ProgressButtonCommand { get; }
        public ICommand UserEducationCommand { get; }

        public UserLearningViewModel(int numberLearning)
        {
            PushUserLearningCommand = numberLearning switch
            {
               1 => NavbarHelper.UserLearningCommand(),
               2 => NavbarHelper.UserLearningCommandOne(), 
               3 => NavbarHelper.UserLearningCommandTwo()
            };

            ProgressButtonCommand = NavbarHelper.ProgressButtonCommand();

            UserEducationCommand = NavbarHelper.UserEducationCommand();
        }
    }
}

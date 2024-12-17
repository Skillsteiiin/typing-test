using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using System.Text.Json;
using System.IO;
using HelloApp1.Common;
using HelloApp1.ViewModels;

namespace HelloApp1.Views
{
    public partial class UserEducation : Page
    {
        private UserEducationViewModel viewModel;
        public UserEducation()
        {
            InitializeComponent();
            viewModel = new UserEducationViewModel();
            DataContext = viewModel;
            ((UserEducationViewModel)DataContext).PropertyChanged += ViewModel_PropertyChanged;
            viewModel.ValidateCharsUpdated += ViewModel_ValidateCharsUpdated;
        }

        private void ViewModel_ValidateCharsUpdated(object? sender, List<ValidateChar> e)
        {
            TextToTypeBlock.Inlines.Clear();
            for(int i = 0; i < e.Count; i++)
            {
                var run = new Run(viewModel.TextToType[i].ToString());

                if (i < e.Where(x => x.Valid !=null).Count())
                {
                    if (e[i].Valid.Value)
                    {
                        run.Foreground = Brushes.LimeGreen;
                    }
                    else
                    {
                        if (e[i].Char == ' ')
                        {
                            run.Text = "_";
                        }
                        run.Foreground = Brushes.Red;
                        
                    }
                }
                else
                {
                    run.Foreground = Brushes.White;
                }

                TextToTypeBlock.Inlines.Add(run);
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserEducationViewModel.TestResultsMessage))
            {
                var viewModel = (UserEducationViewModel)sender;
                if (!string.IsNullOrEmpty(viewModel.TestResultsMessage))
                {
                    MessageBox.Show(viewModel.TestResultsMessage, "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}

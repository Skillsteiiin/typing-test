using System.Windows.Controls;
using HelloApp1.Common;
using HelloApp1.ViewModels;
using LiveCharts;
using LiveCharts.Defaults;
using static HelloApp1.Views.UserEducation;

namespace HelloApp1.Views
{
    public partial class UserProgress : Page
    {
        private List<UserResult> _results;

        public UserProgress()
        {
            InitializeComponent();
            LoadResults();
            DataContext = new UserProgressViewModel(_results); // DataContext для привязки данных
        }

        private void LoadResults()
        {
            // Загрузка результатов с использованием UserResultsManager
            var resultsManager = new UserResultsManager();
            _results = resultsManager.LoadResults();
        }

        

    }
}
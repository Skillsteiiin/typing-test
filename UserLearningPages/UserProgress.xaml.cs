using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using LiveCharts;
using LiveCharts.Defaults;

namespace HelloApp1.UserLearningPages
{
    public partial class UserProgress : Page
    {
        private List<UserEducation.UserResult> _results;

        public UserProgress()
        {
            InitializeComponent();
            LoadResults();
            DisplayResults();
            DataContext = new UserProgressViewModel(_results); // DataContext для привязки данных
        }

        private void LoadResults()
        {
            // Загрузка результатов с использованием UserResultsManager
            var resultsManager = new UserEducation.UserResultsManager();
            _results = resultsManager.LoadResults();
        }

        private void DisplayResults()
        {
            /*if (_results == null || !_results.Any())
            {
                ResultsTextBlock.Text = "Результатов пока нет.";
                return;
            }

            // Формируем текст для отображения
            var resultsText = new StringBuilder();
            foreach (var result in _results)
            {
                resultsText.AppendLine(
                    $"Дата: {result.TestDate:dd.MM.yyyy HH:mm}, " +
                    $"Скорость: {result.SpeedWPM} WPM, " +
                    $"Время: {result.TimeElapsed:mm\\:ss}, " +
                    $"Правильных символов: {result.CorrectCharacters}"
                );
            }
            ResultsTextBlock.Text = resultsText.ToString();*/
        }

        public class UserProgressViewModel
        {
            public ChartValues<ObservableValue> SpeedData { get; set; }
            public ChartValues<ObservableValue> TimeElapsed { get; set; }
            public ChartValues<ObservableValue> CorrectCharacters { get; set; }
            public ChartValues<ObservableValue> TestDate { get; set; }

            public Func<double, string> DateFormatter { get; set; }

            public UserProgressViewModel(List<UserEducation.UserResult> results)
            {
                // Инициализация данных
                SpeedData = new ChartValues<ObservableValue>(results.Select(r => new ObservableValue(r.SpeedWPM)).ToList());
                TimeElapsed = new ChartValues<ObservableValue>(results.Select(r => new ObservableValue(r.TimeElapsed.TotalSeconds)).ToList());
                CorrectCharacters = new ChartValues<ObservableValue>(results.Select(r => new ObservableValue(r.CorrectCharacters)).ToList());

                // Используем индексы или временные метки для оси X
                TestDate = new ChartValues<ObservableValue>(results.Select((r, index) => new ObservableValue(index)).ToList()); // Индексы для оси X

                // Инициализация форматтера даты
                DateFormatter = value => results[(int)value].TestDate.ToString();
            }

        }

    }
}
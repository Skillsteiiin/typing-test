using HelloApp1.Views;
using LiveCharts.Defaults;
using LiveCharts;
using HelloApp1.Common;
namespace HelloApp1.ViewModels
{
    public class UserProgressViewModel
    {
        public ChartValues<ObservableValue> SpeedData { get; set; }
        public ChartValues<ObservableValue> TimeElapsed { get; set; }
        public ChartValues<ObservableValue> CorrectCharacters { get; set; }
        public ChartValues<ObservableValue> TestDate { get; set; }

        public Func<double, string> DateFormatter { get; set; }

        public UserProgressViewModel(List<UserResult> results)
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

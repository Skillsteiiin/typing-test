using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using System.Text.Json;
using System.IO;

namespace HelloApp1.UserLearningPages
{
    public partial class UserEducation : Page
    {
        private string _textToType = "Клара у Карла украла кораллы";
        private bool _isRunning;
        private Stopwatch _stopwatch;
        private DispatcherTimer _timer;

        public class UserResult
        {
            public DateTime TestDate { get; set; }
            public int SpeedWPM { get; set; }
            public TimeSpan TimeElapsed { get; set; }
            public int CorrectCharacters { get; set; }
        }

        public class UserResultsManager
        {
            private string ResultsFilePath = "";

            public List<UserResult> LoadResults()
            {
                ResultsFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\user_results.json"));
                if (!File.Exists(ResultsFilePath))
                    return new List<UserResult>();

                var json = File.ReadAllText(ResultsFilePath);
                if (string.IsNullOrWhiteSpace(json))
                    return new List<UserResult>();

                return JsonSerializer.Deserialize<List<UserResult>>(json) ?? new List<UserResult>();
            }

            public void SaveResults(List<UserResult> results)
            {
                ResultsFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\user_results.json"));
                var json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ResultsFilePath, json);
            }
        }

        private void EndTestWithSave()
        {
            _isRunning = false;
            _timer.Stop();
            _stopwatch.Stop();

            var elapsedMinutes = _stopwatch.Elapsed.TotalMinutes;
            var correctCharacters = 0;

            for (int i = 0; i < UserInput.Text.Length && i < _textToType.Length; i++)
            {
                if (UserInput.Text[i] == _textToType[i])
                    correctCharacters++;
            }

            var wpm = (int)(correctCharacters / 5.0 / elapsedMinutes);

            var resultsManager = new UserResultsManager();
            var results = resultsManager.LoadResults();

            var currentResult = new UserResult
            {
                TestDate = DateTime.Now,
                SpeedWPM = wpm,
                TimeElapsed = _stopwatch.Elapsed,
                CorrectCharacters = correctCharacters
            };

            results.Add(currentResult);
            resultsManager.SaveResults(results);

            var averageWPM = (int)results.Average(r => r.SpeedWPM);
            var bestWPM = results.Max(r => r.SpeedWPM);

            var lastResult = results.Count > 1 ? results[^2].SpeedWPM : 0;
            var improvement = lastResult > 0 ? wpm - lastResult : 0;

            MessageBox.Show(
            $"Тест завершён!\n" +
                $"Время: {_stopwatch.Elapsed:mm\\:ss}\n" +
                $"Скорость: {wpm} WPM\n" +
                $"Средняя скорость: {averageWPM} WPM\n" +
                $"Лучший результат: {bestWPM} WPM\n" +
                $"Изменение: {(improvement >= 0 ? "+" : "")}{improvement} WPM",
                "Результат");

            RestartButton.Visibility = Visibility.Visible;
        }

        public UserEducation()
        {
            InitializeComponent();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;

            UpdateTextToTypeDisplay();
        }

        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("UserLearningPages/UserProgress.xaml", UriKind.Relative));
        }

        private void EducationButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("UserLearning.xaml", UriKind.Relative));
        }

        private void SetTextToType_Letters(object sender, RoutedEventArgs e)
        {
            _textToType = "АБВГДЕЖЗИКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ";
            UpdateTextToTypeDisplay();
        }

        private void SetTextToType_Sentences(object sender, RoutedEventArgs e)
        {
            var sentences = new string[]
            {
                "Это пример предложения для тренировки.",
                "Каждый новый день — это шанс стать лучше.",
                "Всё, что нас не убивает, делает нас сильнее.",
                "Будьте готовы к любым испытаниям в жизни.",
                "Когда-нибудь ваш труд принесёт результаты.",
                "Счастье не в том, что мы имеем, а в том, как мы это ценим",
                "Не бойтесь ошибаться, это часть пути",
                "Путь к успеху всегда начинается с первого шага",
                "Идеи рождаются там, где нет страха",
                "Не важно, как медленно ты идешь, важно не останавливаться",
                "Путь к успеху не всегда прямолинейный, но каждый шаг, каждое усилие делает нас немного сильнее и приближает к нашей цели. Главное — не останавливаться",
                "Важно не только искать ответы, но и задавать правильные вопросы, чтобы в дальнейшем двигаться в том направлении, которое принесёт наибольшие плоды",
            };

            var random = new Random();
            _textToType = sentences[random.Next(sentences.Length)];
            UpdateTextToTypeDisplay();
        }

        private void SetTextToType_Quotes(object sender, RoutedEventArgs e)
        {
            var quotes = new string[]
            {
                "Сила в знании",
                "Тот, кто не рискует, тот не пьёт шампанского",
                "Вера в себя — это ключ к успеху",
                "Жизнь коротка, но её хватает для великих дел",
                "Никогда не сдавайтесь, успех всегда рядом",
                "Сила в знании» — мудрость, которая нас вдохновляет",
                "Жизнь - это не то, что с нами происходит, а то, как мы на это реагируем",
                "Самое важное в жизни — это не то, что мы делаем, а то, как мы это делаем",
                "Мечтать — это не просто закрыть глаза и представить, это — воплотить мечты в реальность",
                "Ты никогда не достигнешь успеха, если не научишься любить процесс",
                "Не бойся делать шаги назад, они могут привести тебя к важным открытиям",
                "Не важно, сколько времени ты тратишь, главное, чтобы ты продолжал двигаться",
                "Чем больше ты узнаешь, тем больше понимаешь, сколько еще предстоит узнать",
                "Будь собой, все остальные роли уже заняты",
            };

            var random = new Random();
            _textToType = quotes[random.Next(quotes.Length)];
            UpdateTextToTypeDisplay();
        }

        private void UserInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _stopwatch = Stopwatch.StartNew();
                _timer.Start();
            }

            UpdateTextToTypeDisplay();

            if (UserInput.Text.Length >= _textToType.Length)
            {
                EndTestWithSave();
            }
        }

        private void UserInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete || e.Key < Key.A || e.Key > Key.Z)
                return;
        }

        private void UpdateTextToTypeDisplay()
        {
            TextToTypeBlock.Inlines.Clear();

            for (int i = 0; i < _textToType.Length; i++)
            {
                var run = new Run(_textToType[i].ToString());

                if (i < UserInput.Text.Length)
                {
                    if (UserInput.Text[i] == _textToType[i])
                    {
                        run.Foreground = Brushes.LimeGreen;
                    }
                    else
                    {
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!_isRunning) return;

            var elapsed = _stopwatch.Elapsed;
        }

        private void EndTest()
        {
            _isRunning = false;
            _timer.Stop();
            _stopwatch.Stop();

            var elapsedMinutes = _stopwatch.Elapsed.TotalMinutes;
            var correctCharacters = 0;
            for (int i = 0; i < UserInput.Text.Length && i < _textToType.Length; i++)
            {
                if (UserInput.Text[i] == _textToType[i])
                    correctCharacters++;
            }

            var wpm = (int)(correctCharacters / 5.0 / elapsedMinutes);

            MessageBox.Show($"Тест завершён!\nВремя: {_stopwatch.Elapsed:mm\\:ss}\nСкорость: {wpm} WPM", "Результат");

            RestartButton.Visibility = Visibility.Visible;
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            UserInput.Clear();
            _isRunning = false;
            RestartButton.Visibility = Visibility.Collapsed;
            _stopwatch.Reset();
        }
    }
}

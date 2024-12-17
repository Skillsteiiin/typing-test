using HelloApp1.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace HelloApp1.ViewModels
{
    internal class UserEducationViewModel : INotifyPropertyChanged
    {
        private string _textToType = " ";
        private string _userInput;
        private bool _isRunning;
        private Stopwatch _stopwatch;
        private DispatcherTimer _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        private ICommand _restartCommand;
        private ICommand _setTextToTypeLettersCommand;
        private ICommand _setTextToTypeSentencesCommand;
        private ICommand _setTextToTypeQuotesCommand;
        private ICommand _endTestCommand;
        public List<ValidateChar> ValidateChars = new List<ValidateChar>();

        public event EventHandler<List<ValidateChar>> ValidateCharsUpdated;

        protected virtual void OnValidateCharsUpdated(List<ValidateChar> validateChars)
        {
            ValidateCharsUpdated?.Invoke(this, validateChars);
        }

        public string TextToType
        {
            get => _textToType;
            set
            {
                _textToType = value;
                OnPropertyChanged();
            }
        }

        public string UserInput
        {
            get => _userInput;
            set
            {
                _userInput = value;
                OnPropertyChanged();
                UpdateValidation();
                if (!_isRunning)
                {
                    StartTest();
                }
                if (UserInput.Length == _textToType.Length)
                {
                    EndTest();
                }
            }
        }

        private void InitializeValidateChars()
        {
            ValidateChars.Clear();
            foreach (var c in TextToType)
            {
                ValidateChars.Add(new ValidateChar { Char = c });
            }
        }

        private void UpdateValidation()
        {
            for (int i = 0; i < TextToType.Length; i++)
            {
                if (i < UserInput.Length)
                {
                    if (TextToType[i] == UserInput[i])
                    {
                        ValidateChars[i].Valid = true;  
                    }
                    else
                    {
                        ValidateChars[i].Valid = false; 
                    }
                }
                else
                {
                    ValidateChars[i].Valid = null;  
                }
            }
            OnValidateCharsUpdated(ValidateChars);
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            // Если значение не изменилось, возвращаем false
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            // Обновляем поле
            field = value;

            // Уведомляем об изменении свойства
            OnPropertyChanged(propertyName);
            return true;
        }


        public ICommand PushUserLearningCommand { get; }
        public ICommand ProgressButtonCommand { get; }
        public ICommand UserEducationCommand { get; }
        public ICommand RestartCommand => _restartCommand ??= new RelayCommandWithoutProps(Restart);

        public ICommand SetTextToTypeLettersCommand => _setTextToTypeLettersCommand ??= new RelayCommandWithoutProps(SetTextToType_Letters);
        public ICommand SetTextToTypeSentencesCommand => _setTextToTypeSentencesCommand ??= new RelayCommandWithoutProps(SetTextToType_Sentences);
        public ICommand SetTextToTypeQuotesCommand => _setTextToTypeQuotesCommand ??= new RelayCommandWithoutProps(SetTextToType_Quotes);

        public string TestResultsMessage { get; set; }

        public UserEducationViewModel()
        {
            _stopwatch = new Stopwatch();
            InitializeValidateChars();
            Restart();
            _timer.Tick += Timer_Tick;
            
            PushUserLearningCommand = NavbarHelper.UserLearningCommand();
            ProgressButtonCommand = NavbarHelper.ProgressButtonCommand();
            UserEducationCommand = NavbarHelper.UserEducationCommand();
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!_isRunning) return;

            var elapsed = _stopwatch.Elapsed;
        }

        private void Restart()
        {
            UserInput = string.Empty;
            SetTextToType_Sentences();
            _stopwatch.Reset();
            TextToType = _textToType;
            _isRunning = false;

            InitializeValidateChars();
            UpdateValidation();
        }

        private void StartTest()
        {
            _isRunning = true;
            _stopwatch.Start();
            _timer.Start();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void EndTest()
        {
            // Завершаем тест, останавливаем таймер
            _stopwatch.Stop();

            // Вычисляем количество правильных символов
            int correctCharacters = 0;
            for (int i = 0; i < UserInput.Length && i < TextToType.Length; i++)
            {
                if (UserInput[i] == TextToType[i])
                {
                    correctCharacters++;
                }
            }

            // Вычисляем скорость ввода (WPM)
            double elapsedMinutes = _stopwatch.Elapsed.TotalMinutes;
            int wpm = (int)(correctCharacters / 5.0 / elapsedMinutes);

            // Вычисляем дополнительные метрики, если необходимо
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

            // Расчет средней и лучшей скорости
            int averageWPM = (int)results.Average(r => r.SpeedWPM);
            int bestWPM = results.Max(r => r.SpeedWPM);

            // Выводим результаты в MessageBox
            var lastResult = results.Count > 1 ? results[^2].SpeedWPM : 0;
            int improvement = lastResult > 0 ? wpm - lastResult : 0;

            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(
                    $"Тест завершён!\n" +
                    $"Время: {_stopwatch.Elapsed:mm\\:ss}\n" +
                    $"Скорость: {wpm} WPM\n" +
                    $"Средняя скорость: {averageWPM} WPM\n" +
                    $"Лучший результат: {bestWPM} WPM\n" +
                    $"Изменение: {(improvement >= 0 ? "+" : "")}{improvement} WPM" + 
                    $"Кол-во правильных символов: {correctCharacters}",
                    "Результат");
            });
            Restart();
        }

        private void SetTextToType_Sentences()
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
            TextToType = _textToType;
            
            InitializeValidateChars();
            UpdateValidation();
            UserInput = string.Empty;
        }

        private void SetTextToType_Quotes()
        {
            var quotes = new string[]
            {
                "Сила в знании",
                "Тот, кто не рискует, тот не пьёт шампанского",
                "Вера в себя - это ключ к успеху",
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
            TextToType = _textToType;
            
            InitializeValidateChars();
            UpdateValidation();
            UserInput = string.Empty;
        }
        private void SetTextToType_Letters()
        {
            TextToType = "АБВГДЕЖЗИКЛМНОПРСТУФХЦЧШЩЫЬЭЮЯ";
            
            InitializeValidateChars();
            UpdateValidation();
            UserInput = string.Empty;
        }
    }
}

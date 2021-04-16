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
using System.Windows.Threading;

namespace KeyBoardTrainee
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] _quests = {
           "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
           "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur",
           "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum"
        };
        private int _indexQuest = -1; //идекс текущей задачи
        private int _indexCurrentLetter = 0; //индекс вводимого символа
        private string _userResult = "";
        private int _countFails = 0; //количество ошибок
        private int _countTotal = 0; //количество нажатых клавиш

        private DispatcherTimer _taskTimer; //таймер одного раунда
        private DateTime _startTime;
        private DateTime _endTime;
        private TimeSpan _elapsedTime; //_endTime - _startTime - прошедшее время

        private List<Border> _buttons;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _buttons = new List<Border>();
            _taskTimer = new DispatcherTimer();
            _taskTimer.Interval = new TimeSpan(1000);
            _taskTimer.Tick += _taskTimer_Tick;
            Button_End.IsEnabled = false;

            foreach (StackPanel onePanel in StackButtonPanel.Children.OfType<StackPanel>())
            {
                foreach (Grid item in onePanel.Children.OfType<Grid>())
                {
                    _buttons.Add(item.Children.OfType<Border>().First());
                }
            }

        }

        private void _taskTimer_Tick(object sender, EventArgs e)
        {

        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            if (!_taskTimer.IsEnabled)
            {
                _startTime = DateTime.Now;
                Button_End.IsEnabled = true;
                Button_Start.IsEnabled = false;
                _taskTimer.Start();
                Random random = new Random();
                _indexQuest = random.Next(0, _quests.Length);
                RTextBox_QuestText.Document.Blocks.Clear();
                RTextBox_QuestText.Document.Blocks.Add(new Paragraph(new Run(_quests[_indexQuest])));
            }
        }

        private void Button_End_Click(object sender, RoutedEventArgs e)
        {
            if (_taskTimer.IsEnabled)
            {
                Button_Start.IsEnabled = true;
                Button_End.IsEnabled = false;

                _endTime = DateTime.Now;
                _elapsedTime = _endTime - _startTime;
                MessageBox.Show(_elapsedTime.Milliseconds.ToString());
                _taskTimer.Stop();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!_taskTimer.IsEnabled) return;

                Key key = e.Key;
                int keyKode = Convert.ToInt32(e.Key);
                string keySymbol = e.Key.ToString().ToLower();

                #region
                // MessageBox.Show(keySymbol + " " +  keyKode);
                
                   

                    //if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.None)
                    //{
                    //    keySymbol = keySymbol.ToLower();
                    //}
                    //if (Keyboard.GetKeyStates(Key.LeftShift) == KeyStates.Down)
                    //{
                    //    keySymbol = keySymbol.ToUpper();
                    //}
                    //if (Keyboard.GetKeyStates(Key.RightShift) == KeyStates.Down)
                    //{
                    //    keySymbol = keySymbol.ToUpper();
                    //}

                    //Key key1 = Key.CapsLock;
                    //MyKey key2 = MyKey.Capslock;               
                    // MyKeyClass.Capslock // 8                
                    /// MyKey.Capslock
                    /// 
                    #endregion

                bool isShift = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
                bool isCapsLock = Keyboard.IsKeyToggled(Key.CapsLock);
                if (isCapsLock ^ isShift)  // ^  XOR              
                {
                    keySymbol = keySymbol.ToUpper();
                }

                #region
                // false ^ false = false
                // true ^ true = false
                // true ^ false = true
                // false ^ true = true

                //if(isCapsLock && !isShift || !isCapsLock && isShift)
                //{
                //    keySymbol = keySymbol.ToUpper();
                //}
                #endregion

                Dictionary<Key, string> keysToPrint = new Dictionary<Key, string>();
                keysToPrint[Key.Space] = " ";
                keysToPrint[Key.D0] = "0";
                keysToPrint[Key.D1] = "1";
                keysToPrint[Key.D2] = "2";
                keysToPrint[Key.D3] = "3";
                keysToPrint[Key.D4] = "4";
                keysToPrint[Key.D5] = "5";
                keysToPrint[Key.D6] = "6";
                keysToPrint[Key.D7] = "7";
                keysToPrint[Key.D8] = "8";
                keysToPrint[Key.D9] = "9";
                keysToPrint[Key.OemComma] = ",";
                keysToPrint[Key.OemPlus] = "=";
                keysToPrint[Key.Oem3] = "~";


                //keys.Contains

                if (keySymbol.Length == 1 || key == Key.Back || keysToPrint.ContainsKey(key))
                {
                    _userResult = "";
                    if (keysToPrint.ContainsKey(key))
                    {
                        _userResult = keysToPrint[key];
                    }                    
                    else if (key == Key.Back) //BackSpace
                    {                       
                        string richText = new TextRange(
                            RTextBox_UserText.Document.ContentStart, /*позиция начала набранного текста*/
                            RTextBox_UserText.Document.ContentEnd).Text; //позиция конца набранного текста
                        MessageBox.Show(richText.Length + " " + _indexCurrentLetter);
                        if (richText.Length > 0  && richText.Length - 2  > _indexCurrentLetter)
                        {
                            richText = richText.Substring(0, richText.Length - 3);
                        }

                        RTextBox_UserText.Document.Blocks.Clear();
                        RTextBox_UserText.AppendText(richText);
                        return;
                    }
                    else
                    {
                        _userResult = keySymbol;
                    }

                    // "a" - string;  'a' - char
                    if (_userResult.Length > 0 && _quests[_indexQuest][_indexCurrentLetter] == _userResult[0])
                    {
                        //угадал
                        _indexCurrentLetter++;
                    }
                    else
                    {
                        _countFails++;
                        TextBlock_Fails.Text = "Fails: " + _countFails;
                    }

                    RTextBox_UserText.AppendText(_userResult);

                    _countTotal++;
                    TextBlock_Total.Text = "Total: " + _countTotal;
                }
                else
                {
                    return;
                }


                foreach (Border oneButton in _buttons)
                {
                    if (((TextBlock)oneButton.Child).Text.ToUpper().Equals(keySymbol.ToUpper()))
                    {
                        Brush oldColor = oneButton.Background; //сохраню старый цвет кнопки
                        if (oldColor != Brushes.DimGray)
                        {
                            oneButton.Background = Brushes.DimGray;
                            BlinkBackground(oldColor);
                        }

                        async Task BlinkBackground(Brush brush)
                        {
                            await Task.Delay(250);
                            oneButton.Background = brush; //везврат к исходному состоянию
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}


// таймер обратного отсчета
// скорость/эффективность

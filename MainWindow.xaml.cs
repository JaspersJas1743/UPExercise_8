using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Задание__8
{
    public partial class MainWindow : Window
    {
        private static bool DarkTheme = false;

        public MainWindow()
        {
            InitializeComponent();
            MainBox.Focus();
        }

        private void Drag(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                Application.Current.MainWindow.DragMove();
        }

        private void ExitClick(object sender, RoutedEventArgs e) => Exit();

        private void DeactivateClick(object sender, RoutedEventArgs e) => Application.Current.MainWindow.WindowState = WindowState.Minimized;

        private async void CorrectButtonClick(object sender, RoutedEventArgs e) => await SetValue();
        
        private void UnderLineButtonClick(object sender, RoutedEventArgs e)
        {
            MainBox.SpellCheck.IsEnabled = !MainBox.SpellCheck.IsEnabled;
            UnderLineButton.Content = MainBox.SpellCheck.IsEnabled ? "Не подчеркивать ошибки" : "Подчеркивать ошибки";
        }

        private void ResetTheme(object sender, RoutedEventArgs e)
        {
            Uri uri;
            if (!DarkTheme)
                uri = new Uri(@"..\Themes\DarkTheme.xaml", UriKind.Relative);
            else uri = new Uri(@"..\Themes\LightTheme.xaml", UriKind.Relative);
            DarkTheme = !(DarkTheme);
            ResourceDictionary? resDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resDict);
        }

        private async Task SetValue()
        {
            ResultBox.Clear();
            var incorrectWorlds = Utility.Corrector.GetDataAsync(MainBox.Text);
            string resultString = MainBox.Text;
            foreach (IncorrectWord ans in await incorrectWorlds)
            {
                resultString = resultString.Replace(ans.Value, ans.PossibleWay.First());
                ResultBox.Text += $"{ans.Value} -> {ans.PossibleWay.First()}\n";
            }
            ResultBox.Text += resultString;
        }

        private void Exit()
        {
            if (MessageBox.Show("Вы уверены, что хотите закрыть окно?", "Close", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private async void MainBoxKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter: await SetValue(); break;
                case Key.Escape: Exit(); break;
            }
        }
    }
}

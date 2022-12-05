using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GoFish
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        bool IsFirst = true;
        public StartWindow()
        {
            InitializeComponent();
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb.Text.Length < 1) MessageBox.Show("Неправильное имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                MainWindow mainWindow = new MainWindow();
                GameStats.CountPlayers = (int)slider.Value;
                GameStats.PlayerName = tb.Text;

                mainWindow.Show();
                Close();
            }

        }

        private void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IsFirst)
            {

                tb.Text = "";
                IsFirst = false;
            }
        }
    }
}

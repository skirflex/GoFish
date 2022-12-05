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
    /// Логика взаимодействия для ChoseOponent.xaml
    /// </summary>
    public partial class ChoseOponent : Window
    {
        
        public ChoseOponent()
        {
            InitializeComponent();
        }

        public int index;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var c in (sender as Button).Content.ToString())
            {
                if (int.TryParse(c.ToString(), out int result)) index = result;
            }
            Close();
        }
    }
}
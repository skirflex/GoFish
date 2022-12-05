using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace GoFish
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameController gameController;
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gameController = new GameController(this);
            Binding mybindingBooks = new Binding("TextForBooks");
            mybindingBooks.Source = gameController;
            mybindingBooks.Mode = BindingMode.OneWay;
            TBBooks.SetBinding(TextBox.TextProperty, mybindingBooks);

            Binding mybindingProgress = new Binding("TextForProgess");
            mybindingProgress.Source = gameController;
            mybindingProgress.Mode = BindingMode.OneWay;
            TBGameProgress.SetBinding(TextBox.TextProperty, mybindingProgress);

            ShowPlayerHand();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bt.IsEnabled = false;
            if (playerHand.SelectedItem != null)
            {
                
                var message = OpenChoseOponent();
                if (message != 0)
                {

                    gameController.DoTurn(message, (playerHand.SelectedItem as Card).Value, ShowPlayerHand);
                    TBGameProgress.ScrollToEnd();
                    TBBooks.ScrollToEnd();
                }

            }
            ShowPlayerHand();
            bt.IsEnabled = true;
        }

      

        private int OpenChoseOponent() // Получаем id пользователя у которого спрашиваем карту
        {
            int maxPlayers = 4;
            ChoseOponent choseOponent = new ChoseOponent();

            int activePlayers = gameController.computerPlayers.Count;
            var buttons = choseOponent.stackPanel.Children;
            for (int i = maxPlayers - 1; i >= activePlayers; i--)
            {
                (buttons[i] as Button).IsEnabled = false;

            }
            choseOponent.ShowDialog();
            return choseOponent.index;
        }

        private void ShowPlayerHand() // Обновляем ListBox
        {
            playerHand.ItemsSource = null;
            playerHand.ItemsSource = gameController.humanPlayer.Hand;
        }
    }
}

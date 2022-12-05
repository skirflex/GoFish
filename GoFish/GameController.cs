using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace GoFish
{
    class GameController : INotifyPropertyChanged
    {
        MainWindow main;

        Deck deck;

        string textForProgess = "";
        string textforbooks = "Books: ";

        public readonly Player humanPlayer;

        public List<Player> computerPlayers;

        public string TextForBooks
        {
            get { return textforbooks; }
            private set {
                textforbooks = value;
                OnPropertyChanged("TextForBooks");
                CheckWinner();
            }
        }

        private void CheckWinner()
        {
            
            if (humanPlayer.Books.Count == 6)
            {
                EndGame(humanPlayer.Name);

            }
            foreach (var player in computerPlayers)
            {
                if (player.Books.Count == 6)
                {
                    EndGame(humanPlayer.Name);
                }
            }
            if (humanPlayer.Hand.Count == 0 || computerPlayers.Select(x=> x.Hand.Count).Any(x=> x == 0))
            {
                var tuple = Tuple.Create(humanPlayer.Name, humanPlayer.Books.Count());
                foreach (var player in computerPlayers)
                {
                    if (player.Books.Count > tuple.Item2)
                    {
                        tuple = Tuple.Create(player.Name, player.Books.Count());
                    }
                }
                EndGame(tuple.Item1);
            }
        }

        public string TextForProgess { 
            get => textForProgess;
            set {
                textForProgess = value;
                OnPropertyChanged("TextForProgess");
            }
        }

        public GameController(MainWindow mainWindow)
        {
            
            deck = new Deck();
            deck.Shuffle();
            int startedCardsCount = GameStats.CountPlayers >= 5 ? 5 : 7;
            humanPlayer = new Player(deck.Take(startedCardsCount) , GameStats.PlayerName);
            computerPlayers = new List<Player>();
            for (int i = 1; i < GameStats.CountPlayers+1; i++)
            {
                computerPlayers.Add(new Player(deck.Take(startedCardsCount), $"Computer #{i}"));
            }
            textForProgess = $"Welcome to the game, {humanPlayer.Name} \n Starting a new game with players: {GetNames()}";
            this.main = mainWindow;
        }

        private string GetNames()
        {
            string result = $"{humanPlayer.Name}";
            foreach (var bot in computerPlayers)
            {
                result += $" , {bot.Name}";
            }
            return result;
        }

        public void DoTurn(int index, Card.Values card , Action action)
        {

            
            var myTuple = humanPlayer.DoTurn(card, computerPlayers[index - 1], deck);
            
            TextForBooks += myTuple.Item2;
            TextForProgess += myTuple.Item1;
            action.Invoke();
            if (!myTuple.Item3)
            {
                for(int i = 0; i < computerPlayers.Count;)
                {
                    
                    var check = computerPlayers[i].GetNeededCard();
                    if (check != null)
                    {
                        var nedeedCard = (Card.Values)check;
                        var myTuple1 = computerPlayers[i].DoTurn(nedeedCard, GetOpponent(nedeedCard, computerPlayers[i]), deck);

                        TextForBooks += myTuple1.Item2;

                        TextForProgess += myTuple1.Item1;
                        action.Invoke();
                        i += myTuple1.Item3 ? 0 : 1;
                    }
                    else CheckWinner();
                }
            }

            

        }

        private Player GetOpponent(Card.Values card, Player currentPlayer) // Здесь нужно у компуктерного игрока найти подходящего оппонента
        {
            
            foreach (var str in TextForProgess.Split('\n'))
            {
                if (str.Contains(card.ToString()) && str.Contains("has"))
                {
                    string name = str.Split().First();
                    foreach (var player in computerPlayers)
                    {
                        if (player.Name == name) return player;
                        
                    }
                    return humanPlayer;
                }
                
            }
            int r = new Random().Next(GameStats.CountPlayers - 1);
            return computerPlayers[r].Name == currentPlayer.Name ? humanPlayer : computerPlayers[r];
        }

        


        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        } // Вообще какая-то магия просиходит!!!Нужно разобраться

        public event PropertyChangedEventHandler PropertyChanged;

        private void EndGame(string name)
        {
            EndGame endGame = new EndGame(name);
            main.Close();
            endGame.Show();
        }
    }

   
}


using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private members
        /// <summary>
        /// holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if player 1'turn x or player 2's turn O
        /// </summary>
        private bool mPlayer1turn;

        /// <summary>
        /// true if the game have ended
        /// </summary>
        private bool mGameEnded;

        #endregion
         

        #region constructor

        /// <summary>
        /// default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }



        #endregion
        /// <summary>
        /// start a new game a clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //make sure the player one is the current player
            mPlayer1turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            // make sure the game haven't finished
            mGameEnded = false;

        }
        /// <summary>
        /// handle a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">the event of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //start a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            //cast the sender to a button
            var button = (Button)sender;

            //find the button position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);
            //don't do anything if the cell already has value on it
            if(mResults[index] != MarkType.Free)
            {
                return;
            }
            // the cell value base on which players turn
            if (mPlayer1turn)
                mResults[index] = MarkType.Cross;
            else
                mResults[index] = MarkType.Nought;
            //set button text to the result
            button.Content = mPlayer1turn ? "X" : "O";

            //change noughts to red
            if (!mPlayer1turn)
                button.Foreground = Brushes.Red;
            //Toggle the players turn
            mPlayer1turn ^= true;

            // Check for a winner
            CheckForwinner();

           
        }
        /// <summary>
        /// checks if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForwinner()
        {
            //check for horizontal win
            //row 0
            if(mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }


            //check for vertical win
            //column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //column 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //check for no winner and all possible move are full
            if (!mResults.Any(f => f == MarkType.Free))
            {
                //Game ended
                mGameEnded = true;

                //turn all cell Orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    
                    button.Background = Brushes.Orange;
                   
                });


            }
        }
    }
}

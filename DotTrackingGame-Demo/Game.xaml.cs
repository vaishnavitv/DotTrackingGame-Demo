using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace DotTrackingGame_Demo
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected MainWindow window;

        //Fine grained timer
        private DispatcherTimer GameTrackTimer;

        //Coarse grained timer
        private DispatcherTimer CountDownTimer;
        private const int timerForMenu = 4;
        private const int windowBorderWidth = 15;
        private const int windowBorderHeight = 37;

        private int timerForCountDown = 0;

        //Game Transaction Log
        private List<GameTransaction> gameTransactionLog;
        private IGameTransactionSource inputSource;

        public Game(MainWindow window, object initialState = null)
        {
            InitializeComponent();

            this.window = window;
            int.TryParse(ConfigurationManager.AppSettings["GameTimerInSeconds"], out timerForCountDown);

            if (initialState != null)
            {
                log.Info("Starting Game in Replay Mode ...");
                StartGameForReplay(initialState);
            }
            else
            {
                log.Info("Starting Game ...");
                StartGame();
            }

        }

        private void StartGame()
        {
            //These static dimensions need fixing. We need to defer this for correct Initialization.
            double maxX = window.Width - DotMain.Width - windowBorderWidth - 1; //Border Fix. Fencepost Fix. 
            double maxY = window.Height - DotMain.Height - windowBorderHeight - 1; //Border Fix. Fencepost Fix.

            inputSource = new GameTransactionSource(maxX, maxY, this.GameCanvas);
            DotReplay.Visibility = Visibility.Collapsed;
            gameTransactionLog = new List<GameTransaction>();

            InitializeAndStartTimers();
        }

        private void StartGameForReplay(object initialState)
        {
            List<GameTransaction> currentTransactionLog = (List<GameTransaction>)initialState;
            inputSource = new ReplayTransactionSource(currentTransactionLog);
            CountdownDisplay.Visibility = Visibility.Collapsed;
            gameTransactionLog = new List<GameTransaction>();

            InitializeAndStartTimers();
        }

        private void InitializeAndStartTimers()
        {
            CountDownTimer = new DispatcherTimer();
            CountDownTimer.Interval = TimeSpan.FromSeconds(1);
            CountDownTimer.Tick += CountDownTimerTick;

            GameTrackTimer = new DispatcherTimer();
            GameTrackTimer.Interval = TimeSpan.FromMilliseconds(8);
            GameTrackTimer.Tick += GameTrackTimerTick;

            CountDownTimer.Start();
            GameTrackTimer.Start();
        }

        private void GameTrackTimerTick(object sender, EventArgs e)
        {
            GameTransaction gameTransactionNow = null;

            //Stop Game Timer.
            if (timerForCountDown <= 0)
            {
                GameTrackTimer.Stop();
            } else
            {
                //Process Input.
                gameTransactionNow = inputSource.GetInput();
                if (gameTransactionNow != null)
                    gameTransactionLog.Add(gameTransactionNow);
            }

            //Display Objects.
            GameDisplay(gameTransactionNow);
        }

        private void CountDownTimerTick(object sender, EventArgs e)
        {
            if ((timerForCountDown + timerForMenu) == 0)
            {
                CountDownTimer.Stop();
                window.SetState(gameTransactionLog);
                this.window.SetPanelContent(new GameMenu(window));
            }

            timerForCountDown--;
        }

        private void DisplaySummary()
        {
            if(String.IsNullOrEmpty(SummaryDisplay.Text))
            {
                double totalDistance = 0;
                foreach (GameTransaction transaction in gameTransactionLog)
                    totalDistance += Point.Subtract(transaction.GamePoint, transaction.MousePoint).Length;
                double totalPoints = gameTransactionLog.Count;
                double averageDistance = (totalPoints > 0) ? (totalDistance / totalPoints) : totalDistance;
                SummaryDisplay.Text = $"Distance: {(int)averageDistance} px";
            }
        }

        private void DisplayCountDown()
        {
            if(timerForCountDown == 0)
            {
                CountdownDisplay.Text = "Time's Up!";
            }
            else
            {
                CountdownDisplay.Foreground = Brushes.LimeGreen;
                if (timerForCountDown <= 10)
                    CountdownDisplay.Foreground = (timerForCountDown % 2 == 0) ? Brushes.Red : Brushes.White;
                int minutes = timerForCountDown / 60, seconds = timerForCountDown % 60;
                CountdownDisplay.Text = string.Format("{0}:{1}", minutes.ToString("D2"), seconds.ToString("D2"));
            }
        }

        private void DisplayTimer()
        {
            if (timerForCountDown < 0)
                return;

            DisplayCountDown();
            if (timerForCountDown == 0)
                DisplaySummary();

        }

        private void GameDisplay(GameTransaction gameTransactionNow)
        {
            if(gameTransactionNow != null)
            {
                Canvas.SetLeft(DotMain, gameTransactionNow.GamePoint.X);
                Canvas.SetTop(DotMain, gameTransactionNow.GamePoint.Y);

                Canvas.SetLeft(DotReplay, gameTransactionNow.MousePoint.X);
                Canvas.SetTop(DotReplay, gameTransactionNow.MousePoint.Y);
            }

            DisplayTimer();
        }
  
    }
}

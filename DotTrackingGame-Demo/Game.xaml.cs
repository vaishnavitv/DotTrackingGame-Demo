using System;
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
        //Fine grained timer
        private DispatcherTimer GameTrackTimer;
        private DispatcherTimer CountDownTimer;

        private int timerForCountDown = 15;
        private int timerForMenu = 8;
            
        Random rand = new Random();
        protected MainWindow window;

        double posX = 0, posY = 0;
        double maxX = 0, maxY = 0;

        double intensity = 32;

        public Game(MainWindow window, object replayMe = null)
        {
            InitializeComponent();
            this.window = window;

            CountDownTimer = new DispatcherTimer();
            CountDownTimer.Interval = TimeSpan.FromSeconds(1);
            CountDownTimer.Tick += CountDownTimer_Tick;

            GameTrackTimer = new DispatcherTimer();
            GameTrackTimer.Interval = TimeSpan.FromMilliseconds(8);
            GameTrackTimer.Tick += GameTrackTimer_Tick;

            CountDownTimer.Start();
            GameTrackTimer.Start();
        }

        private void GameTrackTimer_Tick(object sender, EventArgs e)
        {
            //Movement in one interval
            //Remove the previous ellipse from the paint canvas.

            if (timerForCountDown < 0)
            {
                GameTrackTimer.Stop();
                return;
            }

            double speedX = (2 * intensity * rand.NextDouble()) - intensity;
            double speedY = (2 * intensity * rand.NextDouble()) - intensity;
            double maxX = window.Width - DotMain.Width;
            double maxY = window.Height - DotMain.Height;

            posX = Clamp(posX + speedX, 0, maxX);
            posY = Clamp(posY + speedY, 0, maxY); //Don't get it too close to the bottom.

            Canvas.SetLeft(DotMain, posX);
            Canvas.SetTop(DotMain, posY);

        }

        private void CountDownTimer_Tick(object sender, EventArgs e)
        {
            if (timerForCountDown > 0)
            {
                if (timerForCountDown <= 10)
                {
                    CountdownDisplay.Foreground = (timerForCountDown % 2 == 0) ? Brushes.Red : Brushes.White;
                } else {
                    CountdownDisplay.Foreground = Brushes.Green;
                }
                var minutes = timerForCountDown / 60;
                var seconds = timerForCountDown % 60;
                CountdownDisplay.Text = string.Format("{0}:{1}", minutes.ToString("D2"), seconds.ToString("D2"));
            }
            else if(timerForCountDown == 0)
            {
                CountdownDisplay.Text = "Time's Up!";
            }
            else if ( (timerForCountDown + timerForMenu) == 0) {
                CountDownTimer.Stop();
                this.window.SetPanelContent(new GameMenu(window));
            }
            timerForCountDown--;
        }

        private double Clamp(double valueToClamp,double minValue, double maxValue)
        {
            if (valueToClamp < minValue)
                return minValue;
            if (valueToClamp > maxValue)
                return maxValue;

            return valueToClamp;
        }
    }
}

using System.Windows.Controls;

namespace DotTrackingGame_Demo
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : UserControl
    {

        MainWindow window;
        public GameMenu(MainWindow window)
        {
            InitializeComponent();

            this.window = window;
        }

        private void OnPlay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            window.SetPanelContent(new Game(window));
        }

        private void OnReplay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            window.SetPanelContent(new Game(window));
        }

    }
}

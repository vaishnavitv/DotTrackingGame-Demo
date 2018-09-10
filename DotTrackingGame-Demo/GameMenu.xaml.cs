using log4net;
using System.Windows.Controls;

namespace DotTrackingGame_Demo
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        MainWindow window;

        public GameMenu(MainWindow window)
        {
            
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
            this.window = window;

            ReplayButton.IsEnabled = (this.window.GetState() != null);
        }

        private void OnPlay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            log.Info("Game Menu: OnPlay ...");
            window.SetPanelContent(new Game(window));
        }

        private void OnReplay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            log.Info("Game Menu: OnReplay ...");
            window.SetPanelContent(new Game(window, window.GetState()));
        }

    }
}

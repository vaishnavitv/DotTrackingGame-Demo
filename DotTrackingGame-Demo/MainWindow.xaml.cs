using log4net;
using System.Windows;
using System.Windows.Controls;

namespace DotTrackingGame_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private object state;
        public MainWindow()
        {
            log.Info("Invoking Dot Tracking Game Application ...");
            InitializeComponent();
            SetPanelContent(new GameMenu(this));
        }

        public void SetPanelContent(UserControl userControl)
        {
            log.Info("Setting Panel Contents ...");
            this.Panel.Content = userControl;
        }


        public void SetState(object currentState)
        {
            log.Info("Setting Last Panel State ...");
            this.state = currentState;
        }

        public object GetState()
        {
            return this.state;
        }
    }
}

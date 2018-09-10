using log4net;
using System.Windows;

namespace DotTrackingGame_Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnStartup(StartupEventArgs eventArgs)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("        =============  Started Logging for Dot Tracking Game  =============        ");
            base.OnStartup(eventArgs);
        }

    }
}

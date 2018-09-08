using System.Windows;
using System.Windows.Controls;

namespace DotTrackingGame_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            SetPanelContent(new GameMenu(this));
        }

        public void SetPanelContent(UserControl userControl)
        {
            this.Panel.Content = userControl;
        }
    
    }
}

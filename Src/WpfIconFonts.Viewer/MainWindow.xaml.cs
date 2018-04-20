using System.Windows.Media;

namespace Algel.WpfIconFonts.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Icon = ImageIcon.CreateImageSource(FontIcon.Fa_FlagCheckered_Solid, Brushes.MediumBlue);
        }
    }
}

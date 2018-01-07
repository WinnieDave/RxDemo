using System.Windows;
using RxDemo.ViewModel;

namespace RxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new AddUrlWindow().ShowDialog();
        }
    }
}
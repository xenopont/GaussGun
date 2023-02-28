using System.Windows;

namespace GaussGun {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly StackWindow StackWindow;

        public MainWindow() {
            InitializeComponent();
            StackWindow = new();
            StackWindow.Show();
        }

        private void Button1ClickHandler(object sender, RoutedEventArgs e) {
            listBox1.Items.Clear();
            Experiment.ListAllWindows().ForEach((wnd) =>
            {
                listBox1.Items.Add(wnd.title);
            });
        }

        private void WindowClosingAttemptHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            StackWindow.Close();
        }
    }
}

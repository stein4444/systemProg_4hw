using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Forms;
namespace systemProg_hw4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SortingItem sorting { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            sorting = new SortingItem();
            this.DataContext = sorting;
        }

        private void btnFrom_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                tbFrom.Text = dialog.SelectedPath;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbFrom.Text == null)
                return;
            string folder = tbFrom.Text;
            string[] txtFiles = Directory.GetFiles(folder, "*.txt");
            lsFiles.ItemsSource = txtFiles;
            SortLines(txtFiles);
        }

        private void SortLines(string[] el)
        {
            Thread[] threads = new Thread[el.Length];
            ParameterizedThreadStart start = new ParameterizedThreadStart(sorting.Sorting);
            for (int i = 0; i < el.Length; i++)
            {
                threads[i] = new Thread(start);
                threads[i].Start(el[i]);
            }
        }
    }
}

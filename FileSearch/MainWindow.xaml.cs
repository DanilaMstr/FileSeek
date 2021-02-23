using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace FileSearch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Seeker seeker = new Seeker();
        public MainWindow()
        {
            InitializeComponent();

            //var seeker = new Seeker();
            //seeker.Format = "[a-zA-Z0-9]";
            //seeker.StartDirectory = @"D:\Курсы";
            //FolderView.ItemsSource = seeker.treeNodes;
            //seeker.Seek();
        }

        private void Seek_Button_Click(object sender, RoutedEventArgs e)
        {
            //var seeker = new Seeker();
            //seeker.StartDirectory = tb_start_dir.Text;
            //seeker.Format = tb_filter.Text;
            //FolderView.ItemsSource = seeker.treeNodes;
            //seeker.Seek();

            seeker = new Seeker();
            seeker.Format = "[a-zA-Z0-9]+.dll";
            seeker.StartDirectory = @"D:\Qt\Tools\QtCreator";
            FolderView.ItemsSource = seeker.treeNodes;
            seeker.Seek();
        }
    }
}

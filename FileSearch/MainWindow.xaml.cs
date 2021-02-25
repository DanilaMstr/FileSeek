using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.ComponentModel;
using System;

namespace FileSearch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Seeker seeker = new Seeker();
        private string configFileName = "conf.txt";

        public MainWindow()
        {
            InitializeComponent();
            StartConfig();
        }

        private void Seek_Button_Click(object sender, RoutedEventArgs e)
        {
            seeker = new Seeker();
            seeker.Format = tb_filter.Text;
            seeker.StartDirectory = tb_start_dir.Text;
            FolderView.ItemsSource = seeker.treeNodes;
            seeker.Seek();
        }

        private void StartConfig()
        {
            FileInfo fileConf = new FileInfo(configFileName);
            if (fileConf.Exists)
            {
                using (StreamReader sr = new StreamReader(fileConf.FullName, System.Text.Encoding.Default))
                {
                    string line;
                    if ((line = sr.ReadLine()) != null)
                        tb_start_dir.Text = line;
                    if ((line = sr.ReadLine()) != null)
                        tb_filter.Text = line;
                }
            }
            else
            {
                fileConf.Create();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            FileInfo fileConf = new FileInfo(configFileName);
            if (fileConf.Exists)
            {
                using (StreamWriter sw = new StreamWriter(fileConf.FullName, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(tb_start_dir.Text);
                    sw.WriteLine(tb_filter.Text);
                }
            }
            else
            {
                fileConf.Create();
                using (StreamWriter sw = new StreamWriter(fileConf.FullName, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(tb_start_dir.Text);
                    sw.WriteLine(tb_filter.Text);
                }
            }

            base.OnClosing(e);
        }
    }
}

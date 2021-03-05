using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FileSearch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Seeker seeker = new Seeker();
        private const string configFileName = "conf.txt";
        private bool isStop = true;

        public MainWindow()
        {
            InitializeComponent();
            StartConfig();
        }

        private void Seek_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDirExist())
            {
                Stop_Button.Content = "Остановить";
                if (seeker.ThreadSeekerIsExist())
                    seeker.AbortSeek();
                seeker = new Seeker();
                seeker.Format = tb_filter.Text;
                seeker.StartDirectory = tb_start_dir.Text;
                FolderView.ItemsSource = seeker.treeNodes;
                seeker.Seek();
                DataContext = seeker;
            }
            else
                tb_start_dir.Text = " not exist";

        }

        private bool CheckDirExist()
        {
            var dirInfo = new DirectoryInfo(tb_start_dir.Text);
            return dirInfo.Exists;
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isStop)
            {
                seeker.SeekingReset();
                Stop_Button.Content = "Возобновить";
                isStop = false;
            }
            else
            {
                seeker.SeekingSet();
                Stop_Button.Content = "Остановить";
                isStop = true;
            }
            //seeker.SeekingReset();

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
            if (seeker.ThreadSeekerIsExist())
                seeker.AbortSeek();
            base.OnClosing(e);
        }
    }
}

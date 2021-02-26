using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace FileSearch
{
    public class Seeker : NotifyPropertyChanged
    {
        private Thread _thread;
        private ManualResetEventSlim _okayToContinue = new ManualResetEventSlim(false);
        private uint _countFound = 0;
        private uint _allCountFound = 0;
        private string _format = "[a-zA-z0-9]+";
        private DirectoryNode _startDirectory;
        private string _currDirectory = string.Empty;
        public ObservableCollection<TreeNode> treeNodes;

        public uint Found 
        { 
            get => _countFound;
            set
            {
                _countFound = value;
                OnPropertyChanged("Found");
            }
        }
        public uint AllFound 
        { 
            get => _allCountFound;
            set 
            {
                _allCountFound = value;
                OnPropertyChanged("AllFound");
            }
        }
        public string CurrentDiretory
        {
            get => _currDirectory;
            set
            {
                _currDirectory = value;
                OnPropertyChanged("CurrentDiretory");
            }
        }
        public string Format { set => _format = value; }
        public string StartDirectory 
        { 
            set
            {
                _startDirectory = new DirectoryNode { NodeName = value, NodeFullName = value };
                treeNodes.Add(_startDirectory);
            }
        }

        public Seeker() 
        {
            treeNodes = new ObservableCollection<TreeNode>();
        }

        public void Seek()
        {
            Restart();
            _thread = new Thread(()
                => Seeking())
            { IsBackground = true };
            _thread.Start();
        }

        private void Restart()
        {
            if (_thread != null)
                _thread.Abort();
            _countFound = 0;
            _allCountFound = 0;
        }

        private void Seeking()
        {
            CheckDirectoris(_startDirectory);
        }

        private void CheckDirectoris(DirectoryNode directory)
        {
            try
            {
                bool hasFile = CheckFilesOnDir(directory);
                var dirInfo = new DirectoryInfo(directory.NodeFullName);
                foreach (var dir in dirInfo.GetDirectories())
                {
                    var newDir = new DirectoryNode { NodeName = dir.Name, NodeFullName = dir.FullName };
                    CheckDirectoris(newDir);
                    if (newDir.HasFile)
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            directory.Items.Add(newDir);
                        });
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        private bool CheckFilesOnDir(DirectoryNode directory)
        {
            bool hasFiles = false;
            try
            {
                var currDirInfo = new DirectoryInfo(directory.NodeFullName);
                foreach (var file in currDirInfo.GetFiles())
                {
                    CurrentDiretory = file.DirectoryName;
                    AllFound++;
                    if (Regex.IsMatch(file.Name, _format))
                    {
                        Found++;
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            directory.Items.Add(new FileNode { NodeName = file.Name });
                        });
                        hasFiles = true;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            return hasFiles;
        }

        public void AbortSeek() => _thread.Abort();
        public bool ThreadSeekerIsExist() => _thread != null;
    }
}

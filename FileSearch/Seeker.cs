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
        private string _format = "[a-zA-z]";
        private DirectoryNode _startDirectory;
        private DirectoryNode _currDirectory;
        private Stack<DirectoryNode> _nodes;
        public ObservableCollection<TreeNode> treeNodes;

        public uint Found { get => _countFound; }
        public uint AllFound { get => _allCountFound; }
        public string Format { set => _format = value; }
        public string StartDirectory 
        { 
            set
            {
                _startDirectory = new DirectoryNode { NodeName = value, NodeFullName = value };
                treeNodes.Add(_startDirectory);
                _currDirectory = _startDirectory;
            }
        }
        public string CurrentDirectory { get => _currDirectory.NodeName; }

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
            _nodes = new Stack<DirectoryNode>();
            _currDirectory = _startDirectory;
            _nodes.Push(_currDirectory);
            _countFound = 0;
            _allCountFound = 0;
        }

        private void Seeking()
        {
            CheckDirectoris(_currDirectory);
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
                    _allCountFound++;
                    if (Regex.IsMatch(file.Name, _format))
                    {
                        _countFound++;
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
    }
}

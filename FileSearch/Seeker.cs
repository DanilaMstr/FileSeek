using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace FileSearch
{
    public class Seeker
    {
        private Thread _thread;
        private ManualResetEventSlim _event = new ManualResetEventSlim(false);
        private uint _countFound = 0;
        private uint _allCountFound = 0;
        private Regex _format = new Regex(string.Empty);
        private DirectoryNode _startDirectory;
        private DirectoryNode _currDirectory;
        private Stack<DirectoryNode> _nodes;

        public uint Found { get => _countFound; }
        public uint AllFound { get => _allCountFound; }
        public string Format { set => _format = new Regex(Regex.Escape(value)); }
        public string StartDirectory { set => _startDirectory = new DirectoryNode { NodeName = value }; }
        public string CurrentDirectory { get => _currDirectory.NodeName; }

        public Seeker() { }

        public void Seek()
        {
            Restart();
            _thread = new Thread(()
                => Seeking()) { IsBackground = true };
            _thread.Start();
        }

        private void Restart()
        {
            _nodes = new Stack<DirectoryNode>();
            _currDirectory = _startDirectory;
            _countFound = 0;
            _allCountFound = 0;
        }

        private void Seeking()
        {
            _nodes.Push(_startDirectory);

            while (CheckStop())
            {
                _currDirectory = _nodes.Pop();
                CheckDirectory();
                CheckFiles();
            }
        }

        private void CheckFiles()
        {
            var currDirInfo = new DirectoryInfo(_currDirectory.NodeName);
            foreach (var file in currDirInfo.GetFiles())
            {
                _allCountFound++;
                if (_format.IsMatch(file.Name))
                {
                    _countFound++;
                    _currDirectory.Items.Add(new FileNode { NodeName = file.Name });
                }    
            }
        }

        private void CheckDirectory()
        {
            var currDirInfo = new DirectoryInfo(_currDirectory.NodeName);
            foreach (var dir in currDirInfo.GetDirectories())
            {
                _currDirectory.Items.Add(new DirectoryNode { NodeName = dir.Name });
            }
        }

        private bool CheckStop() => _nodes.Count > 0;
    }
}

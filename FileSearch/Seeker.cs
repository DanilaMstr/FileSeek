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

        public uint Found { get => _countFound; }
        public uint AllFound { get => _allCountFound; }
        public string Format { set => _format = new Regex(Regex.Escape(value)); }
        public string StartDirectory { set => _startDirectory = new DirectoryNode { NodeName = value }; }

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
            _countFound = 0;
            _allCountFound = 0;
        }

        private void Seeking()
        {

        }

        private void CheckFiles()
        {

        }

        private void CheckDirectory()
        {
            
        }
    }
}

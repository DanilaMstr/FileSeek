using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    public class DirectoryNode : TreeNode
    {
        private string _nodeFullName;
        public string NodeFullName
        {
            get => _nodeFullName;
            set
            {
                _nodeFullName = value;
                OnPropertyChanged();
            }
        }

        public DirectoryNode()
        {
            _items = new System.Collections.ObjectModel.ObservableCollection<TreeNode>();
        }
    }
}

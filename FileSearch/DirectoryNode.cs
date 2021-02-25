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

        public bool HasFile
        {
            get
            {
                foreach (var i in _items)
                {
                    if (i is FileNode)
                        return true;
                    else
                    {
                        if (i is DirectoryNode)
                            return ((DirectoryNode)i).HasFile;
                    }
                }
                return false;
            }
        }

        public string NodeFullName
        {
            get => _nodeFullName;
            set
            {
                _nodeFullName = value;
                OnPropertyChanged();
            }
        }

        private bool HasFileOnItems(TreeNode nodes) => nodes.Items.Any(n => n is FileNode);

        public DirectoryNode()
        {
            _items = new System.Collections.ObjectModel.ObservableCollection<TreeNode>();
        }
    }
}

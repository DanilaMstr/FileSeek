using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    public class DirectoryNode : TreeNode
    {
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

        public DirectoryNode()
        {
            _items = new System.Collections.ObjectModel.ObservableCollection<TreeNode>();
        }
    }
}

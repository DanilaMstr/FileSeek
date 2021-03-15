using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    public abstract class TreeNode : NotifyPropertyChanged
    {
        protected string _nodeName;
        protected string _nodeFullName;
        protected ObservableCollection<TreeNode> _items;

        public string NodeFullName
        {
            get => _nodeFullName;
            set
            {
                _nodeFullName = value;
                OnPropertyChanged();
            }
        }

        public string NodeName
        {
            get => _nodeName;
            set
            {
                _nodeName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TreeNode> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    public class TreeNode : NotifyPropertyChanged
    {
        private string _nodeName;
        private ObservableCollection<TreeNode> _items;

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

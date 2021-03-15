using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FileSearch
{
    public class FileNode : TreeNode
    {
        public ICommand CopyCommand { get; }
        public ICommand DeleteCommand { get; }
        public FileNode() 
        {
            _items = new System.Collections.ObjectModel.ObservableCollection<TreeNode>();
            CopyCommand = new SimpleCommand(OnCopy);
            DeleteCommand = new SimpleCommand(OnDelete);
        }

        void OnCopy()
        {
            Clipboard.SetData(DataFormats.Text, (Object)this.NodeFullName); 
        }
        void OnDelete()
        {
            System.IO.File.Delete(this.NodeFullName);
        }
    }
}

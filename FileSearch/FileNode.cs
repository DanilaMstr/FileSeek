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
        public ICommand CopyFullFilePathCommand { get; }
        public ICommand CopyFileNameCommand { get; }
        public ICommand DeleteCommand { get; }
        public FileNode() 
        {
            _items = new System.Collections.ObjectModel.ObservableCollection<TreeNode>();
            CopyFullFilePathCommand = new SimpleCommand(OnCopyFullFilePath);
            CopyFileNameCommand = new SimpleCommand(OnCopyFileName);
            DeleteCommand = new SimpleCommand(OnDelete);
        }

        void OnCopyFullFilePath()
        {
            Clipboard.SetData(DataFormats.Text, (Object)this.NodeFullName); 
        }
        void OnCopyFileName()
        {
            Clipboard.SetData(DataFormats.Text, (Object)this.NodeName);
        }
        void OnDelete()
        {
            System.IO.File.Delete(this.NodeFullName);
        }
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace DAR
{
    public class TreeViewModel : INotifyPropertyChanged
    {
        public TreeViewModel(string name)
        {
            Name = name;
            Children = new ObservableCollection<TreeViewModel>();
        }
        #region Properties
        public string Name { get; private set; }
        public ObservableCollection<TreeViewModel> Children { get; private set; }
        public bool IsInitiallySelected { get; private set; }
        public string Type { get; set; }
        public int Id { get; set; }
        bool? _isChecked = false;
        TreeViewModel _parent;
        #endregion
        #region IsChecked
        public bool? IsChecked
        {
            get { return _isChecked; }
            set { SetIsChecked(value, true, true); }
        }
        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
            {
                return;
            }
            if(_isChecked == false)
            {
                NotifyTriggerAdded(Id.ToString());
            }
            else
            {
                NotifyTriggerRemoved(Id.ToString());
            }
            _isChecked = value;

            if (updateChildren && _isChecked.HasValue)
            {
                foreach(TreeViewModel tvm in Children)
                {
                    tvm.SetIsChecked(_isChecked, true, false);
                }
                //Children.ForEach(c => c.SetIsChecked(_isChecked, true, false));
            }

            if (updateParent && _parent != null) _parent.VerifyCheckedState();

            NotifyPropertyChanged("IsChecked");
            
        }
        public void VerifyCheckedState()
        {
            bool? state = null;

            for (int i = 0; i < Children.Count; ++i)
            {
                bool? current = Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }

            SetIsChecked(state, false, true);
        }
        #endregion
        public void Initialize()
        {
            foreach (TreeViewModel child in Children)
            {
                child._parent = this;
                child.Initialize();
            }
            
        }
        public void RemoveChild(TreeViewModel removeview)
        {
            Children.Remove(removeview);
        }
        public static ObservableCollection<TreeViewModel> SetTree(string topLevelName)
        {
            ObservableCollection<TreeViewModel> treeView = new ObservableCollection<TreeViewModel>();
            TreeViewModel tv = new TreeViewModel(topLevelName);
            treeView.Add(tv);
            tv.Initialize();
            return treeView;
        }
        #region INotifyPropertyChanged Members
        void NotifyTriggerRemoved(string info)
        {
            if(TriggerRemoved != null)
            {
                TriggerRemoved(this, new PropertyChangedEventArgs(info));
            }
        }
        void NotifyTriggerAdded(string info)
        {
            if (TriggerAdded != null)
            {
                TriggerAdded(this, new PropertyChangedEventArgs(info));
            }
        }
        void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler TriggerAdded;
        public event PropertyChangedEventHandler TriggerRemoved;

        #endregion
    }
}

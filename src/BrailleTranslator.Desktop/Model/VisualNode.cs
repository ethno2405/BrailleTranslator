using System;
using System.Windows;
using GalaSoft.MvvmLight;

namespace BrailleTranslator.Desktop.Model
{
    public abstract class VisualNode : ObservableObject
    {
        private bool _isSelected;

        private bool _isExpanded = true;

        private bool _isFocused;

        private string _title;

        public VisualNode()
        {
            _title = GetType().Name;
        }

        public VisualNode(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));

            _title = title;
        }

        public virtual bool IsVisible { get; } = true;

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                Set(nameof(IsExpanded), ref _isExpanded, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                Set(nameof(IsSelected), ref _isSelected, value);
            }
        }

        public bool IsFocused
        {
            get
            {
                return _isFocused;
            }
            set
            {
                if (Set(nameof(IsFocused), ref _isFocused, value))
                {
                    MessageBox.Show(_isFocused.ToString());
                }
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                Set(nameof(Title), ref _title, value);
            }
        }
    }
}
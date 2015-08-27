using System;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using BrailleTranslator.Desktop.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;

namespace BrailleTranslator.Desktop.Model
{
    public class FlowDocumentWrapper : ObservableObject
    {
        private FlowDocument _document;

        private string _title;

        public FlowDocumentWrapper(string title) : this(title, new FlowDocument())
        {
        }

        public FlowDocumentWrapper(string title, FlowDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));

            _document = document;
            _title = title;
            DeleteComponentCommand = new RelayCommand<Component>(DeleteComponent);

            LoadChildren();
        }

        public ICommand DeleteComponentCommand { get; }

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

        public FlowDocument Document
        {
            get
            {
                return _document;
            }
            set
            {
                _document = value;
                LoadChildren();

                RaisePropertyChanged(nameof(Document));
                RaisePropertyChanged(nameof(PlainText));
            }
        }

        public string PlainText
        {
            get
            {
                return new TextRange(_document.ContentStart, _document.ContentEnd).Text;
            }
        }

        public ObservableCollection<BlockComponent> Children { get; } = new ObservableCollection<BlockComponent>();

        private void DeleteComponent(Component component)
        {
        }

        private void LoadChildren()
        {
            var componentFactory = SimpleIoc.Default.GetInstance<IComponentFactory>();

            Children.Clear();

            foreach (var block in _document.Blocks)
            {
                var child = componentFactory.CreateBlockComponent(block);

                child.Parent = this;
                Children.Add(child);
            }
        }
    }
}
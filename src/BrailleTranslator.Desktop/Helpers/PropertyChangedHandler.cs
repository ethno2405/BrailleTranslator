using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BrailleTranslator.Desktop.Helpers
{
    public class PropertyChangedHandler : IDisposable
    {
        private List<string> _properties = new List<string>();

        private List<Func<bool>> _predicates = new List<Func<bool>>();

        private Action _callback;

        private Action _disposeAction;

        private Func<bool> _disposeCondition;

        private INotifyPropertyChanged _observable;

        internal PropertyChangedHandler(INotifyPropertyChanged observable, string propertyName)
        {
            if (observable == null) throw new ArgumentNullException(nameof(observable));
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            AndProperty(propertyName);

            _observable = observable;
            _observable.PropertyChanged += OnPropertyChanged;
            _disposeAction = () => _observable.PropertyChanged -= OnPropertyChanged;
        }

        ~PropertyChangedHandler()
        {
            Dispose(false);
        }

        protected bool Disposed { get; set; }

        public PropertyChangedHandler ForProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            return new PropertyChangedHandler(_observable, propertyName);
        }

        public PropertyChangedHandler AndProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            _properties.Add(propertyName);
            return this;
        }

        public PropertyChangedHandler When(Func<bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            _predicates.Add(predicate);
            return this;
        }

        public PropertyChangedHandler DisposeWhen(Func<bool> disposeCondition)
        {
            if (disposeCondition == null) throw new ArgumentNullException(nameof(disposeCondition));

            _disposeCondition = disposeCondition;
            return this;
        }

        public PropertyChangedHandler Subscribe(Action callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));

            _callback = callback;

            return this;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed) return;

            if (disposing)
            {
                _disposeCondition = () => true;
                _disposeAction();
                _properties.Clear();
                _predicates.Clear();
                Disposed = true;
                GC.SuppressFinalize(this);
            }
        }

        private void OnPropertyChanged(object a, PropertyChangedEventArgs b)
        {
            var handle = true;

            foreach (var cond in _predicates)
            {
                if (!cond())
                {
                    handle = false;
                    break;
                }
            }

            if (handle && _properties.Contains(b.PropertyName) && _callback != null)
            {
                _callback();
            }

            if (_disposeCondition != null && _disposeCondition())
            {
                Dispose();
            }
        }
    }
}
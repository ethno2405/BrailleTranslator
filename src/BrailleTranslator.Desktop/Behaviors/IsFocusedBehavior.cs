using System.Windows;
using System.Windows.Interactivity;

namespace BrailleTranslator.Desktop.Behaviors
{
    public class IsFocusedBehavior : Behavior<UIElement>
    {
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(IsFocusedBehavior), new PropertyMetadata(OnIsFocusedChanged));

        public bool IsFocused
        {
            get
            {
                return (bool)GetValue(IsFocusedProperty);
            }
            set
            {
                SetValue(IsFocusedProperty, value);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.GotFocus += OnAssociatedObjectGotFocus;
            AssociatedObject.LostFocus += OnAssociatedObjectLostFocus;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.GotFocus -= OnAssociatedObjectGotFocus;
            AssociatedObject.LostFocus -= OnAssociatedObjectLostFocus;

            base.OnDetaching();
        }

        private static void OnIsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as IsFocusedBehavior;

            if (behavior == null) return;

            if ((bool)e.NewValue)
            {
                behavior.AssociatedObject.Focus();
            }
        }

        private void OnAssociatedObjectLostFocus(object sender, RoutedEventArgs e)
        {
            IsFocused = false;
        }

        private void OnAssociatedObjectGotFocus(object sender, RoutedEventArgs e)
        {
            IsFocused = true;
        }
    }
}
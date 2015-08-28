using System.Windows;
using System.Windows.Interactivity;

namespace BrailleTranslator.Desktop.Behaviors
{
    public class IsFocusedBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.GotFocus += OnAssociatedObjectGotFocus;
            AssociatedObject.LostFocus += OnAssociatedObjectLostFocus;
        }

        private void OnAssociatedObjectLostFocus(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void OnAssociatedObjectGotFocus(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
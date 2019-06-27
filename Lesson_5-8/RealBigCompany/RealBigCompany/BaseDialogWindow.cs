using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace RealBigCompany
{
    public class BaseDialogWindow : Window
    {
        public BaseDialogWindow()
        {
            DataContextChanged += OnDataContextChanged;

        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ManageClose(e);
        }

        private void ManageClose(DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is IClose)
            {
                ((IClose)e.OldValue).RequestClose -= OnCloseRequested;
                Closed -= ((IClose)e.OldValue).OnClosed;
            }

            if (e.NewValue is IClose)
            {
                ((IClose)e.NewValue).RequestClose += OnCloseRequested;
                Closed += ((IClose)e.NewValue).OnClosed;
            }
        }

        private void OnCloseRequested(object sender, EventArgs e)
        {
            Close();
        }
    }
}

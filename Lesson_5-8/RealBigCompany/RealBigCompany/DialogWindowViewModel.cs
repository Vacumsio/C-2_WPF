using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealBigCompany
{
    public class DialogWindowViewModel : Notify, IClose
    {
        public event EventHandler RequestClose;
        public virtual void OnClosed(object sender, EventArgs eventArgs)
        {
        }

        public DialogWindowViewModel()
        {
            OkCommand = new RelayCommand(o => Ok(), CanPressOk);
            CancelCommand = new RelayCommand(o => Cancel(), u => CanPressCancel(u));
        }

        private void Cancel()
        {
            DialogResult = false;
        }

        private void Ok()
        {
            DialogResult = true;
        }

        private bool? _dialogResult = false;
        public bool? DialogResult
        {
            get { return _dialogResult; }
            private set
            {
                _dialogResult = value;
                var r = RequestClose;
                if (r != null)
                    r(this, EventArgs.Empty);
            }
        }

        public ICommand OkCommand { get; set; }

        public ICommand CancelCommand { get; }

        protected virtual bool CanPressOk(object arg)
        {
            return true;
        }

        protected virtual bool CanPressCancel(object obj)
        {
            return true;
        }

    }
}

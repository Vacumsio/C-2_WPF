using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBigCompany
{
    interface IClose
    {
        event EventHandler RequestClose;
        void OnClosed(object sender, EventArgs eventArgs);
    }
}

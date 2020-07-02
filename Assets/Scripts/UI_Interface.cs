using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Assets
{
    interface UI_Interface
    {
        event ElapsedEventHandler TimeCallBack;
        void TimeStart();
    }
}

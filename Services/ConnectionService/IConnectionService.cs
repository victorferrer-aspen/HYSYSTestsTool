using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Connection
{
    interface IConnectionService
    {
        void Connect();
        void Disconect();
    }
}

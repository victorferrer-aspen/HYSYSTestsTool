using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWrapper.Tests
{
    public interface ITest
    {
        void StartTest();
        bool OpenSimulator();
        void CloseSimulator();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul3HW7
{
    public class BusinessException : Exception
    {
        public BusinessException(string msg)
            : base(msg)
        {
        }

        public static BusinessException SkippedLogic(string message = null)
        {
            return new BusinessException(message);
        }
    }
}

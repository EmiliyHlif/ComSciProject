using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComSciProject
{
    internal class loginmanager
    {
        public loginmanager()
        {

        }

        private int currentLogMId = 1;
        public int getCurLID()
        {
            return currentLogMId;
        }
    }

    
}

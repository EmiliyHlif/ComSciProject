using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ComSciProject
{
    internal class databaseCon
    {
        private static String con = "server=EMILYWORK;database=ComSciProject;UID=TestUser;password=Test1234";
        public static String getCon()
        {
            return con;
        }
    }
}

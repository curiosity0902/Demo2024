using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2024.DB
{
    public class DBConnection
    {
        public static SchoolPracticeEntities schoolPractice = new SchoolPracticeEntities();

        public static Client loginedClient;
    }
}

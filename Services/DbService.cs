using prakt15_Leshukov_TRPO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15_Leshukov_TRPO.Services
{
    public class DbService
    {
        private Prakt15LeshukovTrpoContext context;
        public Prakt15LeshukovTrpoContext Context => context;
        private static DbService? instance;

        public static DbService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbService();
                }
                return instance;
            }
        } 
        private DbService()
        {
            context = new Prakt15LeshukovTrpoContext();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.WPF
{
    public class Globals
    {
        private DateTime _gamedate;

        public static Globals Instance
        {
            get;
            private set;
        }

        static Globals()
        {
            Instance = new Globals(DateTime.MinValue);
        }

        private Globals(DateTime gameDate)
        {
            _gamedate = gameDate;
        }

        public void SetGameDate(DateTime gameDate)
        {
            _gamedate = gameDate;
        }

        public DateTime GameDate()
        {
            return _gamedate;
        }
    }
}

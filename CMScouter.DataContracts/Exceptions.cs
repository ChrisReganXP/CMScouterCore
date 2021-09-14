using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.DataContracts
{
    public class LoadSaveFileException : Exception
    {
        public LoadSaveFileException(string message)
            : base(message)
        {
        }
    }
}

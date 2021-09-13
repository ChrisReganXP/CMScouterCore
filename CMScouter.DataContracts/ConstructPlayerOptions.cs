using CMScouter.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.DataContracts
{
    public class ConstructPlayerOptions
    {
        public PlayerPosition setPosition { get; set; }

        public PlayerPosition movementPosition { get; set; }
    }
}

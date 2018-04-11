using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon2
{
    public class IrcCommand
    {
        public string Command;

        public IrcCommand()
        {
        }

        public override string ToString()
        {
            return Command;
        }
    }
}

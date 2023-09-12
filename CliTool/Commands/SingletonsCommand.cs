using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class SingletonsCommand : Command
    {
        public SingletonsCommand()
            : base("singletons", "command to list singletons in the OData csdl xml file.")
        {
        }
    }
}

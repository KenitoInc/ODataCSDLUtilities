using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class FunctionsCommand : Command
    {
        public FunctionsCommand()
            : base("functions", "command to list functions in the OData csdl xml file.")
        {
        }
    }
}

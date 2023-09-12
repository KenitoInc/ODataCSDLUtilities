using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class ActionsCommand : Command
    {
        public ActionsCommand()
            : base("actions", "command to list actions in the OData csdl xml file.")
        {
        }
    }
}

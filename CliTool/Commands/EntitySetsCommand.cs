using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class EntitySetsCommand : Command
    {
        public EntitySetsCommand()
            : base("entitysets", "command to list entity sets in the OData csdl xml file.")
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class EnumTypesCommand : Command
    {
        public EnumTypesCommand()
            : base("enumtypes", "command to list enum types in the OData csdl xml file.")
        {
        }
    }
}

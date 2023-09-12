using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class ComplexTypesCommand : Command
    {
        public ComplexTypesCommand()
            : base("complextypes", "command to list complex types in the OData csdl xml file.")
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class FunctionImportsCommand : Command
    {
        public FunctionImportsCommand()
            : base("functionimports", "command to list function imports in the OData csdl xml file.")
        {
        }
    }
}

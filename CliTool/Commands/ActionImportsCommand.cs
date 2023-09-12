using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class ActionImportsCommand : Command
    {
        public ActionImportsCommand()
            : base("actionimports", "command to list action imports in the OData csdl xml file.")
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class ShowCommand : Command
    {
        public ShowCommand()
            : base("show", "commands to list types in the OData csdl xml file.")
        {
            AddGlobalOption(Utils.FilePathOption);
        }
    }
}

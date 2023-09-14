using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class AddCommand : Command
    {
        public AddCommand()
            : base("add", "commands to add a type in the OData csdl xml file.")
        {
            AddGlobalOption(Utils.FilePathOption);
        }
    }
}

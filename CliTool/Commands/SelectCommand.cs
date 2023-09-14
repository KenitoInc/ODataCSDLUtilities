using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class SelectCommand : Command
    {
        public SelectCommand()
            : base("select", "command to select an element in the OData csdl xml file.")
        {
            Add(Utils.SelectNameOption);

            AddGlobalOption(Utils.FilePathOption);
        }
    }
}

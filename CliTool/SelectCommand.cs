using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool
{
    internal class SelectCommand : Command
    {
        public SelectCommand()
            : base("select", "Commands to select types in the OData csdl xml file.")
        {
        }
    }
}

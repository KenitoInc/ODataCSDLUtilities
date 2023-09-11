using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool
{
    internal class LoadCommand : Command
    {
        public LoadCommand()
            : base("load", "loads the OData csdl xml file.")
        {
            Option filePath = new Option<string>(new[] { "--file", "-f" })
            {
                Name = "file",
                Description = "The URI of the xml file.",
                IsRequired = true
            };

            this.AddOption(filePath);
        }
    }
}

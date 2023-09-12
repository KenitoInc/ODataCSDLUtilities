using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class FunctionImportCommand : Command
    {
        public FunctionImportCommand()
            : base("functionimport", "command to show a function in the OData csdl xml file.")
        {
            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the function import.",
                IsRequired = true
            };

            Add(nameOption);
        }
    }
}

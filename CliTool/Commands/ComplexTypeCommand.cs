using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class ComplexTypeCommand : Command
    {
        public ComplexTypeCommand()
            : base("complextype", "command to show a complex type in the OData csdl xml file.")
        {
            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the complex type.",
                IsRequired = true
            };

            Add(nameOption);
        }
    }
}

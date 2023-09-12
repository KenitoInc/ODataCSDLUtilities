using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class EnumTypeCommand : Command
    {
        public EnumTypeCommand()
            : base("enumtype", "command to show an enum type in the OData csdl xml file.")
        {
            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the enum type.",
                IsRequired = true
            };

            Add(nameOption);
        }
    }
}

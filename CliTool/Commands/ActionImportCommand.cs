using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class ActionImportCommand : Command
    {
        public ActionImportCommand()
            : base("actionimport", "command to show an action import in the OData csdl xml file.")
        {
            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the action import.",
                IsRequired = true
            };

            Add(nameOption);
        }
    }
}

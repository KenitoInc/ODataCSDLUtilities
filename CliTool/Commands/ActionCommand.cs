using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class ActionCommand : Command
    {
        public ActionCommand()
            : base("action", "command to show an action in the OData csdl xml file.")
        {
            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the action.",
                IsRequired = true
            };

            Add(nameOption);
        }
    }
}

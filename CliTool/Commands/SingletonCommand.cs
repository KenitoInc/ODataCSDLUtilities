using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class SingletonCommand : Command
    {
        public SingletonCommand()
            : base("singleton", "command to show a singleton in the OData csdl xml file.")
        {
            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the singleton.",
                IsRequired = true
            };

            Add(nameOption);
        }
    }
}

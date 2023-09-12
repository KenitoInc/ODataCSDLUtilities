using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class EntitySetCommand : Command
    {
        public EntitySetCommand()
            : base("entityset", "command to show an entity set in the OData csdl xml file.")
        {
            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the entity set.",
                IsRequired = true
            };

            Add(nameOption);
        }
    }
}

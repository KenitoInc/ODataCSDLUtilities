using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class EntityTypeCommand : Command
    {
        public EntityTypeCommand()
            : base("entitytype", "command to show an entity type in the OData csdl xml file.")
        {
            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the entity type.",
                IsRequired = true
            };

            Add(nameOption);
        }
    }
}

using CsdlXPathLib.EdmTypes;
using CsdlXPathLib;
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
            : base("entitytype", "command to add an entity type to the OData csdl xml file.")
        {
            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the entity type.",
                IsRequired = true
            };

            Option<string> baseTypeOption = new Option<string>(new[] { "--basetype", "-t" })
            {
                Name = "type",
                Description = "The basetype of the entity type."
            };

            Add(nameOption);
            Add(baseTypeOption);

            this.SetHandler((filePathOptionValue, nameOptionValue, baseTypeOptionValue) =>
            {
                InvokeCommand(filePathOptionValue, nameOptionValue, baseTypeOptionValue);
            },
            Utils.FilePathOption, nameOption, baseTypeOption);
        }

        private static void InvokeCommand(string filePath, string name, string baseType)
        {
            if (filePath == null)
            {
                Console.WriteLine("File path has not been set.");
                Console.WriteLine($"Ensure you add the option --file \"/path/to/file.xml\"");

                return;
            }

            if (name == null)
            {
                Console.WriteLine($"Ensure you add the option --name");

                return;
            }

            EntityType entityType = new EntityType();
            entityType.Name = name;
            entityType.BaseType= baseType;

            CsdlXPath csdlXPath = new CsdlXPath(filePath, /* isReadOnly */ false);
            csdlXPath.AddEntityType(entityType);
        }
    }
}
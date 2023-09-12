using CsdlXPathLib.EdmTypes;
using CsdlXPathLib;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class EntityTypesCommand : Command
    {
        public EntityTypesCommand()
            : base("entitytypes", "command to list entity types in the OData csdl xml file.")
        {
            this.SetHandler((filePathOptionValue) =>
            {
                InvokeCommand(filePathOptionValue);
            },
            Utils.FilePathOption);
        }

        private static void InvokeCommand(string filePath)
        {
            if (filePath == null)
            {
                Console.WriteLine("File path has not been set.");
                Console.WriteLine($"Ensure you add the option --file \"/path/to/file.xml\"");

                return;
            }

            CsdlXPath csdlXPath = new CsdlXPath(filePath);
            List<EntityType> entityTypes = csdlXPath.GetEntityTypes();

            Console.WriteLine("***Entity Types***");

            foreach (EntityType entityType in entityTypes)
            {
                Console.WriteLine($"{entityType.Name}");
            }
        }
    }
}

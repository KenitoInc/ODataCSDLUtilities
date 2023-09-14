using CsdlXPathLib.EdmTypes;
using CsdlXPathLib;
using System.CommandLine;

namespace CliTool.Commands
{
    internal class PropertiesCommand : Command
    {
        public PropertiesCommand()
            : base("properties", "command to show properties of a type in the OData csdl xml file.")
        {
            Option<string> entityTypeOption = new Option<string>(new[] { "--entitytype", "-etype" })
            {
                Name = "entitytype",
                Description = "The name of the entity type.",
            };

            Option<string> complexTypeOption = new Option<string>(new[] { "--complextype", "-ctype" })
            {
                Name = "complextype",
                Description = "The name of the complex type.",
            };

            Add(entityTypeOption);
            Add(complexTypeOption);

            this.SetHandler((filePathOptionValue, entityTypeOptionValue, complexTypeOptionValue) =>
            {
                InvokeCommand(filePathOptionValue, entityTypeOptionValue, complexTypeOptionValue);
            },
            Utils.FilePathOption, entityTypeOption, complexTypeOption);
        }

        private static void InvokeCommand(string filePath, string entityType, string complexType)
        {
            if (filePath == null)
            {
                Console.WriteLine("File path has not been set.");
                Console.WriteLine($"Ensure you add the option --file \"/path/to/file.xml\"");

                return;
            }

            CsdlXPath csdlXPath = new CsdlXPath(filePath);

            // TODO: Vaidate that you can have both entityType and complexType

            if (!string.IsNullOrEmpty(entityType))
            {
                List<EdmProperty> properties = csdlXPath.GetProperties(entityType);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("***Properties***");
                Console.ForegroundColor = ConsoleColor.Green;

                foreach (EdmProperty edmProperty in properties)
                {
                    Console.WriteLine($"Name: {edmProperty.Name}, Type: {edmProperty.Type}, Nullable: {edmProperty.Nullable}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (!string.IsNullOrEmpty(complexType))
            {
                List<EdmProperty> properties = csdlXPath.GetProperties(complexType,/* isEntity */ false);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("***Properties***");
                Console.ForegroundColor = ConsoleColor.Green;

                foreach (EdmProperty edmProperty in properties)
                {
                    Console.WriteLine($"Name: {edmProperty.Name}, Type: {edmProperty.Type}, Nullable: {edmProperty.Nullable}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}

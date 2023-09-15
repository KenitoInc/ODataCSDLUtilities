using CsdlXPathLib.EdmTypes;
using CsdlXPathLib;
using System.CommandLine;

namespace CliTool.Commands
{
    internal class PropertyCommand : Command
    {
        public PropertyCommand()
            : base("property", "command to add a property of a type in the OData csdl xml file.")
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

            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the property.",
                IsRequired = true,
            };

            Option<bool> keyOption = new Option<bool>(new[] { "--key", "-k" })
            {
                Name = "key",
                Description = "If the property is a key.",
            };

            Option<bool> nullableOption = new Option<bool>(new[] { "--nullable", "-nb" })
            {
                Name = "nullable",
                Description = "If the property is nullable.",
            };

            nullableOption.SetDefaultValue(true);

            Option<string> typeOption = new Option<string>(new[] { "--type", "-t" })
            {
                Name = "type",
                Description = "The type of the property.",
                IsRequired = true,
            };

            Add(entityTypeOption);
            Add(complexTypeOption);
            Add(typeOption);
            Add(nameOption);
            Add(keyOption);
            Add(nullableOption);

            this.SetHandler((filePathOptionValue, entityTypeOptionValue, complexTypeOptionValue, typeOptionValue, nameOptionValue, keyOptionValue, nullableOptionValue) =>
            {
                InvokeCommand(filePathOptionValue, entityTypeOptionValue, complexTypeOptionValue, typeOptionValue, nameOptionValue, keyOptionValue, nullableOptionValue);
            },
            Utils.FilePathOption, entityTypeOption, complexTypeOption, typeOption, nameOption, keyOption, nullableOption);
        }

        private static void InvokeCommand(string filePath, string entityType, string complexType, string type, string name, bool key, bool nullable)
        {
            if (filePath == null)
            {
                Console.WriteLine("File path has not been set.");
                Console.WriteLine($"Ensure you add the option --file \"/path/to/file.xml\"");

                return;
            }

            CsdlXPath csdlXPath = new CsdlXPath(filePath, false);
            EdmProperty property = new EdmProperty();
            property.Name = name;
            property.Type = type;
            property.Nullable = nullable;

            if (!string.IsNullOrEmpty(entityType))
            {
                csdlXPath.AddProperty(property, entityType);
            }

            if (!string.IsNullOrEmpty(complexType))
            {
            }
        }
    }
}

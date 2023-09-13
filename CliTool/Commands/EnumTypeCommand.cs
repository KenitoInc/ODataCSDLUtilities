using CsdlXPathLib.EdmTypes;
using CsdlXPathLib;
using System.CommandLine;

namespace CliTool.Commands
{
    public class EnumTypeCommand : Command
    {
        private string nameOptionValue;

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

            this.SetHandler((filePathOptionValue, nameOptionValue) =>
            {
                InvokeCommand(filePathOptionValue, nameOptionValue);
            },
            Utils.FilePathOption, nameOption);
        }

        private static void InvokeCommand(string filePath, string name)
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

            CsdlXPath csdlXPath = new CsdlXPath(filePath);
            EnumType  enumType = csdlXPath.GetEnumType(name);

            Console.WriteLine("***Enum Type***");
            Console.WriteLine($"{enumType.Name}");
        }
    }
}
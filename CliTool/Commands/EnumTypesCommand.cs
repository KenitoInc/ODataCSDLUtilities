using CsdlXPathLib.EdmTypes;
using CsdlXPathLib;
using System.CommandLine;

namespace CliTool.Commands
{
    public class EnumTypesCommand : Command
    {
        public EnumTypesCommand()
            : base("enumtypes", "command to list enum types in the OData csdl xml file.")
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
            List<EnumType> enumTypes = csdlXPath.GetEnumTypes();

            Console.WriteLine("***Enum Types***");

            foreach (EnumType enumType in enumTypes)
            {
                Console.WriteLine($"{enumType.Name}");
            }
        }
    }
}

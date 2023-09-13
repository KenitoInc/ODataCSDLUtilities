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
    internal class ComplexTypesCommand : Command
    {
        public ComplexTypesCommand()
            : base("complextypes", "command to list complex types in the OData csdl xml file.")
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
            List<ComplexType> complexTypes = csdlXPath.GetComplexTypes();

            Console.WriteLine("***Complex Types***");

            foreach (ComplexType complexType in complexTypes)
            {
                Console.WriteLine($"{complexType.Name}");
            }
        }
    }
}
﻿using CsdlXPathLib.EdmTypes;
using CsdlXPathLib;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool.Commands
{
    internal class ComplexTypeCommand : Command
    {
        public ComplexTypeCommand()
            : base("complextype", "command to show a complex type in the OData csdl xml file.")
        {
            Option<string> nameOption = new Option<string>(new[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the complex type.",
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
            ComplexType complexType = csdlXPath.GetComplexType(name);

            Console.WriteLine("***Complex Type***");
            Console.WriteLine($"{complexType.Name}");
        }
    }
}

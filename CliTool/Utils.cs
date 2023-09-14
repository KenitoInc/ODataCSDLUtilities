using CsdlXPathLib.EdmTypes;
using CsdlXPathLib;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliTool.Commands;
using System.ComponentModel.Design;

namespace CliTool
{
    internal static class Utils
    {
        public static Option<string> FilePathOption = new Option<string>(new[] { "--file", "-f" })
        {
            Name = "file",
            Description = "The URI of the csdl document.",
            IsRequired = true
        };

        public static Option<string> SelectNameOption = new Option<string>(new[] { "--name", "-n" })
        {
            Name = "name",
            Description = "The name of the element.",
            IsRequired = true
        };

        public static RootCommand SetUpCommands()
        {
            ShowCommand showCommand = new ShowCommand();
            SelectCommand selectCommand = new SelectCommand();
            AddCommand addCommand = new AddCommand();

            // show subcommands
            EntityTypesCommand entityTypesCommand = new EntityTypesCommand();
            ComplexTypesCommand complexTypesCommand = new ComplexTypesCommand();
            EnumTypesCommand enumsCommand = new EnumTypesCommand();
            FunctionsCommand functionsCommand = new FunctionsCommand();
            ActionsCommand actionsCommand = new ActionsCommand();
            EntitySetsCommand entitySetsCommand = new EntitySetsCommand();
            SingletonsCommand singletonsCommand = new SingletonsCommand();
            ActionImportsCommand actionimportsCommand = new ActionImportsCommand();
            FunctionImportsCommand functionimportsCommand = new FunctionImportsCommand();
            PropertiesCommand propertiesCommand = new PropertiesCommand();

            // Add subcommands
            EntityTypeCommand entityTypeCommand = new EntityTypeCommand();
            ComplexTypeCommand complexTypeCommand = new ComplexTypeCommand();
            EnumTypeCommand enumCommand = new EnumTypeCommand();
            FunctionCommand functionCommand = new FunctionCommand();
            ActionCommand actionCommand = new ActionCommand();
            EntitySetCommand entitySetCommand = new EntitySetCommand();
            SingletonCommand singletonCommand = new SingletonCommand();
            ActionImportCommand actionimportCommand = new ActionImportCommand();
            FunctionImportCommand functionimportCommand = new FunctionImportCommand();

            showCommand.Add(entityTypesCommand);
            showCommand.Add(complexTypesCommand);
            showCommand.Add(enumsCommand);
            showCommand.Add(functionsCommand);
            showCommand.Add(actionsCommand);
            showCommand.Add(entitySetsCommand);
            showCommand.Add(singletonsCommand);
            showCommand.Add(actionimportsCommand);
            showCommand.Add(functionimportsCommand);
            showCommand.Add(propertiesCommand);

            addCommand.Add(entityTypeCommand);
            addCommand.Add(complexTypeCommand);
            addCommand.Add(enumCommand);
            addCommand.Add(functionCommand);
            addCommand.Add(actionCommand);
            addCommand.Add(entitySetCommand);
            addCommand.Add(singletonCommand);
            addCommand.Add(actionimportCommand);
            addCommand.Add(functionimportCommand);

            RootCommand app = new RootCommand {
                showCommand,
                selectCommand
            };

            return app;
        }

        public static void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

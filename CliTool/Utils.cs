using CsdlXPathLib.EdmTypes;
using CsdlXPathLib;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliTool.Commands;

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

            EntityTypesCommand entityTypesCommand = new EntityTypesCommand();
            EntityTypeCommand entityTypeCommand = new EntityTypeCommand();
            ComplexTypesCommand complexTypesCommand = new ComplexTypesCommand();
            ComplexTypeCommand complexTypeCommand = new ComplexTypeCommand();
            EnumTypesCommand enumsCommand = new EnumTypesCommand();
            EnumTypeCommand enumCommand = new EnumTypeCommand();
            FunctionsCommand functionsCommand = new FunctionsCommand();
            FunctionCommand functionCommand = new FunctionCommand();
            ActionsCommand actionsCommand = new ActionsCommand();
            ActionCommand actionCommand = new ActionCommand();
            EntitySetsCommand entitySetsCommand = new EntitySetsCommand();
            EntitySetCommand entitySetCommand = new EntitySetCommand();
            SingletonsCommand singletonsCommand = new SingletonsCommand();
            SingletonCommand singletonCommand = new SingletonCommand();
            ActionImportsCommand actionimportsCommand = new ActionImportsCommand();
            ActionImportCommand actionimportCommand = new ActionImportCommand();
            FunctionImportsCommand functionimportsCommand = new FunctionImportsCommand();
            FunctionImportCommand functionimportCommand = new FunctionImportCommand();
            PropertiesCommand propertiesCommand = new PropertiesCommand();

            showCommand.Add(entityTypesCommand);
            showCommand.Add(entityTypeCommand);
            showCommand.Add(complexTypesCommand);
            showCommand.Add(complexTypeCommand);
            showCommand.Add(enumsCommand);
            showCommand.Add(enumCommand);
            showCommand.Add(functionsCommand);
            showCommand.Add(functionCommand);
            showCommand.Add(actionsCommand);
            showCommand.Add(actionCommand);
            showCommand.Add(entitySetsCommand);
            showCommand.Add(entitySetCommand);
            showCommand.Add(singletonsCommand);
            showCommand.Add(singletonCommand);
            showCommand.Add(actionimportsCommand);
            showCommand.Add(actionimportCommand);
            showCommand.Add(functionimportsCommand);
            showCommand.Add(functionimportCommand);
            showCommand.Add(propertiesCommand);

            RootCommand app = new RootCommand {
                showCommand,
                selectCommand
            };

            return app;
        }
    }
}

using CliTool.Commands;
using CsdlXPathLib;
using CsdlXPathLib.EdmTypes;
using System;
using System.CommandLine;
using System.Runtime.CompilerServices;

namespace CliTool
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            ShowCommand showCommand = new ShowCommand();

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
            
            RootCommand app = new RootCommand {
                showCommand
            };

            return await app.InvokeAsync(args);
        }
    }
}
using System;
using System.CommandLine;

namespace CliTool
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            string filePath = null;
            ShowCommand showCommand = new ShowCommand();
            LoadCommand loadCommand = new LoadCommand();

            var filePathOption = new Option<string>("--file") { IsRequired = true };
            var getFileCommand = new Command("getfile","");
            getFileCommand.Add(filePathOption);

            Command entityTypesCommand = new Command("entitytypes", "");
            Command complexTypesCommand = new Command("complextypes", "");
            Command enumsCommand = new Command("enums", "");
            Command functionsCommand = new Command("functions", "");
            Command actionsCommand = new Command("actions", "");
            Command entitySetsCommand = new Command("entitysets", "");
            Command singletonsCommand = new Command("singletons", "");
            Command actionimportsCommand = new Command("actionimports", "");
            Command functionimportsCommand = new Command("functionimports", "");

            showCommand.Add(entityTypesCommand);
            showCommand.Add(complexTypesCommand);
            showCommand.Add(enumsCommand);
            showCommand.Add(functionsCommand);
            showCommand.Add(actionsCommand);
            showCommand.Add(entitySetsCommand);
            showCommand.Add(singletonsCommand);
            showCommand.Add(actionimportsCommand);
            showCommand.Add(functionimportsCommand);
            
            RootCommand app = new RootCommand {
                showCommand,
                loadCommand,
                getFileCommand
            };

            getFileCommand.SetHandler((path) =>
                {
                    filePath = path;
                },
                filePathOption);

            return await app.InvokeAsync(args);
        }
    }
}
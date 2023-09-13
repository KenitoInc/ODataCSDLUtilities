using CliTool.Commands;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool
{
    internal sealed class ReadEvalPrintLoop
    {
        Parser parser;
        CliCache cache;
        public ReadEvalPrintLoop(Parser parser)
        {
            this.parser = parser;
            this.cache = new CliCache();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the CSDL REPL (Read Eval Print Loop)!");
            Console.WriteLine("Type commands at the prompt and press Enter to evaluate them.");
            Console.WriteLine("Type Help to learn more, Exit to quit, and Clear to clear your terminal.");
            Console.WriteLine(string.Empty);

            while (true)
            {
                Console.WriteLine(string.Empty);
                Console.ForegroundColor = ConsoleColor.White;
                string commandText = Console.ReadLine();

                if (commandText == "exit") { break; }
                if (commandText == "clear") { Console.Clear(); continue; }
                if (new[] { "help", "#help", "?" }.Contains(commandText))
                {
                    PrintHelp();
                    continue;
                }

                commandText = HandleInput(commandText);

                int code = parser.Invoke(commandText);
            }
        }

        private string HandleInput(string commandText)
        {
            ParseResult parseResult = parser.Parse(commandText);

            if (IsSelectCommand(parseResult.CommandResult))
            {
                this.cache.Select = parseResult.GetValueForOption(Utils.SelectNameOption)?.ToString();
            }
            else
            {
                commandText = ApplyCachedNamedOption(commandText, parseResult);
                this.cache.Command = parseResult.CommandResult.Command;
            }

            if (parseResult.HasOption(Utils.FilePathOption))
            {
                this.cache.File = parseResult.GetValueForOption(Utils.FilePathOption)?.ToString();
            }
            else
            {
                commandText = string.Concat(commandText, " --file ", this.cache.File);
            }

            return commandText;
        }

        private string ApplyCachedNamedOption(string commandText, ParseResult parseResult)
        {
            if (parseResult.CommandResult.Command is PropertiesCommand)
            {
                if(!parseResult.HasOption(Utils.SelectNameOption))
                {
                    if (this.cache.Command is EntityTypesCommand)
                    {
                        commandText = string.Concat(commandText, " --entitytype ", this.cache.Select);
                    }
                    if (this.cache.Command is ComplexTypesCommand)
                    {
                        commandText = string.Concat(commandText, " --complextype ", this.cache.Select);
                    }
                }
            }

            return commandText;
        }

        private bool IsSelectCommand(CommandResult commandResult)
        {
            if (commandResult.Command is SelectCommand)
            {
                return true;
            }
            return false;
        }

        private void PrintHelp()
        {
            Console.WriteLine(
                $@"
Commands
===============
A few commands include

setfile --file /path/to/file

show entitytypes
show entitytype --name <EntityTypeName>
select <EntityTypeName>
show properties --entitytype <EntityTypeName>

Run --help to view more commands
"
            );
        }
    }
}

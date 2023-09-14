using CliTool.Commands;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Globalization;
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
                string breadcrumb = ">";

                foreach (Command cmd in this.cache.Commands)
                {
                    string str = cmd.Name;
                    if (cmd is SelectCommand)
                    {
                        str = this.cache.Select;
                    }
                    breadcrumb = string.Concat(str, "\\", breadcrumb);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(breadcrumb);
                Console.ForegroundColor = ConsoleColor.White;

                string commandText = Console.ReadLine();

                if (string.IsNullOrEmpty(commandText))
                {
                    continue;
                }

                if (commandText == "exit") { break; }
                if (commandText == "clear") { Console.Clear(); continue; }
                if (new[] { "help", "#help", "?" }.Contains(commandText))
                {
                    PrintHelp();
                    continue;
                }
                if (commandText == "back")
                {
                    this.cache.Commands.Pop();
                    continue;
                }

                string updatedCommandText;
                bool isInputValid = HandleInput(commandText, out updatedCommandText);

                if (isInputValid)
                {
                    parser.Invoke(updatedCommandText);
                }
            }
        }

        private bool HandleInput(string commandText, out string command)
        {
            command = commandText;
            ParseResult parseResult = parser.Parse(commandText);

            bool isValidFile = HandleFileOption(parseResult, commandText, out command);

            if (!isValidFile)
            {
                WriteErrorMessage("File path has not been set in this session");

                return false;
            }

            commandText = command;

            if (IsSelectCommand(parseResult.CommandResult))
            {
                bool isSelectValid = HandleSelect(parseResult);

                if (!isSelectValid)
                {
                    return false;
                }
            }
            else
            {
                ApplyCachedNamedOption(commandText, parseResult, out command);
            }

            AddCommandToCache(parseResult.CommandResult.Command);

            return true;
        }

        private bool HandleFileOption(ParseResult parseResult, string commandText, out string command)
        {
            command = commandText;
            if (parseResult.HasOption(Utils.FilePathOption))
            {
                this.cache.File = parseResult.GetValueForOption(Utils.FilePathOption)?.ToString();

                return true;
            }
            else
            {
                if (string.IsNullOrEmpty(this.cache.File))
                {
                    return false;
                }
                else
                {
                    command = string.Concat(commandText, " --file ", this.cache.File);

                    return true;
                }
            }
        }

        private void ApplyCachedNamedOption(string commandText, ParseResult parseResult, out string command)
        {
            command = commandText;

            if (parseResult.CommandResult.Command is PropertiesCommand)
            {
                if(!parseResult.HasOption(Utils.SelectNameOption))
                {
                    foreach (Command cmd in this.cache.Commands)
                    {
                        if (cmd is EntityTypesCommand)
                        {
                            command = string.Concat(commandText, " --entitytype ", this.cache.Select);
                            break;
                        }
                        if (cmd is ComplexTypesCommand)
                        {
                            command = string.Concat(commandText, " --complextype ", this.cache.Select);
                            break;
                        }
                    }
                }
            }
        }

        private void AddCommandToCache(Command command)
        {
            if (
                command is EntityTypesCommand ||
                command is ComplexTypesCommand ||
                command is SelectCommand
                )
            {
                Command cmd;
                this.cache.Commands.TryPeek(out cmd);

                if (cmd != command)
                {
                    this.cache.Commands.Push(command);
                }
            }
        }

        private bool HandleSelect(ParseResult parseResult)
        {
            Command command;
            this.cache.Commands.TryPeek(out command);

            if (
                command is EntityTypesCommand ||
                command is ComplexTypesCommand ||
                command is EnumTypesCommand ||
                command is FunctionsCommand ||
                command is ActionCommand ||
                command is FunctionImportsCommand ||
                command is ActionImportsCommand ||
                command is EntitySetsCommand ||
                command is SingletonsCommand
                )
            {
                if (parseResult.HasOption(Utils.SelectNameOption))
                {
                    this.cache.Select = parseResult.GetValueForOption(Utils.SelectNameOption)?.ToString();

                    return true;
                }
                else
                {
                    WriteErrorMessage("select command doesn't have --name option");
                }
            }
            else
            {
                WriteErrorMessage("select command cannot be applied");
            }

            return false;
        }

        private void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
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

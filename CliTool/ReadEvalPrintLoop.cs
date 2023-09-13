using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool
{
    internal sealed class ReadEvalPrintLoop
    {
        Parser parser;
        public ReadEvalPrintLoop(Parser parser)
        {
            this.parser = parser;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the CSDL REPL (Read Eval Print Loop)!");
            Console.WriteLine("Type commands at the prompt and press Enter to evaluate them.");
            Console.WriteLine("Type Help to learn more, Exit to quit, and Clear to clear your terminal.");
            Console.WriteLine(string.Empty);

            while (true)
            {
                string response = Console.ReadLine();
                string commandText = response.Trim().ToLowerInvariant();
                
                var result = parser.Parse(commandText);

                List<string> arguments = new List<string>();

                foreach (var x in result.Tokens)
                {
                    arguments.Add(x.Value);
                }

                //parser.InvokeAsync(commandText);
                parser.InvokeAsync(arguments.ToArray());

                if (commandText == "exit") { break; }
                if (commandText == "clear") { Console.Clear(); continue; }
                if (new[] { "help", "#help", "?" }.Contains(commandText))
                {
                    PrintHelp();
                    continue;
                }
            }
        }

        private void HandleInput(string commandText)
        {
        }

        private void PrintHelp()
        {
            Console.WriteLine(
                $@"
More details and screenshots are available at
https://github.com/waf/CSharpRepl/blob/main/README.md

Evaluating Code
===============
Configuration Options
=====================
All configuration, including theming, is done at startup via command line flags.
Run --help at the command line to view these options
"
            );
        }
    }
}

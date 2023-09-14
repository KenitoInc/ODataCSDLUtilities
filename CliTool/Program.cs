using CliTool.Commands;
using CsdlXPathLib;
using CsdlXPathLib.EdmTypes;
using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Runtime.CompilerServices;

namespace CliTool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RootCommand app = Utils.SetUpCommands();
            Parser parser = new Parser(app);

            new ReadEvalPrintLoop(parser).Run();
        }
    }
}
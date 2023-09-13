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
            //var result = parser.Parse(args);
            //var result2 = parser.Parse("show entitytype --name Person --file trippin.xml");


            //return await parser.InvokeAsync(args);
            new ReadEvalPrintLoop(parser).Run();
        }

        //private static ParseResult ParseArgs(string[] args)
        //{
        //    Parser parser = new Parser(app);

        //    var result = parser.Parse(args);

        //    return result;
        //}
    }
}
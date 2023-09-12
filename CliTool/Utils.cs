using CsdlXPathLib.EdmTypes;
using CsdlXPathLib;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

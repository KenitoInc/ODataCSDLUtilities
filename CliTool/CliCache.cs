using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTool
{
    internal class CliCache
    {
        public CliCache() { }

        public string Select { get; set; }
        public string File { get; set; }
        public Command Command { get; set; }
    }
}

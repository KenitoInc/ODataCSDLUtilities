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
        private Stack<Command> commands;
        public CliCache()
        {
            commands = new Stack<Command>();
        }

        public string Select { get; set; }
        public string File { get; set; }
        public Stack<Command> Commands
        {
            get
            {
                return commands;
            }
        }
    }
}

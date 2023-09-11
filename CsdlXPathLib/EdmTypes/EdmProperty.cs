using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsdlXPathLib.EdmTypes
{
    public class EdmProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string MaxLength { get; set; }
        public bool Nullable { get; set; }
    }
}

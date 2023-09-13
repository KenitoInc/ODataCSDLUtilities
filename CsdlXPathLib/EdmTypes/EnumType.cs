using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsdlXPathLib.EdmTypes
{
    public class EnumType
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public List<EdmMember> Members { get; set; }
    }
}

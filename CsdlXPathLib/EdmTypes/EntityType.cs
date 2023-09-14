using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsdlXPathLib.EdmTypes
{
    public class EntityType
    {
        public string Name { get; set; }
        public string BaseType { get; set; }
        public List<EdmProperty> Properties { get; set; }
        public List<EdmNavigationProperty> NavigationProperties { get; set; }
    }
}

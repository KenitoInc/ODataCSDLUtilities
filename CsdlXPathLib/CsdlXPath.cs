using CsdlXPathLib.EdmTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace CsdlXPathLib
{
    public class CsdlXPath
    {
        private XPathDocument document;
        private XPathNavigator navigator;
        private XmlNamespaceManager namespaceManager;
        public CsdlXPath(string filePath)
        {
            document = new XPathDocument(filePath);
            navigator = document.CreateNavigator();
            NavigateToSchemaNode();
            InitializeNamespaceManager();
        }

        private void NavigateToSchemaNode()
        {
            navigator.MoveToRoot();
            navigator.MoveToFirstChild(); // <edmx:Edmx ...>
            navigator.MoveToFirstChild(); // <edmx:DataServices>
            navigator.MoveToFirstChild(); // <Schema ...>
        }

        private void InitializeNamespaceManager()
        {
            namespaceManager = new XmlNamespaceManager(new NameTable());

            foreach (KeyValuePair<string, string> kvp in navigator.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml))
            {
                string sKey = kvp.Key;
                if (sKey == "")
                {
                    sKey = "default";
                }
                namespaceManager.AddNamespace(sKey, kvp.Value);
            }
        }

        public List<EntityType> GetEntityTypes()
        {
            string query = string.Format($"//default:EntityType");
            XPathNodeIterator results = navigator.Select(query, this.namespaceManager);
            List<EntityType> entityTypes = new List<EntityType>();

            //use the XPathNodeIterator to create EntityType objects.
            if (results.Count > 0)
            {
                while (results.MoveNext())
                {
                    string name = string.Empty;

                    if (results.Current.MoveToAttribute(XmlConstants.Name, ""))
                    {
                        name = results.Current.Value;
                    }

                    EntityType entityType = new EntityType()
                    {
                        Name = name
                    };

                    entityTypes.Add(entityType);

                    results.Current.MoveToParent();
                }
            }

            return entityTypes;
        }

        public EntityType GetEntityType(string entityTypeName)
        {
            string query = string.Format($"//default:EntityType[@Name='{entityTypeName}']");
            XPathNodeIterator results = navigator.Select(query, this.namespaceManager);

            results.MoveNext();
            string name = string.Empty;

            if (results.Current.MoveToAttribute(XmlConstants.Name, ""))
            {
                name = results.Current.Value;
            }

            EntityType entityType = new EntityType()
            {
                Name = name
            };

            return entityType;
        }

        public List<ComplexType> GetComplexTypes()
        {
            string query = string.Format($"//default:ComplexType");
            XPathNodeIterator results = navigator.Select(query, this.namespaceManager);
            List<ComplexType> complexTypes = new List<ComplexType>();

            if (results.Count > 0)
            {
                while (results.MoveNext())
                {
                    string name = string.Empty;

                    if (results.Current.MoveToAttribute(XmlConstants.Name, ""))
                    {
                        name = results.Current.Value;
                    }

                    ComplexType complexType = new ComplexType()
                    {
                        Name = name
                    };

                    complexTypes.Add(complexType);

                    results.Current.MoveToParent();
                }
            }

            return complexTypes;
        }

        public ComplexType GetComplexType(string complexTypeName)
        {
            string query = string.Format($"//default:ComplexType[@Name='{complexTypeName}']");
            XPathNodeIterator results = navigator.Select(query, this.namespaceManager);

            results.MoveNext();
            string name = string.Empty;

            if (results.Current.MoveToAttribute(XmlConstants.Name, ""))
            {
                name = results.Current.Value;
            }

            ComplexType complexType = new ComplexType()
            {
                Name = name
            };

            return complexType;
        }

        public List<EdmProperty> GetProperties(string entityTypeName)
        {
            string query = string.Format($"//default:EntityType[@Name='{entityTypeName}']/default:Property");
            XPathNodeIterator results = navigator.Select(query, this.namespaceManager);

            List<EdmProperty> properties = new List<EdmProperty>();

            //use the XPathNodeIterator to create EdmProperty objects.
            if (results.Count > 0)
            {
                while (results.MoveNext())
                {
                    string name = string.Empty;
                    string type = string.Empty;
                    bool nullable = true;

                    if (results.Current.MoveToAttribute(XmlConstants.Name, ""))
                    {
                        name = results.Current.Value;
                    }
                    if (results.Current.MoveToAttribute(XmlConstants.Type, ""))
                    {
                        type = results.Current.Value;
                    }
                    if (results.Current.MoveToAttribute(XmlConstants.Nullable, ""))
                    {
                        nullable = ToBoolean(results.Current.Value);
                        
                    }

                    EdmProperty property = new EdmProperty()
                    {
                        Name = name,
                        Type = type,
                        Nullable = nullable
                    };

                    properties.Add(property);

                    results.Current.MoveToParent();
                }
            }

            return properties;
        }

        private bool ToBoolean(string value)
        {
            switch (value.ToLower())
            {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return false;
            }
        }
    }
}

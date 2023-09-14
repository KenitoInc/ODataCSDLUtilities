using CsdlXPathLib.EdmTypes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CsdlXPathLib
{
    public class CsdlXPath
    {
        private XPathDocument xPathDocument;
        private XmlDocument xmlDocument;
        private XPathNavigator navigator;
        private XmlNamespaceManager namespaceManager;
        public CsdlXPath(string filePath, bool isReadOnly=true)
        {
            if (isReadOnly)
            {
                xPathDocument = new XPathDocument(filePath);
                navigator = xPathDocument.CreateNavigator();
            }
            else
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                navigator = xmlDocument.CreateNavigator();
            }

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
            XPathNodeIterator nodes = navigator.Select(query, this.namespaceManager);

            nodes.MoveNext();
            string name = string.Empty;
            string key = string.Empty;
            if (nodes.Current.MoveToAttribute(XmlConstants.Name, ""))
            {
                name = nodes.Current.Value;
            }

            EntityType entityType = new EntityType()
            {
                Name = name
            };
            
            nodes.Current.MoveToParent(); // <EntityType  ...>*/
            nodes.Current.MoveToFirstChild(); // <Key  ...>*/
            nodes.Current.MoveToFirstChild(); // <PropertyRef   ...>*/
            if (nodes.Current.MoveToAttribute(XmlConstants.Name, ""))
            {
                key = nodes.Current.Value;
            }
            entityType.Key = key;

            return entityType;
        }

        public void AddEntityType(EntityType entityType)
        {
            string query = string.Format($"//default:EntityType");
            XPathNodeIterator nodes = navigator.Select(query, this.namespaceManager);

            nodes.MoveNext();
            var navg = nodes.Current;
            var writer = navg.InsertBefore();

            writer.WriteStartElement(XmlConstants.EntityType);
            writer.WriteAttributeString(XmlConstants.Name, entityType.Name);
            if (!string.IsNullOrEmpty(entityType.BaseType)) 
            {
                writer.WriteAttributeString(XmlConstants.BaseType, entityType.BaseType);
            }
            writer.WriteEndElement();

            writer.Close();
        }

        public void AddProperty(EdmProperty property, string name)
        {
            string query = string.Format($"//default:EntityType[@Name='{name}']");
            XPathNodeIterator nodes = navigator.Select(query, this.namespaceManager);

            nodes.MoveNext();
            var navg = nodes.Current;
            var writer = navg.AppendChild();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString(XmlConstants.Name, property.Name);
            writer.WriteAttributeString(XmlConstants.Type, property.Type);

            if (!property.Nullable) 
            {
                writer.WriteAttributeString(XmlConstants.Nullable, property.Nullable.ToString());
            }
            if(!string.IsNullOrEmpty(property.MaxLength))
            {
                writer.WriteAttributeString(XmlConstants.MaxLength, property.MaxLength);
            }
            writer.WriteEndElement();

            writer.Close();
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

        public List<EnumType> GetEnumTypes()
        {
            string query = string.Format($"//default:EnumType");
            XPathNodeIterator results = navigator.Select(query, this.namespaceManager);
            List<EnumType> enumTypes = new List<EnumType>();

            if (results.Count > 0)
            {
                while (results.MoveNext())
                {
                    string name = string.Empty;

                    if (results.Current.MoveToAttribute(XmlConstants.Name, ""))
                    {
                        name = results.Current.Value;
                    }

                    EnumType enumType = new EnumType()
                    {
                        Name = name
                    };

                    enumTypes.Add(enumType);

                    results.Current.MoveToParent();
                }
            }

            return enumTypes;
        }

        public EnumType GetEnumType(string enumTypeName)
        {
            string query = string.Format($"//default:EnumType[@Name='{enumTypeName}']");
            XPathNodeIterator results = navigator.Select(query, this.namespaceManager);

            results.MoveNext();
            string name = string.Empty;

            if (results.Current.MoveToAttribute(XmlConstants.Name, ""))
            {
                name = results.Current.Value;
            }

            EnumType enumType = new EnumType()
            {
                Name = name
            };

            return enumType;
        }

        public List<EnumMember> GetEnumMembers(string enumTypeName)
        {
            string query = string.Format($"//default:EnumType[@Name='{enumTypeName}']");
            XPathNodeIterator nodes = navigator.Select(query, this.namespaceManager);

            List<EnumMember> results = new List<EnumMember>();
            if (nodes.Count > 0)
            {
                nodes.MoveNext();

                if (nodes.Current.MoveToAttribute(XmlConstants.Name, ""))
                {
                    string name = string.Empty;
                    string value = string.Empty;
                    nodes.Current.MoveToParent(); // <EnumType  ...>*/;
                    nodes.Current.MoveToFirstChild(); // <Member  ...>*/
                    do
                    {
                        if (nodes.Current.MoveToAttribute(XmlConstants.Name, ""))
                        {
                            name = nodes.Current.Value;
                            nodes.Current.MoveToParent();// <Member  ...>*/
                        }

                        if (nodes.Current.MoveToAttribute(XmlConstants.Value, ""))
                        {
                            value = nodes.Current.Value;
                        }

                        EnumMember member = new EnumMember()
                        {
                            Name = name,
                            Value = value
                        };
                        results.Add(member);
                        nodes.Current.MoveToParent();// <Member  ...>*/
                    } while (nodes.Current.MoveToNext());
                }
            }

            return results;
        }

        public List<EdmProperty> GetProperties(string entityTypeName, bool isEntity = true)
        {
            string query = string.Empty;

            if (isEntity)
            {
                query = string.Format($"//default:EntityType[@Name='{entityTypeName}']/default:Property");
            }
            else
            {
                query = string.Format($"//default:ComplexType[@Name='{entityTypeName}']/default:Property");
            }

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

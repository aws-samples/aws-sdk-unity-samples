using System;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;

namespace UnityEditor.FacebookEditor
{
    public class PListDict : Dictionary<string,object>
    {
        public PListDict()
        {
        }
        
        public PListDict(XElement dict)
        {
            Load(dict);
        }
        
        public void Load(XElement dict)
        {
            var dictElements = dict.Elements();
            ParseDictForLoad(this, dictElements);
        }
        
        private void ParseDictForLoad(PListDict dict, IEnumerable<XElement> elements)
        {
            for (int i = 0; i < elements.Count(); i += 2)
            {
                XElement key = elements.ElementAt(i);
                XElement val = elements.ElementAt(i + 1);
                dict[key.Value] = ParseValueForLoad(val);
            }
        }
        
        private List<object> ParseArrayForLoad(IEnumerable<XElement> elements)
        {
            var list = new List<object>();
            foreach (XElement e in elements)
            {
                object one = ParseValueForLoad(e);
                list.Add(one);
            }
            return list;
        }
        
        private object ParseValueForLoad(XElement val)
        {
            switch (val.Name.ToString())
            {
                case "string":
                    return val.Value;
                case "integer":
                    return int.Parse(val.Value);
                case "real":
                    return float.Parse(val.Value);
                case "true":
                    return true;
                case "false":
                    return false;
                case "dict":
                    PListDict plist = new PListDict();
                    ParseDictForLoad(plist, val.Elements());
                    return plist;
                case "array":
                    return ParseArrayForLoad(val.Elements());
                default:
                    throw new ArgumentException("Format unsupported, Parser update needed");
            }
        }
      
        public void Save(string fileName, XDeclaration declaration, XDocumentType docType)
        {
            XElement plistNode = new XElement("plist", ParseDictForSave(this));
            plistNode.SetAttributeValue("version", "1.0");
            XDocument file = new XDocument(declaration, docType);
            file.Add(plistNode);
            file.Save(fileName);
        }
          
        private XElement ParseDictForSave(PListDict dict)
        {
            XElement dictNode = new XElement("dict");
            foreach (string key in dict.Keys)
            {
                dictNode.Add(new XElement("key", key));
                dictNode.Add(ParseValueForSave(dict[key]));
            }
            return dictNode;
        }
      
        public XElement ParseValueForSave(Object node)
        {
            if (node.GetType() == typeof(string))
            {
                return new XElement("string", node);
            }
            else if (node.GetType() == typeof(Boolean))
            {
                return new XElement(node.ToString().ToLower());
            }
            else if (node.GetType() == typeof(int))
            {
                return new XElement("integer", node);
            }
            else if (node.GetType() == typeof(float))
            {
                return new XElement("real", node);
            }
            else if (node.GetType() == typeof(List<object>))
            {
                return ParseArrayForSave(node);
            }
            else if (node.GetType() == typeof(PListDict))
            {
                return ParseDictForSave((PListDict)node);
            }
            return null;
        }
      
        private XElement ParseArrayForSave(Object node)
        {
            XElement arrayNode = new XElement("array");
            var array = (List<object>)node;
            for (int i = 0; i < array.Count; i++)
            {
                arrayNode.Add(ParseValueForSave(array[i]));
            }
            return arrayNode;
        }
    }
}
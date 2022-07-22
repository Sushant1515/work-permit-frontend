using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkFlowEntry.Models
{
    [XmlRoot(ElementName = "Attribute")]
    public class Attribute
    {


        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "counrty")]
        public string Country { get; set; }



    }
    [XmlRoot(ElementName = "entry")]
    public class Entry
    {
        [XmlElement(ElementName = "attribute")]
        public List<Attribute> Attribute { get; set; }
    }
    }

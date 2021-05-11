using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UblSharp;

namespace UBLGenratorMy
{
   // [System.Xml.Serialization.XmlRoot("StandardBusinessDocument", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
    public class StandardBusinessDocument
    {
        [System.Xml.Serialization.XmlElement("StandardBusinessDocumentHeader", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public StandardBusinessDocumentHeader StandardBusinessDocumentHeader { get; set; }
       
        public InvoiceType Invoice { get; set; }
    }

    public class StandardBusinessDocumentHeader
    {
        [System.Xml.Serialization.XmlElement("HeaderVersion", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public string HeaderVersion { get; set; } = "1.0";

        [System.Xml.Serialization.XmlElementAttribute("Sender", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public Sender[] Sender { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("Receiver", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public Receiver[] Receiver { get; set; }

        [System.Xml.Serialization.XmlElement("DocumentIdentification", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public DocumentIdentification DocumentIdentification { get; set; }
    }

    public class Sender
    {
        [System.Xml.Serialization.XmlElement("Identifier", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public Identifier Identifier { get; set; }

    }

    public class Receiver
    {
        [System.Xml.Serialization.XmlElement("Identifier", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public Identifier Identifier { get; set; }

    }

    public class Identifier
    {
        [System.Xml.Serialization.XmlAttribute("Authority", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public string Authority { get; set; } = "countrycode:organizationid";

        [System.Xml.Serialization.XmlText()]
        public string Value { get; set; } 
    }

    public class DocumentIdentification
    {
        [System.Xml.Serialization.XmlElement("Standard", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public string Standard { get; set; }

        [System.Xml.Serialization.XmlElement("TypeVersion", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public string TypeVersion { get; set; }

        [System.Xml.Serialization.XmlElement("InstanceIdentifier", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public string InstanceIdentifier { get; set; }
       
        [System.Xml.Serialization.XmlElement("Type", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public string Type { get; set; }
        
        [System.Xml.Serialization.XmlElement("MultipleType", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public bool MultipleType { get; set; } = true;
        
        [System.Xml.Serialization.XmlElement("CreationDateAndTime", Namespace = "urn:sfti:documents:StandardBusinessDocumentHeader")]
        public String CreationDateAndTime { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using UblSharp;
using UblSharp.Tests.Samples;
using UblSharp.Validation;

namespace UBLGenratorMy
{
    public class Invoice
    {
        public string ID { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            var doc = UBLCreditNote20Example.Create();

            //SerializeDeserialize<CreditNoteType> serializeEmployee;
            //serializeEmployee = new SerializeDeserialize<CreditNoteType>();
            //string serializedEmployee = serializeEmployee.SerializeData(doc);

            //var docInvoice = UBLInvoice21Example.Create();
            //var invoice = new SerializeDeserialize<InvoiceType>();
            //string invoiceXml = invoice.SerializeData(docInvoice);

            //var smallInvoice = SmallInvoiceTest.Create();
            //var invoiceSmall = new SerializeDeserialize<InvoiceType>();
            //string invoiceSmallXml = invoice.SerializeData(smallInvoice);

            Foo();

            

            /*
            var OKQ8Invoice = OKQ8invoice.Create();
            var invoiceSmall = new SerializeDeserialize<InvoiceType>();
            string invoiceOKQ8XML = invoiceSmall.SerializeData(OKQ8Invoice);

            string fileName = OKQ8Invoice.ID;
            //var path = @"D:\temp\ROM\xml\test.xml";
            //File.WriteAllText(path, invoiceOKQ8XML);

            using (StreamWriter outputFile = File.CreateText(@"D:\temp\ROM\MultiJson\xml\"+fileName + ".xml"))
            {
                outputFile.Write(invoiceOKQ8XML);
            }
            */



        }

        public static void Foo() {
            //var path = @"D:\ROM\CLARIFICATIONS\Questionnaires\Mapping\CASE_1\TARGET_12007011905.xml"; // new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            var path = @"D:\temp\ROM\xml\test.xml"; // new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;


            //XmlSchemaSet schema = new XmlSchemaSet();
            //schema.Add("urn:oasis:names:specification:ubl:schema:xsd:Invoice-2", @"D:\ROMProject\MagicInvoice\solMagicInvoic\InvoiceGenerator\schema\UBL_Invoice_2_1.xsd");

            XmlReader rd = XmlReader.Create(path);
            XDocument doc = XDocument.Load(rd);

            UblDocumentValidator val = new UblDocumentValidator();
            var errs = val.Validate(doc, suppressWarnings: false);

           

            //doc.Validate(schema, ValidationEventHandler);
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type = XmlSeverityType.Warning;
            if (Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                if (type == XmlSeverityType.Error) 
                    throw new Exception(e.Message);
            }
        }
    }
}

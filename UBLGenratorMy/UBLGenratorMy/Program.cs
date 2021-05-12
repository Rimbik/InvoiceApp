using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UblSharp;
using UblSharp.Tests.Samples;

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

            //var doc = UBLCreditNote20Example.Create();

            ////SerializeDeserialize<CreditNoteType> serializeEmployee;
            ////serializeEmployee = new SerializeDeserialize<CreditNoteType>();
            ////string serializedEmployee = serializeEmployee.SerializeData(doc);

            //var docInvoice = UBLInvoice21Example.Create();
            //var invoice = new SerializeDeserialize<InvoiceType>();
            //string invoiceXml = invoice.SerializeData(docInvoice);

            
            var smallInvoice = SmallInvoiceTest.Create();
            var invoiceSmall = new SerializeDeserialize<InvoiceType>();

            var invoice = new SerializeDeserialize<InvoiceType>();
            string invoiceSmallXml = invoice.SerializeData(smallInvoice);

            
            var path = @"D:\temp\ROM\xml\test.xml";
            File.WriteAllText(path, invoiceSmallXml);

            //
            var SBDH = new StandardBusinessDocument<InvoiceType>();
            SBDH.Invoice = smallInvoice;
            SBDH.StandardBusinessDocumentHeader = new StandardBusinessDocumentHeader();
            SBDH.StandardBusinessDocumentHeader.Sender = new Sender[2];
            SBDH.StandardBusinessDocumentHeader.Sender[0] = new Sender()
               {
                   Identifier = new Identifier() { Authority = "countrycode:organizationid", Value = "003729605388" }
               };
            SBDH.StandardBusinessDocumentHeader.Sender[1] = new Sender()
            {
                Identifier = new Identifier() { Authority = "operatorid", Value = "6430037520006" }
            };

            SBDH.StandardBusinessDocumentHeader.Receiver = new Receiver[2];
            SBDH.StandardBusinessDocumentHeader.Receiver[0] = new Receiver()
            {
                Identifier = new Identifier() { Authority = "countrycode:organizationid", Value = "FI9022929071421725" }
            };
            SBDH.StandardBusinessDocumentHeader.Receiver[1] = new Receiver()
            {
                Identifier = new Identifier() { Authority = "operatorid", Value = "NDEAFIHH" }
            };

            SBDH.StandardBusinessDocumentHeader.DocumentIdentification = new DocumentIdentification()
            {
                Standard = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2",
                TypeVersion = "1.0",
                InstanceIdentifier = "1",
                Type = "BasicInvoice",
                MultipleType = true,
                CreationDateAndTime = "2021-05-04T01:38:59.829+02:00"
            };

            var invoiceSBDHSmall = new SerializeDeserialize<StandardBusinessDocument<InvoiceType>>();
            string invoiceSBDHSmallXml = invoiceSBDHSmall.SerializeData(SBDH);

            var path1 = @"D:\temp\ROM\xml\testSBDH.xml";
            File.WriteAllText(path1, invoiceSBDHSmallXml);

            //Try new
            dynamic myfileObject = new SBDHClass();
            myfileObject.Invoice123 = smallInvoice;

            var invoiceSBDHSmall1 = new SerializeDeserialize<SBDHClass>();
            string invoiceSBDHSmallXml1 = invoiceSBDHSmall1.SerializeData(myfileObject);
            var path11 = @"D:\temp\ROM\xml\testSBDH1.xml";
            File.WriteAllText(path11, invoiceSBDHSmallXml1);

        }
    }
}

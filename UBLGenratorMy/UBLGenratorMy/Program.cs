using System;
using System.Collections.Generic;
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

            var doc = UBLCreditNote20Example.Create();

            SerializeDeserialize<CreditNoteType> serializeEmployee;
            serializeEmployee = new SerializeDeserialize<CreditNoteType>();
            string serializedEmployee = serializeEmployee.SerializeData(doc);

            var docInvoice = UBLInvoice21Example.Create();
            var invoice = new SerializeDeserialize<InvoiceType>();
            string invoiceXml = invoice.SerializeData(docInvoice);

            var smallInvoice = SmallInvoiceTest.Create();
            var invoiceSmall = new SerializeDeserialize<InvoiceType>();
            string invoiceSmallXml = invoice.SerializeData(smallInvoice);


        }
    }
}

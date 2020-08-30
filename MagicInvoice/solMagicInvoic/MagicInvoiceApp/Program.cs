using OKQ8.InvoiceGenerator;
using System;
using System.IO;
using System.Text;

namespace MagicInvoiceApp
{
    class Program
    {
        //const int megabyte = 144*1; // 400 lines at once to read
        const int megabyte = 7000000 * 10; // 400 lines at once to read

        static void Main(string[] args)
        {
            string fileWithFullPath = @"F:\MustDel\ReadLarge.txt";

            var invoiceGenerator = new OKQ8.InvoiceGenerator.InvoiceGenerator(fileWithFullPath);
            invoiceGenerator.Process();

        }

    }

}

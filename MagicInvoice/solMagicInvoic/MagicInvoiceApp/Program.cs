using OKQ8.InvoiceGenerator;
using System;
using System.IO;
using System.Text;

namespace MagicInvoiceApp
{
    class Program
    {
        //const int megabyte = 144*1; // 400 lines at once to read
        const int megabyte = 144 * 10; // 400 lines at once to read

        static void Main(string[] args)
        {
            string fileWithFullPath = @"F:\MustDel\MLANG.AUG2020_Test.txt";

            //var invoiceGenerator = new OKQ8.InvoiceGenerator.InvoiceGenerator(fileWithFullPath);
            //invoiceGenerator.Process();


            var invGenerator = new OKQ8.InvoiceGenerator.LargeFileProcessor();
              invGenerator.ReadAndProcessLargeFile(fileWithFullPath);
             //  invGenerator.ReadWithStream(fileWithFullPath);

        }

    }

}

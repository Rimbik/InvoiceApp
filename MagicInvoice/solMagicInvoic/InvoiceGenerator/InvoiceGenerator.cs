using System;
using System.IO;
using System.Text;

namespace OKQ8.InvoiceGenerator
{
    public class InvoiceGenerator
    {
        string FILENAME;
        const int megabyte = 7000000 * 10; // 400 lines at once to read

        public InvoiceGenerator(string fileWithFullPath)
        {
            FILENAME = fileWithFullPath;
        }
        public void Process()
        {
        }
       

    }
}

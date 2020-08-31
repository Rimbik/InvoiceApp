using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OKQ8.InvoiceGenerator
{
  
    public class LargeFileProcessor
    {
        string FILENAME;
        const int megabyte = 144 * 10; // 400 lines at once to read

        public LargeFileProcessor()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        }

        public void ReadAndProcessLargeFile(string theFilename, long whereToStartReading = 0)
        {
            FileInfo info = new FileInfo(theFilename);
            long fileLength = info.Length;
            long timesToRead = (fileLength / megabyte);
            long ctr = 0;
            long timesRead = 0;


            var fileStram = new FileStream(theFilename, FileMode.Open, FileAccess.Read);
            

            using (fileStram)
            {
                byte[] buffer = new byte[megabyte];

                fileStram.Seek(whereToStartReading, SeekOrigin.Begin);

                int bytesRead = 0;
                
                while ((bytesRead = fileStram.Read(buffer, 0, megabyte)) > 0)
                {
                    ProcessChunk(buffer, bytesRead);
                    buffer = new byte[megabyte];
                }

            }
        }

        private void ProcessChunk(byte[] buffer, int bytesRead)
        {
            // Do the processing here with Swidish Lines
            string uniCodeString = Encoding.GetEncoding(1252).GetString(buffer, 0, buffer.Length);

            string[] lines = uniCodeString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            
            Process(lines);

            Console.WriteLine(uniCodeString);
        }

        private void Process(string[] lines)
        {
            OKQ8.InvoiceGenerator.Processor.Process(lines);
        }

        public void ReadWithStream(string theFilename)
        {
            var buffer = new char[megabyte];

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            StreamReader reader = new StreamReader(theFilename, System.Text.Encoding.GetEncoding(1252), true);
            reader.Read(buffer, 0, buffer.Length);

            var data = new string(buffer);
        }
    }
}

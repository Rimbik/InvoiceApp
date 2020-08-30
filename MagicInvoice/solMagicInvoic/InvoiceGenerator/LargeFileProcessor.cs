using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InvoiceGenerator
{
  
    public class LargeFileProcessor
    {
        string FILENAME;
        const int megabyte = 7000000 * 10; // 400 lines at once to read

        public void ProcessLargeTXT()
        {

        }

        private void ReadAndProcessLargeFile(string theFilename, long whereToStartReading = 0)
        {
            FileInfo info = new FileInfo(theFilename);
            long fileLength = info.Length;
            long timesToRead = (fileLength / megabyte);
            long ctr = 0;
            long timesRead = 0;

            FileStream fileStram = new FileStream(theFilename, FileMode.Open, FileAccess.Read);
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
            // Do the processing here
            string utfString = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            string[] lines = utfString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Console.WriteLine("Processed 100 lines...");
        }
    }
}

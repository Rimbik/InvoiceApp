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

        public void ProcessLargeTXT()
        {

        }

        public void ReadAndProcessLargeFile(string theFilename, long whereToStartReading = 0)
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
            
            //string[] lines = File.ReadAllLines(textFile, System.Text.Encoding.GetEncoding(1252)); //read swidish codes

            Process(lines);

            Console.WriteLine(utfString);
        }

        private void Process(string[] lines)
        {
            throw new NotImplementedException();
        }
    }
}

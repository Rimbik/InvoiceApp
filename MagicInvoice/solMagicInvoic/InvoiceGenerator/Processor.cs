using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using OKQ8.InvoiceGenerator.Model;


namespace OKQ8.InvoiceGenerator
{
    public class Processor
    {
        static string INVOICE_NUMBER = "";
        static string PREV_INVOICE_NUMBER = INVOICE_NUMBER;

        static string CARDNO = "";
        static bool ISCARDFOUND = false;
        static int INVOICE_CTR = 0;
        static bool ISLINEITEM_HEADER_READ = false;
        static bool ISLINEITEM_STARTED = false;
        static bool ISLINEITEM_FINISHED = false;

        static Invoices HEADER_INVOICE = new Invoices() {
            Cards = new List<Card>()
        };

        // static InvoiceRow row = null;
        static Card CARD = new Card() { 
            Items = new LineItems() { 
                InvoiceRows = new List<InvoiceRow>() 
            }
        };

        public static void Process(string[] lines)
        {

            bool InvoiceBlockBreakerFound = false;

            InVoiceCollection invColl = new InVoiceCollection();
            invColl.Invoices = new List<Invoices>();
            var invoiceColl = new List<InvoiceRow>();


            //  Console.WriteLine("Start Processing ....");

            int standardRowIDLength = "90194560014".Length;

            string str = "-1";

            int invoiceCtr = 0;
            bool hasStarted = false;
            bool getReadyForLineItems = false;

            //  Invoice invoice = new Invoice() { TypeOfInvoice = InvoiceType.Unknown };
            long lineNumber = 0;
            string InvoiceNumber = "";
            bool IsBreakingFromCardGroup = false;

            //foreach (string line in lines)
            for (long lineNo = 0; lineNo < lines.Length; lineNo++)
            {

                var lineReading = lines[lineNo];
                if (lineReading.Length < 140)
                    continue;

                if (String.IsNullOrEmpty(lineReading))
                    continue;

                byte[] rowBytes = Encoding.ASCII.GetBytes(lineReading);

                var rowIdStr = Encoding.ASCII.GetString(rowBytes, 0, standardRowIDLength);

                var isInvoiceBloackStarted = Encoding.ASCII.GetString(rowBytes, 21, 3).Trim();
                var isBOI = isInvoiceBloackStarted == "\u001f1";

                //Identify InvoiceNumber
                var getInvoiceStr = Encoding.ASCII.GetString(rowBytes, 19, 3).Trim();

                //Is Line Holding invoiceNumber
                var isInvoiceLine = getInvoiceStr == "\u001f ^";
                var isDuplicateInvoiceLine = getInvoiceStr == "\a ^" || getInvoiceStr == "\u001a ^";

                ////Get InvoiceAmount
                //var invoiceTotalAmountRowIdentifier = Encoding.ASCII.GetString(rowBytes, 32, 17).Trim();
                //var isInvoiceAmountStr = invoiceTotalAmountRowIdentifier == "Totalt alla kort";

                //Is Card No
                var cardNoRowIdentifierStr = Encoding.ASCII.GetString(rowBytes, 19, 4).Trim();
                var isCardNo = cardNoRowIdentifierStr == "\u001f\u0001\a3" || GetField(lineReading, 30, 4) == "KORT";

                if (isInvoiceLine || isDuplicateInvoiceLine)
                {
                    INVOICE_NUMBER = Encoding.ASCII.GetString(rowBytes, 109, 12);  //109 - 109+11
                    ++INVOICE_CTR;

                    if(INVOICE_NUMBER != PREV_INVOICE_NUMBER)
                    {
                        PREV_INVOICE_NUMBER = INVOICE_NUMBER;
                        SetNewInvoiceArea();
                    }
                    else if(INVOICE_NUMBER == PREV_INVOICE_NUMBER)
                    {
                        //Invoice has Multiple Cards
                       
                       SaveFile();
                        SetNewInvoiceArea();
                    }


                    continue;
                }

                if(isCardNo)
                {
                    CARD = new Card()
                    {
                        Items = new LineItems()
                        {
                            InvoiceRows = new List<InvoiceRow>()
                        }
                    };

                    CARD.CardNo = Encoding.ASCII.GetString(rowBytes, 34, 5).Trim() ?? "-";

                    CARD.CardText = "KORT " + Encoding.ASCII.GetString(rowBytes, 34, 5).Trim();
                    CARD.InvoiceNumber = INVOICE_NUMBER;
                    

                    ISCARDFOUND = true;
                    CARDNO = CARD.CardNo;

                    ISCARDFOUND = true;
                    InvoiceBlockBreakerFound = false;

                    if (CARD.CardNo == "0027")
                    {
                        var x1 = "111";
                    }

                    
                    HEADER_INVOICE.Cards.Add(CARD);

                    continue; // Go to Next Line
                }

                if(ISCARDFOUND && ISLINEITEM_HEADER_READ == false)
                {
                    bool isLineItemHeader = GetField(lineReading, 33, 7) == "Köpnota";
                    if(isLineItemHeader)
                    {
                        //Get Ready to read LineItems
                        ISLINEITEM_HEADER_READ = true;
                    }

                    continue;
                }

                if (ISLINEITEM_STARTED)
                {
                    var _row = ReadLineItemRow(lineReading);
                    if(!String.IsNullOrEmpty(_row.Kopnota))
                    if (_row.Kopnota == "-------" && _row.Dag == "----")
                    {
                        // LineItems Finished
                        ISLINEITEM_FINISHED = true;
                        ISLINEITEM_STARTED = false;
                        ISCARDFOUND = false;

                        //TIME TO CREATE INVOICE FILE (AS CARD and Its LineItems are created)
                    }
                }

                if (ISCARDFOUND && ISLINEITEM_HEADER_READ)
                {
                    //Read LineItems
                    ISLINEITEM_STARTED = true;
                    var _row = ReadLineItemRow(lineReading);
                    CARD.Items.InvoiceRows.Add(_row);
                    
                    continue;
                }

                if (ISLINEITEM_FINISHED)
                {
                    rowBytes = Encoding.ASCII.GetBytes(lineReading);
                    var invoiceTotalAmountRowIdentifier = Encoding.ASCII.GetString(rowBytes, 32, 17).Trim();
                    var isInvoiceAmountStr = invoiceTotalAmountRowIdentifier == "Totalt alla kort";

                    if (isInvoiceAmountStr)
                    {
                        GetInvoiceAmount(lineReading);

                        ISLINEITEM_FINISHED = false;
                        //Create File in HDD
                        SaveFile();
                        ResetCard();
                    }
                }

                //if(!ISLINEITEM_FINISHED && isDuplicateInvoiceLine)
                //{
                //    //Save Prev File
                //    SaveFile();
                //}

                //Id Card Found then Get All LineItems

                /* SUSPENDED FOR NEW LOOP
                if (isCardNo)
                {
                    //CARD Found
                    //

                    if (CardFound && InvoiceBlockBreakerFound)
                    {
                        //InvoiceType_1
                    }
                    else if (CardFound == true && InvoiceBlockBreakerFound == false)
                    {
                        //InvoiceType_2
                    }
                    else if (CardFound == false && InvoiceBlockBreakerFound == false)
                    {
                        //InvoiceType_2 ???

                    }


                    Card card = new Card() { };
                    card.CardNo = Encoding.ASCII.GetString(rowBytes, 34, 5).Trim();
                    card.CardText = "KORT " + Encoding.ASCII.GetString(rowBytes, 34, 5).Trim();

                    CardFound = true;
                    InvoiceBlockBreakerFound = false;

                    if (card.CardNo == "0023")
                    {
                        var sssd = "ss";
                    }

                    card.Items = new LineItems();
                    card.Items.InvoiceRows = new List<InvoiceRow>();

                    var _invoiceHeader = new
                    {
                        InvoiceNumber = InvoiceNumber
                      ,
                        RowIdentifier = rowIdStr
                      ,
                        TypeOfInvoice = InvoiceType.Unknown
                      ,
                        IsInvoiceNumberRepeated = isDuplicateInvoiceLine
                    };

                    ////Get InvoiceAmount
                    //var invoiceTotalAmountRowIdentifier = Encoding.ASCII.GetString(rowBytes, 32, 17).Trim();
                    //var isInvoiceAmountStr = invoiceTotalAmountRowIdentifier == "Totalt alla kort";

                    //Next line will be Columns
                    lineNo = lineNo + 1; // No need to read as ColumnHeader Text

                    //Then Next lines will be Invoice-LineItems
                    lineNo = lineNo + 1; // position line no to Beg Of LineItems Row
                    lineReading = lines[lineNo];
                    getReadyForLineItems = true;

                    //Set LineItem Values
                    //Start Get
                    var invoiceRows = new List<InvoiceRow>();
                    int rowIndex = 0;
                    while (true) // Keep reading Invoice - LineItems
                    {
                        //Get InvoiceAmount
                        rowBytes = Encoding.ASCII.GetBytes(lineReading);

                        var invoiceTotalAmountRowIdentifier = Encoding.ASCII.GetString(rowBytes, 32, 17).Trim();
                        var isInvoiceAmountStr = invoiceTotalAmountRowIdentifier == "Totalt alla kort";


                        //row create
                        InvoiceRow row = new InvoiceRow();

                        //rowBytes = Encoding.ASCII.GetBytes(lineReading);

                        row.RowId = ++rowIndex;

                        //row.Kopnota = Encoding.ASCII.GetString(rowBytes, 32, 8).Trim(); ; //32,8
                        row.Kopnota = GetField(lineReading, 32, 8).Trim();
                        row.Dag = GetField(lineReading, 41, 4).Trim();
                        row.Tid = GetField(lineReading, 46, 5).Trim();  //46,5
                        row.ForsStalle = GetField(lineReading, 52, 17).Trim();
                        row.Kundref = GetField(lineReading, 69, 9).Trim(); //69,9
                        row.Artikel = GetField(lineReading, 80, 10).Trim(); //80,10
                        row.Kvant = GetField(lineReading, 93, 5).Trim(); //93,5
                        row.Apris = GetField(lineReading, 101, 6).Trim(); //101,6
                        row.Brutto = GetField(lineReading, 109, 7).Trim(); //109,7
                        row.Rabatt = GetField(lineReading, 117, 7).Trim(); //117,7
                        row.Moms = GetField(lineReading, 127, 6).Trim(); //127,6
                        row.Netto = GetField(lineReading, 133, 9).Trim(); //135,9


                        //end-row Create

                        if (row.Kopnota == "-------" && row.Dag == "----")
                        {
                            //this is lineitem break  amd ends
                            row.Kopnota =
                            row.Dag =
                            row.Tid =
                            row.ForsStalle =
                            row.Kundref =
                            row.Artikel = "";

                            for (int x = 0; x < 9; x++)
                            {
                                lineNo += 1;

                                if (lineNo == 310)
                                {
                                    var tkBrk = true;
                                }

                                //Is Card No
                                rowBytes = Encoding.ASCII.GetBytes(lineReading);
                                lineReading = lines[lineNo];
                                cardNoRowIdentifierStr = Encoding.ASCII.GetString(rowBytes, 19, 4).Trim();
                                isCardNo = cardNoRowIdentifierStr == "\u001f\u0001\a3" || GetField(lineReading, 30, 4) == "KORT";

                                if (isCardNo)
                                {
                                    //Looks like card is in Group
                                    if (CardFound && InvoiceBlockBreakerFound == false)
                                    {
                                        lineNo -= 1;
                                        IsBreakingFromCardGroup = true;
                                        break;
                                    }
                                }

                            }

                            //here
                            if (IsBreakingFromCardGroup)
                                break;
                        }
                        else if (isInvoiceAmountStr)
                        {
                            string invoiceAmount = "0";
                            invoiceAmount = Encoding.ASCII.GetString(rowBytes, 132, 10).Trim();
                            var _invoiceAmount = decimal.Parse(invoiceAmount.Replace(",", ".").Replace(" ", ""));
                            var _invoiceAmountRounded = Math.Ceiling(_invoiceAmount);

                            card.Items.InvoiceAmount = _invoiceAmount;
                            card.Items.InvoiceAmountRounded = _invoiceAmountRounded;
                            card.Items.RowIdentifier = _invoiceHeader.RowIdentifier;
                            card.Items.InvoiceNumber = _invoiceHeader.InvoiceNumber;
                            card.InvoiceNumber = _invoiceHeader.InvoiceNumber;

                            InvoiceBlockBreakerFound = true;


                            break;
                        }
                        //else
                        //{

                        card.Items.InvoiceRows.Add(row);

                        lineNo = lineNo + 1; // move next
                        if (lineNo == lines.Length - 1)
                            break;

                        lineReading = lines[lineNo];
                        //}//

                        if (lineNo == 317)
                        {
                            var sbraek = "aaa";
                        }

                        //TODO:(smn) Need Break here

                        ////Identify InvoiceNumber
                        //rowBytes = Encoding.ASCII.GetBytes(lineReading);
                        //getInvoiceStr = Encoding.ASCII.GetString(rowBytes, 19, 3).Trim();
                        //isInvoiceLine = getInvoiceStr == "\u001f ^";
                        //if (isInvoiceLine)
                        //    break;

                    }
                    //
                    //string invoiceAmount = "0";
                    //if (isInvoiceAmountStr)
                    //{
                    //    invoiceAmount = Encoding.ASCII.GetString(rowBytes, 132, 10).Trim();
                    //    card.Invoice.InvoiceAmount = decimal.Parse(invoiceAmount.Replace(",", ".").Replace(" ", ""));
                    //    card.Invoice.InvoiceAmountRounded = Math.Ceiling(card.Invoice.InvoiceAmount);
                    //}

                    //here
                    Invoices headerInv = new Invoices();
                    headerInv.Cards = new List<Card>();

                    if (CardFound && InvoiceBlockBreakerFound)
                        card.IsCardInGroup = false;
                    else if (CardFound && InvoiceBlockBreakerFound == false)
                        card.IsCardInGroup = true;
                    else
                    {
                        //
                    }

                    headerInv.Cards.Add(card);
                    //invColl.Invoices.Add(headerInv); //suspended for memory problem (mem overflow exception while processing 800 MB file)

                    //here
                    var jsonContent = JsonConvert.SerializeObject(headerInv, Formatting.Indented);
                    string fileName = invoiceCtr + "_" + card.CardNo.Trim() + "_" + card.InvoiceNumber + "_Invoice.json";
                    System.IO.File.WriteAllText(@"D:\temp\ROM\MultiJson\" + fileName.Trim(), jsonContent);

                    Console.WriteLine("Invoice Created and Saved :" + fileName);
                }
                */

                //"3"
                //if (lineReading.Contains("KORT"))
                //{
                //    var xs = "wait";
                //}
                //if (isDuplicateInvoiceLine)
                //{
                //    int abc = 123;
                //}

                //if (line.Contains("2(3)"))
                //{
                //    int ddd = 123;
                //}






                //if (isInvoiceBloackStarted == "3")
                //if (isBOI)
                //    invoice.TypeOfInvoice = InvoiceType.Type_1;

                if (isInvoiceLine || isDuplicateInvoiceLine)
                {
                    isBOI = true;
                    //invoice = new Invoice() { 
                    //    RowIdentifier = rowIdStr, TypeOfInvoice = InvoiceType.Unknown, 
                    //    IsInvoiceNumberRepeated = isDuplicateInvoiceLine 
                    //};

                    hasStarted = true;
                    isInvoiceLine = false;
                    InvoiceNumber = Encoding.ASCII.GetString(rowBytes, 109, 12);  //109 - 109+11

                    //Create Invoice CLR Row
                    invoiceCtr++;
                }

            }

            //  var jsonStr =  JsonConvert.SerializeObject(invColl, Formatting.Indented);
            //  System.IO.File.WriteAllText(@"D:\temp\ROM\InvoiceCollectionLarge.json", jsonStr);
        }

        private static void SetNewInvoiceArea()
        {
            HEADER_INVOICE = new Invoices()
            {
                Cards = new List<Card>()
            };
            
            CARD = new Card()
            {
                Items = new LineItems()
                {
                    InvoiceRows = new List<InvoiceRow>()
                }
            };
        }

        private static void SaveFile()
        {
            //HEADER_INVOICE.Cards = new List<Card>();
            //HEADER_INVOICE.Cards.Add(CARD);
            
            var jsonContent = JsonConvert.SerializeObject(HEADER_INVOICE, Formatting.Indented);

            if (CARD.CardNo == null)
                CARD.CardNo = "-";

            string fileName = INVOICE_CTR + "_" + CARD.CardNo.Trim() + "_" + CARD.InvoiceNumber + "_Invoice.json";
            System.IO.File.WriteAllText(@"D:\temp\ROM\MultiJson\" + fileName.Trim(), jsonContent);
        }

        private static void ResetCard()
        {
            CARD = new Card()
            {
                Items = new LineItems()
                {
                    InvoiceRows = new List<InvoiceRow>()
                }
            };
        }

        private static void GetInvoiceAmount(string lineReading)
        {
            string invoiceAmount = "0";
            
            var rowBytes = Encoding.ASCII.GetBytes(lineReading);
            invoiceAmount = Encoding.ASCII.GetString(rowBytes, 132, 10).Trim();
            var _invoiceAmount = decimal.Parse(invoiceAmount.Replace(",", ".").Replace(" ", ""));
            var _invoiceAmountRounded = Math.Ceiling(_invoiceAmount);

            CARD.Items.InvoiceAmount = _invoiceAmount;
            CARD.Items.InvoiceAmountRounded = _invoiceAmountRounded;
           // card.Items.RowIdentifier = _invoiceHeader.RowIdentifier;
            CARD.Items.InvoiceNumber = INVOICE_NUMBER;
            CARD.InvoiceNumber = INVOICE_NUMBER;

         //   InvoiceBlockBreakerFound = true;
        }

        private static InvoiceRow ReadLineItemRow(string lineReading)
        {
            //row create
            var row = new InvoiceRow();

           // row.RowId = ++rowIndex;

            //row.Kopnota = Encoding.ASCII.GetString(rowBytes, 32, 8).Trim(); ; //32,8
            row.Kopnota = GetField(lineReading, 32, 8).Trim();
            row.Dag = GetField(lineReading, 41, 4).Trim();
            row.Tid = GetField(lineReading, 46, 5).Trim();  //46,5
            row.ForsStalle = GetField(lineReading, 52, 17).Trim();
            row.Kundref = GetField(lineReading, 69, 9).Trim(); //69,9
            row.Artikel = GetField(lineReading, 80, 10).Trim(); //80,10
            row.Kvant = GetField(lineReading, 93, 5).Trim(); //93,5
            row.Apris = GetField(lineReading, 101, 6).Trim(); //101,6
            row.Brutto = GetField(lineReading, 109, 7).Trim(); //109,7
            row.Rabatt = GetField(lineReading, 117, 7).Trim(); //117,7
            row.Moms = GetField(lineReading, 127, 6).Trim(); //127,6
            row.Netto = GetField(lineReading, 133, 9).Trim(); //135,9


            //end-row Create

            return row;
        }

        private static string GetField(string lineReading, int startIndex, int endIndex)
        {
            var lineLength = lineReading.Length;

            if (startIndex > lineLength || endIndex > lineLength || startIndex + endIndex > lineLength || startIndex < 0 || endIndex < 0)
            {
                return "-err-";
            }

            try
            {
                return lineReading.Substring(startIndex, endIndex);
            }
            catch (Exception err)
            {
                var errDetails = err;
            }

            return "-err-";
        }
    }


}

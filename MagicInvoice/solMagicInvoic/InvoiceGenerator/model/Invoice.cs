using System.Collections.Generic;

namespace OKQ8.InvoiceGenerator.Model
{
    public class InvoiceRow
    {
        //Köpnota  Dag  Tid  Förs ställe      Kundref    Artikel      Kvant   Apris   Brutto  Rabatt     Moms     Netto
        public int RowId { get; set; }
        public string Kopnota { get; set; }
        public string Dag { get; set; }
        public string Tid { get; set; }
        public string ForsStalle { get; set; }
        //public string stalle { get; set; }
        public string Kundref { get; set; }
        public string Artikel { get; set; }
        public string Kvant { get; set; }
        public string Apris { get; set; }
        public string Brutto { get; set; }
        public string Rabatt { get; set; }
        public string Moms { get; set; }
        public string Netto { get; set; }

    }

    public class InvoiceColumns
    {
        //Köpnota  Dag  Tid  Förs ställe      Kundref    Artikel      Kvant   Apris   Brutto  Rabatt     Moms     Netto

        public string RowId = "RowId";
        public string Kopnota = "Köpnota";
        public string Dag = "Dag";
        public string Tid = "Tid";
        public string Fors = "Förs";
        public string stalle = "ställe";
        public string Kundref = "Kundref";
        public string Artikel = "Artikel";
        public string Kvant = "Kvant";
        public string Apris = "Apris";
        public string Brutto = "Brutto";
        public string Rabatt = "Rabatt";
        public string Moms = "Moms";
        public string Netto = "Netto";

    }

    public class InVoiceCollection
    {
        public InvoiceColumns invoiceColumns;

        public InVoiceCollection()
        {
            invoiceColumns = new InvoiceColumns();
        }

        public List<Invoices> Invoices { get; set; }
    }

    public class Invoices
    {
        public List<Card> Cards { get; set; }
    }

    public class Card
    {
        public string InvoiceNumber { get; set; }
        public string CardNo { get; set; }
        public string CardText { get; set; }
        public bool IsCardInGroup { get; set; }
        public LineItems Items { get; set; }
    }

    public class LineItems
    {
        public string InvoiceNumber { get; set; }
        public InvoiceType TypeOfInvoice { get; set; }
        public List<InvoiceRow> InvoiceRows { get; set; }
        public string RowIdentifier { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal InvoiceAmountRounded { get; set; }
        public bool IsInvoiceNumberRepeated { get; set; }
    }

    public enum InvoiceType
    {
        Unknown,
        Type_1,
        Type_2,
        Type_3,
        Type_4
    }
}
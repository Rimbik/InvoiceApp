using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBLGenratorMy
{

    public class Rootobject
    {
        public Card[] Cards { get; set; }
    }

    public class Card
    {
        public string InvoiceNumber { get; set; }
        public string CardNo { get; set; }
        public string CardText { get; set; }
        public bool IsCardInGroup { get; set; }
        public Items Items { get; set; }
    }

    public class Items
    {
        public string InvoiceNumber { get; set; }
        public int TypeOfInvoice { get; set; }
        public Invoicerow[] InvoiceRows { get; set; }
        public string RowIdentifier { get; set; }
        public float InvoiceAmount { get; set; }
        public float InvoiceAmountRounded { get; set; }
        public bool IsInvoiceNumberRepeated { get; set; }
    }

    public class Invoicerow
    {
        public int RowId { get; set; }
        public string Kopnota { get; set; }
        public string Dag { get; set; }
        public string Tid { get; set; }
        public string ForsStalle { get; set; }
        public string Kundref { get; set; }
        public string Artikel { get; set; }
        public string Kvant { get; set; }
        public string Apris { get; set; }
        public string Brutto { get; set; }
        public string Rabatt { get; set; }
        public string Moms { get; set; }
        public string Netto { get; set; }
    }

}

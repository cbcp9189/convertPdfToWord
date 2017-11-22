using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PdfToWordWebSite.Models
{
    public class PdfStream
    {
        public int id { get; set; }
        public int doc_type { get; set; }
        public long doc_id { get; set; }
        public String pdf_path { get; set; }
    }
}
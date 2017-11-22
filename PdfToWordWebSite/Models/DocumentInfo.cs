using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PdfToWordWebSite.Models
{
    public class DocumentInfo
    {
        public int docId; 
        public int docType;
        public int userId;
        public DocumentInfo(int docId, int docType, int userId)
        {
            this.docId = docId;
            this.docType = docType;
            this.userId = userId;
        }
    }
}
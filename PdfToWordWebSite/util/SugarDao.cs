using MySqlSugar;
using PdfToWordWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PdfToWordWebSite.util
{
    public class SugarDao
    {
        private SugarDao()
        {
        }
        public static string ConnectionString
        {
            get
            {
                string reval = "server=106.75.116.2;uid=root;pwd=hoboom;database=scrapy";
                return reval;
            }
        }
        public static SqlSugarClient GetInstance()
        {
            var db = new SqlSugarClient(ConnectionString);
            db.IsEnableLogEvent = true;//Enable log events
            db.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par + "\r\n"); };
            return db;
        }

        public static void main(String[] arg)
        {
            using (var db = SugarDao.GetInstance())
            {
                var pdf = db.Queryable<PdfStream>().InSingle(1);
                Console.WriteLine(pdf.doc_id);
                Console.WriteLine(pdf.doc_type);
                Console.WriteLine(pdf.pdf_path);
            }
        }
    }
}
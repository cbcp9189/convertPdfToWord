using MySql.Data.MySqlClient;
using PdfToWordWebSite.Models;
using PdfToWordWebSite.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PdfToWordWebSite.Controllers
{
    public class convertController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public Response Get(long docId, int docType, int userId)
        {
            Result r = new Result();
            try
            {
                JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new JobProcessorServiceReference.IjobProcessorServiceClient();
                using (var db = SugarDao.GetInstance())
                {
                    MySqlParameter param0 = new MySqlParameter("doc_id", docId);
                    MySqlParameter param1 = new MySqlParameter("doc_type", docType);
                    MySqlParameter[] paramArray = new MySqlParameter[2];
                    paramArray[0] = param0;
                    paramArray[1] = param1;
                    List<PdfStream> list = db.SqlQuery<PdfStream>("select * from pdf_stream where doc_id=@doc_id and doc_type=@doc_type", paramArray);
                    if (list != null)
                    {
                        String pdfPath = "";
                        foreach (PdfStream pdfInfo in list)
                        {
                            pdfPath = PathUtil.getAbsolutePdfPath(pdfInfo.pdf_path, pdfInfo.doc_type);
                            LogHelper.WriteLog(typeof(convertController), pdfPath);
                            //pdfPath = pdfPath.Replace("/", @"\");
                            //LogHelper.WriteLog(typeof(convertController), pdfPath);
                        }
                        r.JobId = serviceClient.convertWordJob(pdfPath);
                    }
                }
                //r.JobId = serviceClient.convertWordJob(@"D:\solid\hellob.PDF");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(convertController), "convert error");
                LogHelper.WriteLog(typeof(convertController), ex.Message);
            }
            return Response.success(r);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
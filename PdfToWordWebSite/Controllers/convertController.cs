using MySql.Data.MySqlClient;
using PdfToWordWebSite.constant;
using PdfToWordWebSite.Models;
using PdfToWordWebSite.util;
using System;
using System.Collections.Generic;
using System.IO;
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
        public Response Get(long docId, int docType, int userId, String pdfPath)
        {
            Result r = new Result();
            try
            {
                JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new JobProcessorServiceReference.IjobProcessorServiceClient();
                LogHelper.WriteLog(typeof(convertController), pdfPath);
                if (pdfPath.StartsWith(SystemConstant.OPT_PATH))
                {
                    pdfPath = pdfPath.Replace(SystemConstant.OPT_PATH, "");
                }
                String local = Path.Combine(SystemConstant.NORMAL_PATH, pdfPath);
                String remotePath = Path.Combine(SystemConstant.REMOTE_PDF_PATH, Path.GetFileName(pdfPath));
                LogHelper.WriteLog(typeof(convertController), remotePath+"-"+local);

                bool isExist = Ftp.checkFile(local);
                if (!isExist)
                {
                    LogHelper.WriteLog(typeof(convertController), "服务器文件不存在");
                    
                }
                bool isSuccess = Ftp.donwload(remotePath, local);
                if (!isSuccess)
                {
                    LogHelper.WriteLog(typeof(convertController), "下载失败");
                }
                else
                {
                    r.JobId = serviceClient.convertWordJob(remotePath);
                }
                //r.JobId = serviceClient.convertWordJob(@"D:\solid\hellob.PDF");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(convertController), "convert error");
                LogHelper.WriteLog(typeof(convertController), ex);
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
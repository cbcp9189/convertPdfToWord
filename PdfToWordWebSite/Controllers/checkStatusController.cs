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
    public class checkStatusController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public Response Get(int JobId)
        {
            ResponseJson res = new ResponseJson();
            try
            {
                JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new JobProcessorServiceReference.IjobProcessorServiceClient();
                JobProcessorServiceReference.JobEnvelopeStatus status = serviceClient.GetJobStatus(JobId);
                Console.WriteLine(status.Message + "-message");
                LogHelper.WriteLog(typeof(convertController), status.Message + "-message");
                if (status.Status != JobProcessorServiceReference.JobStatus.Created && status.Status != JobProcessorServiceReference.JobStatus.Started)
                {
                    //成功就直接下载文件
                    Stream downloadStream;
                    long fileLength;
                    string strConvertedName = serviceClient.DownloadFile(JobId, out fileLength, out downloadStream);
                    LogHelper.WriteLog(typeof(convertController), strConvertedName);
                    string downloadPath = SystemConstant.BASE_DOC_PATH;
                    downloadPath = Path.Combine(downloadPath, strConvertedName);
                    downloadPath = downloadPath.Replace(".PDF", "").Replace(".pdf", "");
                    Console.WriteLine(downloadPath);
                    LogHelper.WriteLog(typeof(convertController), downloadPath);
                    FileStream localStream = new FileStream(downloadPath, FileMode.Create);
                    downloadStream.CopyTo(localStream);
                    localStream.Flush();
                    localStream.Close();
                    downloadStream.Close();
                    res.code = 1;
                    res.path = Path.Combine(SystemConstant.DOWNLOAD_PATH,strConvertedName.Replace(".PDF", "").Replace(".pdf", ""));
                }
                else
                {
                    res.code = 0;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(checkStatusController), "checkstatus error");
                LogHelper.WriteLog(typeof(checkStatusController), ex.Message);
                Console.WriteLine(ex.Message);
                res.code = 2;
            }
            return Response.success(res);
        }
    }
}
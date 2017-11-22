using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace PdfToWordWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //配置返回的时间类型数据格式 
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
            new Newtonsoft.Json.Converters.IsoDateTimeConverter()
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            });

            // 关闭xml 返回格式，只返回json 
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            
            //增加内容 start  
            //string s = Server.MapPath("log4net.config");
            //System.IO.FileInfo info = new System.IO.FileInfo(s);
            //log4net.Config.XmlConfigurator.Configure(info);
            //增加内容 end  

        }
    }
}

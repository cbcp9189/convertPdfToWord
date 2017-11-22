using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PdfToWordWebSite.Models
{
    public class Response
    {
        public int code;
        public String msg;
        public Object data;
        public Boolean ok;
        public Response()
        {
            this.msg = "ok";
            this.code = 0;
            this.ok = true;
        }

        public static Response success(Object data) {

            Response response = new Response();

            response.data = data;

            return response;

        }

        public static Response error(int code)
        {
            Response response = new Response();
            response.data = null;
            response.code = code;
            return response;
        }

    }
}
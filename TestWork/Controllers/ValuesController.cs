using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestWork.Code;
using TestWork.Models;

namespace TestWork.Controllers
{
    public class ValuesController : ApiController
    {              
        // GET api/values/5
        public HttpResponseMessage Get(string path)
        {
            WorkWithDirectory get_files = new WorkWithDirectory();           
            return Request.CreateResponse(HttpStatusCode.OK, get_files.GetDirAndFiles(path)); 
        }

    }
}
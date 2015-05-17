using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.IO;

namespace PriceTag.Controllers.api
{
    public class TagController : ApiController
    {
        //Multiple Uploads
        [HttpPost]
        public async Task<HttpResponseMessage> Uploads()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();

            // Read the form data.
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            int id = 0;

            Int32.TryParse(provider.FormData.GetValues("id")[0], out id);

            // Delete File - not working
            //var fi = new FileInfo(result.FileData.First().LocalFileName);
            //fi.Delete();

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            // IMPORTANT: replace "(tilde)" with the real tilde character
            // (our editor doesn't allow it, so I just wrote "(tilde)" instead)
            var uploadFolder = "~/App_Data"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Assignment_8.ApiControllers
{
    public class PdfHelperController : ApiController
    {
        [HttpGet]
        public String generatePdf(int pdfID)
        {            
            String pPath = HttpContext.Current.Server.MapPath("~/docs");
            String pdfName = EAD.DAL.PdfHelper.GeneratePDF(pPath, pdfID);
            return pdfName;
        }
        //[HttpGet]
        //public Object DownloadPDF(String pdf)
        //{
        //    var rootPath = System.Web.HttpContext.Current.Server.MapPath("~/docs");
            
        //    if (pdf != null)
        //    {
        //        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        //        var fileFullPath = System.IO.Path.Combine(rootPath, fileDTO.fileUniqueName + fileDTO.FileExt);
        //        byte[] file = System.IO.File.ReadAllBytes(fileFullPath);
        //        System.IO.MemoryStream ms = new System.IO.MemoryStream(file);

        //        response.Content = new ByteArrayContent(file);
        //        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //        response.Content.Headers.ContentType = new MediaTypeHeaderValue(fileDTO.ContentType);
        //        response.Content.Headers.ContentDisposition.FileName = fileDTO.Name;
        //        return response;
        //    }
        //    else
        //    {
        //        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
        //        return response;
        //    }
        //}
    }
}
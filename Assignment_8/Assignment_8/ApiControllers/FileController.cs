using EAD.Entities;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Assignment_8.ApiControllers
{
    public class FileController : ApiController
    {
        [HttpPost]
        public int uploadFile()
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                foreach (var fileName in HttpContext.Current.Request.Files.AllKeys)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
                    if (file != null)
                    {
                        FileDTO fileDTO = new FileDTO();
                        fileDTO.Name = file.FileName;
                        fileDTO.FileExt = Path.GetExtension(file.FileName);
                        fileDTO.ContentType = file.ContentType;
                        fileDTO.UploadedOn = DateTime.Now;
                        fileDTO.CreatedBy = Convert.ToInt32(HttpContext.Current.Request["createdBy"]);
                        fileDTO.ParentFolderID = Convert.ToInt32(HttpContext.Current.Request["parentFId"]);
                        fileDTO.FileSizeInKB = file.ContentLength / 1024; //file.ContentLength is the file size in bytes
                        fileDTO.fileUniqueName = Guid.NewGuid().ToString();

                        var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
                        var fileSavePath = System.IO.Path.Combine(rootPath, fileDTO.fileUniqueName + fileDTO.FileExt);
                        file.SaveAs(fileSavePath);

                        int result = EAD.BAL.FileBO.Save(fileDTO);
                        if (result > 0)
                            return result;
                        else
                            return 0;
                    }
                    else
                         return 0;                    
                }
            }
            return 0;            

        }
        [HttpGet]
        public Object DownloadFile(int uniqueName)
        {
            var rootPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles");
            var fileDTO = EAD.BAL.FileBO.GetFileById(uniqueName);
            if (fileDTO != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var fileFullPath = System.IO.Path.Combine(rootPath, fileDTO.fileUniqueName + fileDTO.FileExt);
                byte[] file = System.IO.File.ReadAllBytes(fileFullPath);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(file);

                response.Content = new ByteArrayContent(file);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(fileDTO.ContentType);
                response.Content.Headers.ContentDisposition.FileName = fileDTO.Name;
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
        [HttpGet]
        public bool DeleteFile(int fileId)
        {
            FileDTO dto = new FileDTO();
            dto.FileID = fileId;
            int r = EAD.BAL.FileBO.Save(dto);
            if (r >= 1)
                return true;
            else
                return false;
        }
        [HttpGet]
        public Object GetThumbnail(int id)
        {

            // Physical path of root folder
            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");

            // Find file from DB using unique name
            var FileDTO = EAD.BAL.FileBO.GetFileById(id);
            var fileFullPath = Path.Combine(rootPath, FileDTO.fileUniqueName + FileDTO.FileExt);

            ShellFile shellFile = ShellFile.FromFilePath(fileFullPath);
            Bitmap shellThumb = shellFile.Thumbnail.MediumBitmap;

            if (FileDTO != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                byte[] file = ImageToBytes(shellThumb); // Calling private ImageToBytes

                MemoryStream ms = new MemoryStream(file);

                response.Content = new ByteArrayContent(file);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");

                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(FileDTO.ContentType);
                response.Content.Headers.ContentDisposition.FileName = FileDTO.Name;
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }

        }
       
        private byte[] ImageToBytes(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
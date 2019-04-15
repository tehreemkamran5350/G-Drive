using EAD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment_8.ApiControllers
{
    public class FolderController : ApiController
    {
        [HttpGet]
        public bool CreateFolder(String Name, int CreatedBy)
        {
            FolderDTO dto = new FolderDTO();
            dto.Name = Name;
            dto.CreatedBy = CreatedBy;
            dto.CreatedOn = DateTime.Now;
            int r = EAD.BAL.FolderBO.Save(dto);
            if (r >= 1)
                return true;
            else
                return false;
        }
        [HttpGet]
        public bool CreateChildFolder(String Name, int CreatedBy, int parentId)
        {
            FolderDTO dto = new FolderDTO();
            dto.Name = Name;
            dto.CreatedBy = CreatedBy;
            dto.CreatedOn = DateTime.Now;
            dto.ParentFolderID = parentId;
            int r = EAD.BAL.FolderBO.Save(dto);
            if (r >= 1)
                return true;
            else
                return false;
        }

        [HttpGet]
        public List<FolderDTO> showFolders(int userId)
        {
            return EAD.BAL.FolderBO.GetAllFolders(userId);

        }
        [HttpGet]
        public bool DeleteFolder(int folderId)
        {
            FolderDTO dto = new FolderDTO();
            dto.FolderID = folderId;
            int r = EAD.BAL.FolderBO.Save(dto);
            if (r >= 1)
                return true;
            else
                return false;
        }
        [HttpGet]
        public List<FolderDTO> OpenFolder(int pId)
        {
            return EAD.BAL.FolderBO.GetAllSubFolders(pId);

        }
    }
}
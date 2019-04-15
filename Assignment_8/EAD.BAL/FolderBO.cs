using EAD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD.BAL
{
   public  class FolderBO
    {
       public static int Save(FolderDTO dto)
       {
           return EAD.DAL.FolderDAO.Save(dto);
       }
       public static List<FolderDTO> GetAllFolders(int userId)
       {
           return EAD.DAL.FolderDAO.GetAllFolders(userId);
       }
       public static List<FolderDTO> GetAllSubFolders(int parentFolderId)
       {
           return EAD.DAL.FolderDAO.GetAllSubFolders(parentFolderId);
       }

    }
}

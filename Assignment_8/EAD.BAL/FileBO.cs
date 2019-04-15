using EAD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD.BAL
{
    public class FileBO
    {
        public static int Save(FileDTO dto)
        {
            return EAD.DAL.FileDAO.Save(dto);
        }
        public static List<FileDTO> GetAllFiles(int userId)
        {
            return EAD.DAL.FileDAO.GetAllFiles(userId);
        }
        public static List<FileDTO> GetFiles()
        {
            return EAD.DAL.FileDAO.GetFiles();
        }
        public static FileDTO GetFileByName(string Name)
        {
            return EAD.DAL.FileDAO.GetFileByName(Name);
        }
        public static FileDTO GetFileById(int id)
        {
            return EAD.DAL.FileDAO.GetFileById(id);
        }
    }
}
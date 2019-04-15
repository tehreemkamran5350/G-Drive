using EAD.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD.DAL
{
   public static  class FileDAO
    {

       public static int Save(FileDTO dto)
       {
           using (DBHelper helper = new DBHelper())
           {
               String sqlQuery = "";
               if (dto.FileID > 0)
               {
                   sqlQuery = String.Format("Update dbo.Files Set IsActive='{0}' Where Id={1}",
                      0, dto.FileID);
                   helper.ExecuteQuery(sqlQuery);
                   return dto.FileID;
               }
               else
               {
                   sqlQuery = String.Format("INSERT INTO dbo.Files(Name, ParentFolderId, UploadedOn, CreatedBy,IsActive,FileExt,FileSizeinKB,fileUniqueName,ContentType) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'); Select @@IDENTITY",
                       dto.Name, dto.ParentFolderID, dto.UploadedOn, dto.CreatedBy,'1', dto.FileExt, dto.FileSizeInKB, dto.fileUniqueName,dto.ContentType);

                   var obj = helper.ExecuteScalar(sqlQuery);
                   return Convert.ToInt32(obj);
               }
           }
       }
       public static FileDTO GetFileById(int id)
       {

           var query = String.Format("Select * from dbo.Files Where Id={0}", id);

           using (DBHelper helper = new DBHelper())
           {
               var reader = helper.ExecuteReader(query);

               FileDTO dto = null;

               if (reader.Read())
               {
                   dto = FillDTO(reader);
               }

               return dto;
           }
       }

       public static FileDTO GetFileByName(string Name)
       {

           var query = String.Format("Select * from dbo.Files Where fileUniqueName={0}", Name);

           using (DBHelper helper = new DBHelper())
           {
               var reader = helper.ExecuteReader(query);

               FileDTO dto = null;

               if (reader.Read())
               {
                   dto = FillDTO(reader);
               }

               return dto;
           }
       }

       public static List<FileDTO> GetAllFiles(int userId)
       {
           var query = string.Format("Select * from dbo.Files Where IsActive = 1 and CreatedBy={0} ", userId);

           using (DBHelper helper = new DBHelper())
           {
               var reader = helper.ExecuteReader(query);
               List<FileDTO> list = new List<FileDTO>();

               while (reader.Read())
               {
                   var dto = FillDTO(reader);
                   if (dto != null)
                   {
                       list.Add(dto);
                   }
               }

               return list;
           }
       }

       public static List<FileDTO> GetFiles()
       {
           var query = string.Format("Select * from dbo.Files Where IsActive = 1");

           using (DBHelper helper = new DBHelper())
           {
               var reader = helper.ExecuteReader(query);
               List<FileDTO> list = new List<FileDTO>();

               while (reader.Read())
               {
                   var dto = FillDTO(reader);
                   if (dto != null)
                   {
                       list.Add(dto);
                   }
               }

               return list;
           }
       }


       public static List<FileDTO> getMetaDataOfFiles(int id)
       {
           List<int> id_list = new List<int>();
           List<FileDTO> files_list = new List<FileDTO>();
           int i = 0;
           id_list.Add(id);
           do
           {
               String name = "ROOT";
               String query = "Select * from dbo.Files where ParentFolderId='" + id_list[i] + "'";
               try
               {
                   using (DBHelper helper = new DBHelper())
                   {
                       FileDTO file = new FileDTO();
                       var result = helper.ExecuteReader(query);
                       while (result.Read())
                       {
                           using (DBHelper helper2 = new DBHelper())
                           {
                               String parent_query = "Select * from dbo.Folder where Id ='" + id_list[i] + "'";
                               var parent = helper2.ExecuteReader(parent_query);
                               if (parent.Read())
                               {
                                   name = parent.GetString(1);
                               }
                           }
                           file = FillDTO(result);
                           file.parentFolderName = name;
                           files_list.Add(file);
                       }
                       using (DBHelper helper3 = new DBHelper())
                       {
                           String parentFol = "Select * from dbo.Folder where ParentFolderId='" + id_list[i] + "'";
                           var fid_result = helper3.ExecuteReader(parentFol);
                           while (fid_result.Read())
                           {
                               id_list.Add(fid_result.GetInt32(0));
                           }
                       }

                   }
                   i++;
               }
               catch (Exception e)
               {
                   Console.Write(e.Message);
               }

           } while (i < id_list.Count);
           return files_list;
       }


       private static FileDTO FillDTO(SqlDataReader reader)
       {
           var dto = new FileDTO();
           dto.FileID = reader.GetInt32(0);
           dto.Name = reader.GetString(1);
           dto.ParentFolderID = reader.GetInt32(2);
           dto.FileExt = reader.GetString(3);
           dto.FileSizeInKB = reader.GetInt32(4);
           dto.CreatedBy = reader.GetInt32(5);
           dto.UploadedOn = reader.GetDateTime(6);
           dto.IsActive = reader.GetBoolean(7);
           dto.ContentType = reader.GetString(8);
           dto.fileUniqueName = reader.GetString(9);
           return dto;
       }
    }
}

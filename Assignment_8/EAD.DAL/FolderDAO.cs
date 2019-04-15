using EAD.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD.DAL
{
    public static class FolderDAO
    {
        public static int Save(FolderDTO dto)
        {
            using (DBHelper helper = new DBHelper())
            {
                String sqlQuery = "";
                if (dto.FolderID > 0)
                {
                    sqlQuery = String.Format("Update dbo.Folder Set IsActive='{0}' Where Id={1}",
                       0,dto.FolderID);
                    helper.ExecuteQuery(sqlQuery);
                    return dto.FolderID;
                }
                else
                {
                    sqlQuery = String.Format("INSERT INTO dbo.Folder(Name, ParentFolderId, CreatedOn, CreatedBy,IsActive) VALUES('{0}','{1}','{2}','{3}','{4}'); Select @@IDENTITY",
                        dto.Name, dto.ParentFolderID, dto.CreatedOn, dto.CreatedBy, 1);

                    var obj = helper.ExecuteScalar(sqlQuery);
                    return Convert.ToInt32(obj);
                }
            }
        }
        public static List<FolderDTO> GetAllFolders(int userId)
        {
            var query = string.Format("Select * from dbo.Folder Where IsActive = 1 and CreatedBy={0} and ParentFolderId={1}",userId,0);

            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);
                List<FolderDTO> list = new List<FolderDTO>();

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
        public static List<FolderDTO> GetAllSubFolders(int parentFolderId)
        {
            var query = string.Format("Select * from dbo.Folder Where IsActive = 1 and ParentFolderId={0}", parentFolderId);

            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);
                List<FolderDTO> list = new List<FolderDTO>();

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

        public static List<FolderDTO> getMetaDataOfFolders(int id)
        {
            List<int> id_list = new List<int>();
            List<FolderDTO> folder_list = new List<FolderDTO>();
            int i = 0;
            id_list.Add(id);
            do
            {
                String name = "ROOT";
                String query = "Select * from dbo.Folder where ParentFolderId='" + id_list[i] + "'";
                try
                {
                    using (DBHelper helper = new DBHelper())
                    {
                        FolderDTO folder = new FolderDTO();
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
                            folder = FillDTO(result);
                            folder.parentFolderName = name;
                            folder_list.Add(folder);
                            id_list.Add(result.GetInt32(0));
                        }
                    }
                    i++;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }

            } while (i < id_list.Count);
            return folder_list;
        }

        private static FolderDTO FillDTO(SqlDataReader reader)
        {
            var dto = new FolderDTO();
            dto.FolderID= reader.GetInt32(0);
            dto.Name = reader.GetString(1);
            dto.ParentFolderID = reader.GetInt32(2);
            dto.CreatedOn = reader.GetDateTime(3);            
            dto.IsActive = reader.GetBoolean(4);
            dto.CreatedBy = reader.GetInt32(5);
           
         
            return dto;
        }
        }
    }


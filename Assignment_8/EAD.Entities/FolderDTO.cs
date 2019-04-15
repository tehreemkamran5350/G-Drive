using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD.Entities
{
   public class FolderDTO
    {
        public int FolderID { get; set; }
        public int ParentFolderID { get; set; }
        public String Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public String parentFolderName { get; set; }
    }
}

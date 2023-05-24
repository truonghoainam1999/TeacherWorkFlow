using HMZ.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Queries.Catalog
{
    public class ClassRoomQuery : BaseEntity
    {
        public String? Name { get; set; }
    }
}

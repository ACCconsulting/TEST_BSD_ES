using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class EntityAudit
    {
        public string CreateBy { get; set; }
        public DateTime? Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}

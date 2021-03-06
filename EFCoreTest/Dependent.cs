using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreTest
{
    public partial class Dependent
    {
        public Dependent()
        {
            DependentAttributes = new HashSet<DependentAttribute>();
        }

        public int DependentId { get; set; }
        public int ParentId { get; set; }

        public virtual Parent Parent { get; set; }
        public virtual ICollection<DependentAttribute> DependentAttributes { get; set; }
    }
}

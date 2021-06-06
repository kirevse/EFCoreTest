using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreTest
{
    public partial class Parent
    {
        public int ParentId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; } = new HashSet<Dependent>();
    }
}

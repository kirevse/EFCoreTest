using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreTest
{
    public partial class Parent
    {
        public Parent()
        {
            Dependents = new HashSet<Dependent>();
        }

        public int ParentId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }
    }
}

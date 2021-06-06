using System;
using System.Collections.Generic;

#nullable disable

namespace EFCoreTest
{
    public partial class DependentAttribute
    {
        public int DependentAttributeId { get; set; }
        public int DependentId { get; set; }
        public int ReferenceDataId { get; set; }

        public virtual Dependent Dependent { get; set; }
        public virtual ReferenceDatum ReferenceData { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Program
{
    using System;
    using System.Collections.Generic;
    
    public partial class Material
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Material()
        {
            this.UsedMaterials = new HashSet<UsedMaterial>();
        }
    
        public int Id { get; set; }
        public Nullable<int> NakladnayaNumberID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Name { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> Sum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsedMaterial> UsedMaterials { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarSales.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cars
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cars()
        {
            this.Advertisements = new HashSet<Advertisements>();
            this.Photos = new HashSet<Photos>();
        }
    
        public int Id { get; set; }
        public int MarkModel { get; set; }
        public string Vin { get; set; }
        public string StateNumber { get; set; }
        public int Year { get; set; }
        public int Color { get; set; }
        public int Transmission { get; set; }
        public int Handlebar { get; set; }
        public int Wheeldrive { get; set; }
        public int Bodywork { get; set; }
        public int Engine { get; set; }
        public string Mileage { get; set; }
        public int NumOfOwners { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Advertisements> Advertisements { get; set; }
        public virtual Bodyworks Bodyworks { get; set; }
        public virtual Colors Colors { get; set; }
        public virtual Engines Engines { get; set; }
        public virtual Handlebars Handlebars { get; set; }
        public virtual Models Models { get; set; }
        public virtual Transmissions Transmissions { get; set; }
        public virtual Wheeldrives Wheeldrives { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photos> Photos { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESN3.WebUI.PROFESSORWEB
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderSet
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string Discription { get; set; }
        public int Quanity { get; set; }
        public System.DateTime PurhaseDate { get; set; }
        public int CustomerCustomerId { get; set; }
    
        public virtual CustomerSet CustomerSet { get; set; }
    }
}

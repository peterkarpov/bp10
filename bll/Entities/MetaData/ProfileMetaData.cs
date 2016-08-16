using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ESN.Domain.Entities.MetaData
{
    public partial class ProfileMetaData
    {
        [Key]

        [HiddenInput(DisplayValue = false)]
        public Guid ProfileId { get; set; }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please, enter fName")]
        public string fName { get; set; }


        [Display(Name = "Middle Name")]
        public string mName { get; set; }


        [Display(Name = "Last Name")]
        public string lName { get; set; }
        

        //[DataType(DataType.MultilineText)]
        [Display(Name = "date of birthday")]
        [Required(ErrorMessage = "Please, enter dob")]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        //[Range(typeof(DateTime), "1970", "" , ErrorMessage = "wrong value of Date Time")]
        //[UIHint("DateTime")]
        //[DataType(DataType.Date)]
        public DateTime dob { get; set; }


        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please, enter Gender")]
        public Gender? Gender { get; set; }
        

        public string Annotation { get; set; }
        

        public language? Language { get; set; }


        public country? Country { get; set; }


        public RelationShipStatus? rStatus { get; set; }
        

        [HiddenInput(DisplayValue = false)]
        public Guid? AvatarImageId { get; set; }
        

        [HiddenInput(DisplayValue = false)]
        public virtual List<News> News { get; set; }


        [HiddenInput(DisplayValue = false)]
        public virtual List<Photobook> Photobooks { get; set; }
    }
}

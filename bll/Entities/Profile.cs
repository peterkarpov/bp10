using ESN.Domain.Entities.MetaData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ESN.Domain.Entities
{
    [MetadataType(typeof(ProfileMetaData))]
    public class Profile
    {
        public Guid ProfileId { get; set; }
        public string fName { get; set; }
        public string mName { get; set; }
        public string lName { get; set; }
        public DateTime dob { get; set; }
        public Gender? Gender { get; set; }
        public string Annotation { get; set; }
        public language? Language { get; set; }
        public country? Country { get; set; }
        public RelationShipStatus? rStatus { get; set; }
        public Guid? AvatarImageId { get; set; }
        
        public virtual List<News> News { get; set; }
        public virtual List<Photobook> Photobooks { get; set; }

    }
    public enum language
    {
        russian,
        english,
        japanese,
        french,
        german,
    }

    public enum country
    {
        Russia,
        England,
    }

    public enum Gender
    {
        none,
        male,
        female,
    }

    public enum RelationShipStatus
    {
        none,
        maried,
        friend,
    }
}

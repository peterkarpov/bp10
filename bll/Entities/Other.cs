using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using ESN.Domain.Concrete;

namespace ESN.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        //

    }

    public class Message
    {
        public Guid MessageId { get; set; }
        public Guid from { get; set; }//public Guid from { get; set; }

        public string text { get; set; }

        public DateTime creationTime { get; set; }

        public Guid TalkId { get; set; }
        

        //[ForeignKey("ProfileTo")]
        public Guid to { get; set; }

        [ForeignKey("to")]
        public virtual Profile ProfileTo { get; set; }

        //?????????????????????????????????????????????????
        //public virtual Profile ProfileTo { get { return new EFDbContext().Profiles.Find(to); } }
        //public virtual Profile ProfileFrom { get { return new EFDbContext().Profiles.Find(ProfileId); } }



        [ForeignKey("from")]
        public virtual Profile ProfileFrom { get; set; }

        // Ссылка на контент
        public virtual List<Content> Contents { get; set; }
    }

    public class Talk
    {
        public Guid TalkId { get; set; }
        public Guid Profile1Id { get; set; }//public Guid Profile1Id { get; set; }//from
        public Guid Profile2Id { get; set; }//public Guid Profile2Id { get; set; }//to
        [ForeignKey("Profile2Id")]
        public virtual Profile Profile2 { get; set; }
        [ForeignKey("Profile1Id")]
        public virtual Profile Profile1 { get; set; }
        public virtual List<Message> Messages { get; set; }
    }

    public class Content
    {
        public Guid ContentId { get; set; }
        public string type { get; set; }
        public string annotation { get; set; }

        //ссылка на сообщение
        public Message Message { get; set; }
        //public List<Guid> items { get; set; }

    }

    public class Friend
    {
        public int FriendId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid subscriberId { get; set; }

 
    }

    public class News
    {
        [HiddenInput(DisplayValue = false)]
        public Guid NewsId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public Guid ProfileId { get; set; }
        public List<Guid> contents { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime creationTime { get; set; }
        [Required(ErrorMessage = "dont be a null")]
        public string theme { get; set; }


        //
        [ForeignKey("TargetId")]
        public virtual List<Like> Likes { get; set; }

        public virtual Profile Profile { get; set; }

        [ForeignKey("TargetId")]
        public virtual List<Comment> Comments { get; set; }

        [ForeignKey("TargetId")]
        public virtual List<Dislike> Dislikes { get; set; }
    }

    public class Photobook
    {
        public string Title { get; set; }
        public Guid ProfileId { get; set; }
        public Guid PhotobookId { get; set; }
        public int viewForFriend { get; set; }
        public int viewForAll { get; set; }


        
        //Entity Framework
        public virtual Profile Profile { get; set; }
        public virtual List<Image> Images { get; set; } //http://professorweb.ru/my/entity-framework/6/level1/1_4.php

        //Lazy Loading

        //private List<Image> myImages;

        //public Photobook()
        //{
        //    myImages = null;
        //}

        //public virtual List<Image> Images
        //{
        //    get
        //    {
        //        if (myImages == null)
        //        {
        //            myImages = DataLoader.Load();
        //        }
        //        return myImages;
        //    }

        //}
    }

    public class Image
    {
        public Guid ImageId { get; set; }
        public Guid PhotobookId { get; set; }
        [Required(ErrorMessage = "ImageData is null")]
        public byte[] ImageData { get; set; }
        [Required(ErrorMessage = "ImageMimeType is null")]
        public string ImageMimeType { get; set; }

        [Display(Name = "File Name")]
        public string fileName { get; set; }

        public Photobook Photobook { get; set; } //http://professorweb.ru/my/entity-framework/6/level1/1_4.php

        public DateTime DateOfLoad { get; set; }
    }

    public class Text
    {
        public Guid id { get; set; }
        public string item { get; set; }
    }

    public class Like
    {
        public Guid LikeId { get; set; }
        public Guid TargetId { get; set; }
        public Guid ProfileId { get; set; }


        //public virtual Guid target { get; set; }
        //public virtual string type { get; set; }
    }

    public class Dislike
    {
        public Guid DislikeId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid TargetId { get; set; }
    }

    public class Content2
    {
        public Guid ContentId { get; set; }
        public Guid ProfileId {get;set;}
        public object item { get; set; }
        public string itemMimeType { get; set; }

        public Guid TargetId { get; set; }

        public virtual Profile Profile { get; set; }
    }

    public class Comment
    {
        public Guid CommentId { get; set; }
        public string text { get; set; }

        public DateTime creationTime { get; set; }
        public Guid ProfileId { get; set; }

        public Guid TargetId { get; set; }
        

        public virtual Profile Profile { get; set; }

        [ForeignKey("TargetId")]
        public virtual List<Like> Likes { get; set; }

        [ForeignKey("TargetId")]
        public virtual List<Dislike> Dislikes { get; set; }
    }


    
}

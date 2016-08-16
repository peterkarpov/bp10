using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ESN.Domain.Concrete
{
    public class EFOtherRepository : IOtherRepository
    {
        private EFDbContext otherContext = new EFDbContext();

        //public EFOtherRepository()
        //{
        //    //string mdfFilePath = HttpContext.Current.Server.MapPath("~/App_Data/ESN2.mdf");
        //    string mdfFilePath = HttpContext.Current.Server.MapPath("~/App_Data/ESN3.mdf");
        //    otherContext = new EFDbContext(string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0};Integrated Security=True", mdfFilePath));
        //    //context = new EFDbContext(string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Documents\ESN2.mdf;Integrated Security=True;Connect Timeout=30", mdfFilePath));
        //}



        public IEnumerable<User> Users
        {
            get
            {
                return otherContext.Users;

                //return new List<User>()
                //{
                //    new User { UserId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a1"), login = "login1", password = "12345", email = "login1@email.com" },
                //    new User { UserId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a2"), login = "login2", password = "12345", email = "login2@email.com" },
                //    new User { UserId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a3"), login = "login3", password = "12345", email = "login3@email.com" },
                //    new User { UserId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a4"), login = "login4", password = "12345", email = "login4@email.com" },
                //    new User { UserId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a5"), login = "login5", password = "12345", email = "login5@email.com" },
                //    new User { UserId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a6"), login = "login6", password = "12345", email = "login6@email.com" },
                //    new User { UserId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a7"), login = "login7", password = "12345", email = "login7@email.com" },
                //    new User { UserId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a8"), login = "login8", password = "12345", email = "login8@email.com" },
                //    new User { UserId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a9"), login = "login9", password = "12345", email = "login9@email.com" },

                //};
            }
        }

        public bool AddUser(User user)
        {
            otherContext.Users.Add(user);
            return otherContext.SaveChanges() > 0;
        }

        public bool EditUser(User user)
        {
            User dbEntry = otherContext.Users.Find(user.UserId);
            if (dbEntry != null)
            {
                dbEntry.login = user.login;
                dbEntry.email = user.email;
                dbEntry.password = user.password;
            }
            return otherContext.SaveChanges() > 0;
        }

        public bool DeleteUser(Guid UserId)
        {
            User dbEntry = otherContext.Users.Find(UserId);
            if (dbEntry != null)
            {
                otherContext.Users.Remove(dbEntry);
            }
            return otherContext.SaveChanges() > 0;
        }



        public IEnumerable<Content> Contents
        {
            get
            {


                return new List<Content>()
                {
                    new Content { ContentId = Guid.Parse("4da7b61b-832d-4e4d-8fc1-c32217b5bc91"), annotation = "exampleAnnotaton1", type = "image", /*items = default(List<Guid>)*/},
                    new Content { ContentId = Guid.Parse("4da7b61b-832d-4e4d-8fc1-c32217b5bc92"), annotation = "exampleAnnotaton2", type = "image", /*items = default(List<Guid>)*/},
                    new Content { ContentId = Guid.Parse("4da7b61b-832d-4e4d-8fc1-c32217b5bc93"), annotation = "exampleAnnotaton3", type = "image", /*items = default(List<Guid>)*/},
                    new Content { ContentId = Guid.Parse("4da7b61b-832d-4e4d-8fc1-c32217b5bc94"), annotation = "exampleAnnotaton4", type = "text",  /*items = default(List<Guid>)*/},
                    new Content { ContentId = Guid.Parse("4da7b61b-832d-4e4d-8fc1-c32217b5bc95"), annotation = "exampleAnnotaton5", type = "text",  /*items = default(List<Guid>)*/},
                    new Content { ContentId = Guid.Parse("4da7b61b-832d-4e4d-8fc1-c32217b5bc96"), annotation = "exampleAnnotaton6", type = "text",  /*items = default(List<Guid>)*/},
                };
            }
        }

        public IEnumerable<Friend> Friends
        {
            get
            {
                return otherContext.Friends;
                //return new List<Friend>()
                //{
                //    new Friend { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a1"), subscriberId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a2") },
                //};
            }
        }

        public bool AddToFriends(Friend Friend)
        {
            var friend = from f in otherContext.Friends
                         where f.ProfileId == Friend.ProfileId
                         where f.subscriberId == Friend.subscriberId
                         select f;

            if (friend != null)
            {
                otherContext.Friends.Add(Friend);
            }

            return otherContext.SaveChanges() > 0;

        }

        public bool delToFriends(Friend Friend)
        {
            var friend = from f in otherContext.Friends
                         where f.ProfileId == Friend.ProfileId
                         where f.subscriberId == Friend.subscriberId
                         select f;

            if (friend != null)
            {
                otherContext.Friends.Remove(Friend);
            }

            return otherContext.SaveChanges() > 0;
        }
        public bool setToFriends(Friend Friend)
        {
            var friend = from f in otherContext.Friends
                         where f.ProfileId == Friend.ProfileId
                         where f.subscriberId == Friend.subscriberId
                         select f;

            if (friend != null)
            {
                otherContext.Friends.Remove(Friend);
            }
            else
            {
                otherContext.Friends.Remove(Friend);
            }

            return otherContext.SaveChanges() > 0;
        }
        public bool isFriend(Friend Friend)
        {
            var friend = from f in otherContext.Friends
                         where f.ProfileId == Friend.ProfileId
                         where f.subscriberId == Friend.subscriberId
                         select f;

            return friend != null;
        }

        public bool isBestFriend(Friend Friend)
        {
            var friend1 = from f in otherContext.Friends
                          where f.ProfileId == Friend.ProfileId
                          where f.ProfileId == Friend.subscriberId
                          select f;

            var friend2 = from f in otherContext.Friends
                          where f.ProfileId == Friend.subscriberId
                          where f.subscriberId == Friend.ProfileId
                          select f;

            return (friend1 != null) && (Friend != null);
        }


        public IEnumerable<Image> Images
        {
            get
            {
                return otherContext.Images;

                //return new List<Image>()
                //{
                //    new Image { ImageId = Guid.Parse("2d3c3e11-3554-41b9-89f9-6e8d677ea1a1"), ImageData = default(byte[]), ImageMimeType = "image/jpeg" },
                //};
            }
        }

        public bool SaveImage(Image image)
        {

            if (otherContext.Images.Find(image.ImageId) == null)
            {
                if (image.ImageId == default(Guid)) image.ImageId = Guid.NewGuid();
                if (image.DateOfLoad == default(DateTime)) image.DateOfLoad = DateTime.Now;
                otherContext.Images.Add(image);
            }
            else
            {
                var dbEntry = otherContext.Images.Find(image.ImageId);
                if (dbEntry != null)
                {
                    dbEntry.ImageId = image.ImageId;
                    dbEntry.PhotobookId = image.PhotobookId;
                    dbEntry.ImageData = image.ImageData;
                    dbEntry.ImageMimeType = image.ImageMimeType;
                    dbEntry.fileName = image.fileName;
                    dbEntry.DateOfLoad = image.DateOfLoad;

                }
            }
            return otherContext.SaveChanges() > 0;
        }

        public IEnumerable<Like> Likes
        {
            get
            {
                return otherContext.Likes;

                //return new List<Like>()
                //{
                //    new Like { target = Guid.Parse("a9ea0f22-bf85-420f-9f31-2939906dc201"), ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a1")},
                //};
            }
        }

        public bool SetLike(Like Like, string type)
        {
            Like dbEntry = null;

            switch (type)
            {
                case "News":
                    var like = from l in otherContext.Likes
                               where l.ProfileId == Like.ProfileId
                               where l.TargetId == Like.TargetId
                               select l;
                    dbEntry = like.FirstOrDefault();
                    break;
                default:
                    break;
            }

            if (dbEntry == null)
            {
                if (Like.LikeId == default(Guid)) Like.LikeId = Guid.NewGuid();
                otherContext.Likes.Add(Like);
            }
            else
            {
                otherContext.Likes.Remove(dbEntry);
            }

            return otherContext.SaveChanges() > 0;

        }

        public IEnumerable<Dislike> Dislikes
        {
            get
            {
                return otherContext.Dislikes;
            }
        }

        public bool SetDislike(Dislike Dislike)
        {
            var Like = otherContext.Dislikes
                .Where(p => p.ProfileId == Dislike.ProfileId)
                .Where(t => t.TargetId == Dislike.TargetId)
                .FirstOrDefault();

            if (Like == null)
            {
                otherContext.Dislikes.Add(Dislike);
            }
            else
            {
                otherContext.Dislikes.Remove(Like);
            }

            return otherContext.SaveChanges() > 0;

        }

        public IEnumerable<Message> Messages
        {
            get
            {
                return otherContext.Messages;
                //return new List<Message>()
                //{
                //    new Message { MessageId = Guid.Parse("dfd5dcce-6041-435d-b6d5-45090e8690b1"), contents = new List<Guid>(){ Guid.Parse("07b2bf05-4aa5-4777-9c70-22d6e5700c91")}, to = Guid.Parse("4da7b61b-832d-4e4d-8fc1-c32217b5bc91"), from = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a2"), creationTime = default(DateTime) },
                //};
            }
        }

        public IEnumerable<Talk> Talks
        {
            get { return otherContext.Talks; }
        }

        public bool SaveTalk(Talk Talk)
        {
            otherContext.Talks.Add(Talk);
            return otherContext.SaveChanges() > 0;
        }

        public bool SaveMessage(Message message)
        {
            if (message.MessageId != default(Guid))
            {
                otherContext.Messages.Add(message);
            }
            else
            {
                Message dbEntry = otherContext.Messages.Find(message.MessageId);
                if (dbEntry != null)
                {
                    dbEntry.Contents = message.Contents;
                    dbEntry.creationTime = message.creationTime;
                    dbEntry.to = message.to;
                    dbEntry.MessageId = message.MessageId;
                    //dbEntry.Profile = message.Profile;
                    //dbEntry.ProfileId = message.ProfileId;
                    dbEntry.from = message.from;
                    //dbEntry.theme = message.theme;
                    dbEntry.TalkId = message.TalkId;
                }
            }

            return otherContext.SaveChanges() > 0;
        }


        public IEnumerable<News> News
        {
            get
            {
                return otherContext.News;

                //return new List<News>()
                //{
                //    new News { id = Guid.Parse("a9ea0f22-bf85-420f-9f31-2939906dc201"), ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a1"), contents = new List<Guid>() { Guid.Parse("4da7b61b-832d-4e4d-8fc1-c32217b5bc91") }, creationTime = default(DateTime) },
                //};
            }
        }

        public bool SaveNews(News News)
        {
            var dbEntry = otherContext.News.Find(News.NewsId);

            if (dbEntry == null)
            {
                if (News.NewsId == default(Guid)) News.NewsId = Guid.NewGuid();
                otherContext.News.Add(News);
            }
            else
            {
                dbEntry.contents = News.contents;
                dbEntry.creationTime = News.creationTime;
                dbEntry.theme = News.theme;
            }

            return otherContext.SaveChanges() > 0;
        }

        public bool DeleteNews(Guid NewsId)
        {
            var dbEntry = otherContext.News.Find(NewsId);

            if (dbEntry != null)
                otherContext.News.Remove(dbEntry);

            return otherContext.SaveChanges() > 0;
        }

        public IEnumerable<Photobook> Photobooks
        {
            get
            {
                return otherContext.Photobooks;
                //return new List<Photobook>()
                //{
                //    new Photobook { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a1"), title = "title1", images = new List<Guid>() { Guid.Parse("2d3c3e11-3554-41b9-89f9-6e8d677ea1a1") }, viewForFriend = true, viewForAll = true },
                //};
            }
        }

        public bool SavePhotobook(Photobook photobook)
        {
            if (photobook.PhotobookId == default(Guid))
            {
                photobook.PhotobookId = Guid.NewGuid();
                otherContext.Photobooks.Add(photobook);
            }
            else
            {
                Photobook dbEntry = otherContext.Photobooks.Find(photobook.PhotobookId);
                if (dbEntry != null)
                {
                    dbEntry.Title = photobook.Title;

                }
            }

            return otherContext.SaveChanges() > 0;
        }



        public IEnumerable<Text> Texts
        {
            get
            {
                return new List<Text>()
                {
                    new Text {id = Guid.Parse("07b2bf05-4aa5-4777-9c70-22d6e5700c91"), item = "example message text" }
                };
            }
        }

        public IEnumerable<Comment> Comments
        {
            get
            {
                return otherContext.Comments;
            }
        }

        public bool SaveComment(Comment comment)
        {
            if (comment.CommentId != default(Guid))
            {
                otherContext.Comments.Add(comment);
            }
            else if (string.IsNullOrEmpty(comment.text)) //deleting ?? need more tests
            {
                Comment dbEntry = otherContext.Comments.Find(comment.CommentId);
                if (dbEntry != null)
                {
                    otherContext.Comments.Remove(dbEntry);
                }
            }
            else
            {
                Comment dbEntry = otherContext.Comments.Find(comment.CommentId);
                if (dbEntry != null)
                {
                    dbEntry.text = comment.text;

                }
            }

            return otherContext.SaveChanges() > 0;
        }




        






    }
}

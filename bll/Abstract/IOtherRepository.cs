using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESN.Domain.Abstract
{
    public interface IOtherRepository
    {
        IEnumerable<User> Users { get; }
            bool AddUser(User user);
            bool EditUser(User user);
        IEnumerable<Message> Messages { get; }
            bool SaveMessage(Message message);
        IEnumerable<Talk> Talks { get; }
            bool SaveTalk(Talk Talk);
        IEnumerable<Content> Contents { get; }
        IEnumerable<Comment> Comments { get; }
            bool SaveComment(Comment Comment);
        IEnumerable<Friend> Friends { get; }
            bool AddToFriends(Friend Friend);
            bool delToFriends(Friend Friend);
            bool setToFriends(Friend Friend);
            bool isFriend(Friend Friend);
            bool isBestFriend(Friend Friend);
        IEnumerable<News> News { get; }
            bool SaveNews(News News);
            bool DeleteNews(Guid NewsId);
        IEnumerable<Photobook> Photobooks { get; }
            bool SavePhotobook (Photobook photobook);
        IEnumerable<Image> Images { get; }
            bool SaveImage(Image image);
        IEnumerable<Like> Likes { get; }
            bool SetLike(Like Like, string type);
        IEnumerable<Dislike> Dislikes { get; }
            bool SetDislike(Dislike Dislike);

        
    }
}

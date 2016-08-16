using System.Web.Security;
using ESN3.WebUI.Infrastructure.Abstract;
using ESN.Domain.Abstract;
using System.Linq;
using ESN.Domain.Concrete;
using ESN.Domain.Entities;
using System.Web;
using System;

namespace ESN3.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        private IOtherRepository otherRepository;

        public FormAuthProvider(IProfileRepository repo, IOtherRepository otherRepo)
        {
            otherRepository = otherRepo;
        }

        public bool Authenticate(string username, string password)
        {
            //bool result = FormsAuthentication.Authenticate(username, password);

            var user = otherRepository.Users.FirstOrDefault(x => x.login == username);

            if (user == null) return false;

            bool result = user.password == password;

            string UserId = user.UserId.ToString();

            username += "|";
            username += UserId;

            if (result)
                FormsAuthentication.SetAuthCookie(username, false);



            //HttpCookie cookie = new HttpCookie("Profile cookie");
            //cookie["ProfileId"] = otherRepository.Users.FirstOrDefault(x => x.login == username).UserId.ToString();

            return result;
        }

        public bool Registrate(User user)
        {
            otherRepository.AddUser(user);
            return Authenticate(user.login, user.password);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        
    }
}
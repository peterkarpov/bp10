using ESN.Domain.Entities;

namespace ESN3.WebUI.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
        void SignOut();

        bool Registrate(User user);
    }
}

using Ebookstore.Models;

namespace Ebookstore.Interfaces
{
    public interface IUsersRepository
    {
        //int AddUser(UserModel userModel);
        //int DeleteUser(UserModel userModel);
        //int EditUser(UserModel userModel);
        public List<UserModel> UserList();

    }
}

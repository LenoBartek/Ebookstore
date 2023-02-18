using Ebookstore.Interfaces;
using Ebookstore.Models;
using Ebookstore.Repositories;
using Microsoft.AspNetCore.Components;

namespace Ebookstore.Pages
{
    public partial class Users : ComponentBase
    {
        [Inject]
        public UsersRepository? _usersRepository { get; set; }
        private List<UserModel> UsersList { get; set; }

        protected override Task OnInitializedAsync()
        {
            ReadUsers();
            return base.OnInitializedAsync();
        }

        private void ReadUsers()
        {
            UsersList = _usersRepository.UserList();
        }

    }
}

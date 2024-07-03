using Entites.Enum;
using System.ComponentModel.DataAnnotations;

namespace Entites
{
    public class AdminUsersEntity
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public AdminRoleEnum Role { get; set; }

    }
}

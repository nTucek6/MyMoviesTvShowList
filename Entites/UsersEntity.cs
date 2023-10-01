using Entites.Enum;
using System.ComponentModel.DataAnnotations;

namespace Entites
{
    public class UsersEntity
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Username { get; set; }
        public RolesEnum RoleId { get; set; }
        public byte[]? ProfileImageData { get; set; }
        public string? UserBio { get; set; }
        public DateTime Joined { get; set; }
    }
}

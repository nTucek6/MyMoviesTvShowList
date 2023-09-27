using Entites.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class UsersEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Username { get; set; }
        public RolesEnum RoleId { get; set; }
        public byte[]? ProfileImageData { get; set; }
        public string? UserBio { get; set; }
        public DateTime Joined { get; set; }
    }
}

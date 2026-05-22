using System;

namespace BankManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public User()
        {
            Username = "";
            PasswordHash = "";
            Email = "";
            FullName = "";
            SecurityQuestion = "";
            SecurityAnswer = "";
            Role = "";
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace JarekWebAPI.WebApi
{
    public class AccountUser
    {
        public Guid Id { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
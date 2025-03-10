using System;
using System.ComponentModel.DataAnnotations;

namespace JarekWebAPI.WebApi
{
    public class AccountUser
    {
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
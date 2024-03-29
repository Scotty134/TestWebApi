﻿namespace Infrastructure.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
    }
}

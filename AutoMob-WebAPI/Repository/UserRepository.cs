﻿using AutoMob_WebAPI.Models;

namespace AutoMob_WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly VehicleDbContext _context;

        public UserRepository(VehicleDbContext context)
        {
            _context = context;
        }
        public UserModel Authenticate(string username, string password)
        {
            return _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
        }

        public void Register(UserModel user)
        {
            user.UserId = 0;
            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }
}

using smileRed25.Domain;
using smileRed25.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace smileRed25.Helpers
{
    public static class Converter
    {
        public static UserLocal ToUserLocal(User user)
        {
            return new UserLocal
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                Telephone = user.Telephone,
                UserId = user.UserId,
                UserTypeId = user.TypeofUserId,
                Address = user.Address,
                Location = user.Location,
            };
        }

        public static User ToUserDomain(UserLocal user, byte[] imageArray)
        {
            return new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                Telephone = user.Telephone,
                UserId = user.UserId,
                TypeofUserId = 2,
                ImageArray = imageArray,
                Address = user.Address,
                Location = user.Location,
            };
        }     
    }
}
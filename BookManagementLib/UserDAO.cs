﻿using System;
using System.Collections.Generic;
using System.Linq;
using BookManagementLib.DataAccess;

namespace BookManagementLib
{
    class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<User> GetUserList()
        {
            var Users = new List<User>();
            try
            {
                using var context = new BookManagementDBContext();
                Users = context.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Users;
        }

        public User GetUserByEmail(string email)
        {
            User User = null;
            try
            {
                using var context = new BookManagementDBContext();
                User = context.Users.SingleOrDefault(m => m.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return User;
        }

        public bool CheckLogin(string email, string pass)
        {
            User User = null;
            try
            {
                using var context = new BookManagementDBContext();
                User = context.Users.SingleOrDefault(m => m.Email == email && m.Password == pass);
                if (User!=null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public void Insert(User User)
        {
            try
            {
                User user = GetUserByEmail(User.Email);
                if (user == null)
                {
                    using var context = new BookManagementDBContext();
                    context.Users.Add(User);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The User is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(User User)
        {
            try
            {
                User user = GetUserByEmail(User.Email);
                if (user != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Users.Update(User);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The User does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(string UserEmail)
        {
            try
            {
                User user = GetUserByEmail(UserEmail);
                if (user != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The User does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

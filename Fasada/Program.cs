using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WzorzecFasada
{

    interface IUserService
    {
        void CreateUser(string email);
        void DeleteUser(string email);
        void CountUser();
    }

    static class EmailNotification
    {
        public static void SendEmail(string to, string subject)
        {
            Console.WriteLine(subject + " " + to);
        }
        public static void SendEmailGb(string to, string subject)
        {
            Console.WriteLine(subject + " " + to);
        }
    }

    class UserRepository
    {
        public readonly List<string> users = new List<string>
        {
            "john.doe@gmail.com", "sylvester.stallone@gmail.com"
        };

        public bool IsEmailFree(string email)
        {
            foreach (string item in users)
            {
                if (item == email)
                {
                    return false;
                }

            }
            return true;
        }

        public void AddUser(string email)
        {
            users.Add(email);
        }
    }

    static class Validators
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase);
        }
    }

    class UserService : IUserService
    {
        private readonly UserRepository userRepository = new UserRepository();

        public void CountUser()
        {
            Console.WriteLine($"Aktualna liczba adresów: {userRepository.users.Count}");
        }

        public void CreateUser(string email)
        {
            if (!Validators.IsValidEmail(email))
            {
                throw new ArgumentException("Błędny email");
            }

            if (!userRepository.IsEmailFree(email))
            {
                throw new ArgumentException("Ten email jest zajęty");
            }
            userRepository.AddUser(email);
            EmailNotification.SendEmail(email, "Welcome to our service");
        }

        public void DeleteUser(string email)
        {
            if (!userRepository.IsEmailFree(email))
            {
                userRepository.users.Remove(email);
                EmailNotification.SendEmailGb(email, "Goodbye");
            }
            else
            {
                throw new ArgumentException("Nie można usunąć maila");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IUserService userService = new UserService();
            // TODO: wyświetlić liczbę
            userService.CountUser();
            userService.CreateUser("someemail@gmail.com");
            // TODO: wyświetlić liczbę
            userService.CountUser();
            // TODO: usunąć użytkownika
            userService.DeleteUser("john.doe@gmail.com");
            // TODO: wyświetlić liczbę
            userService.CountUser();
        }
    }

}
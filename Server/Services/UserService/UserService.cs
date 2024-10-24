using Microsoft.AspNetCore.Identity.Data;
using Server.Models;
using System.Collections.Generic;

namespace Server.Services.UserService
{
    public class UserService : IUserService
    {
        public User Register(User newUser)
        {
            // Logique d'inscription ici
            // ajouter l'utilisateur à la base de données
            return newUser; // Retourner l'utilisateur après inscription
        }

        public User Login(string email, string password)
        {
            // Logique de connexion ici
            //  vérifier les identifiants et retourner l'utilisateur connecté
            return new User();
        }

        public void Logout(int userId)
        {
            // Logique de déconnexion ici
            //  Invalider le token de session ou tout autre mécanisme de déconnexion
        }
    }
}
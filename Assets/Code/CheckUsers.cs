using System.Collections.Generic;
using UnityEngine;
using System;

public static class CheckUsers
{
    private static List<User> _listUser;

    public static bool CheckAdmin(string name, string password)
    {
        _listUser = SaveToJSON.Users.Users;

        var user = _listUser[0];

        if(name == "admin" && user.Password == password)
        {
            return true;
        }

        return false;
    }

    public static bool CheckUser(string name, string password)
    {
        _listUser = SaveToJSON.Users.Users;

        foreach (var user in _listUser)
        {
            if (!user.Blocked && user.Name == name && user.Password == password)
            {
                return true;
            }
        }
        return false;
    }

    public static User ReturnUser(string name, string password)
    {
        _listUser = SaveToJSON.Users.Users;

        foreach(var user in _listUser)
        {
            if(user.Name == name && user.Password == password)
            {
                return user;
            }
        }

        Debug.LogError("ÎØÈÁÎ×ÍÎÅ ÄÅÉÑÒÂÈÅ, ÎÁĞÀÒÈÒÅÑÜ Ê ÀÄÌÈÍÓ!");
        return new User();
    }

    public static bool CheckPassword(User user)
    {
        if (!user.Restrictions) 
            return true;

        bool mathSimbol = false;
        bool upper = false; //Çàãëàâíàÿ áóêâà
        bool downer = false; //Ñòğî÷íàÿ áóêâà

        string password = user.Password;

        for (int i = 0; i < password.Length; i++)
        {
            if (password[i] == '+' || password[i] == '-' || password[i] == '*' || password[i] == '/' || password[i] == '='
                || password[i] == '^' || password[i] == '>' || password[i] == '<')
            {
                mathSimbol = true;
            }

            if (Char.IsUpper(password[i]))
            {
                upper = true;
            }
            else
            {
                downer = true;
            }
        }

        if (mathSimbol && upper && downer) 
            return true;

        return false;
    }
}

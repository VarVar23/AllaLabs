using System.Collections.Generic;


public static class AddUser 
{
    private static List<User> _listUser;

    public static bool Add(string name)
    {
        _listUser = SaveToJSON.Users.Users;

        foreach (var user in _listUser)
        {
            if (user.Name == name)
            {
                return false;
            }
        }

        SaveToJSON.SaveUser(new User { Name = name });
        SaveToJSON.LoadData(Encryptor.Key);
        return true;
    }
}

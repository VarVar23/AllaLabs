using System.Collections.Generic;

[System.Serializable]
public struct User
{
    public string Name;
    public string Password;
    public bool Blocked;
    public bool Restrictions;
    public int ID;
    
}

[System.Serializable]
public class ListUser
{
    public List<User> Users = new List<User>();
}

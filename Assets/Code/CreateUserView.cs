using System.Collections.Generic;
using UnityEngine;

public static class CreateUserView 
{
    private static List<User> _listUser;

    public static void StartCreate(GameObject prefab, Transform parent)
    {
        _listUser = SaveToJSON.Users.Users;

        for(int i = 0; i < _listUser.Count; i++)
        {
            var obj = GameObject.Instantiate(prefab, parent);
            obj.GetComponent<ViewUserButton>().UserID = _listUser[i].ID;
        }
    }

    public static void CreateAddUser(string userName, GameObject prefab, Transform parent)
    {
        _listUser = SaveToJSON.Users.Users;
        var i = _listUser.Count - 1;

        var obj = GameObject.Instantiate(prefab, parent);
        obj.GetComponent<ViewUserButton>().UserID = _listUser[i].ID;
    }
}

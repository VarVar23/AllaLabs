using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class SaveToJSON
{
    public static ListUser Users;
    private static string _path;
    private static string _fileName = "SaveFile";

    public static void Start()
    {
        Users = new ListUser();
        _path = Path.Combine(Application.dataPath, _fileName);
        LoadData(Encryptor.Key);
    }

    public static void SavePassword(int userID, string password)
    {
        LoadData(Encryptor.Key);

        User user = Users.Users[userID];
        user.Password = password;
        Users.Users[userID] = user;

        string json = JsonUtility.ToJson(Users, true);
        Save(json);
    }

    public static void SaveToggleBlock(int userID, bool toggleValue)
    {
        LoadData(Encryptor.Key);

        User user = Users.Users[userID];
        user.Blocked = toggleValue;
        Users.Users[userID] = user;

        string json = JsonUtility.ToJson(Users, true);
        Save(json);
    }

    public static void SaveToggleRestrictions(int userID, bool toggleValue)
    {
        LoadData(Encryptor.Key);

        User user = Users.Users[userID];
        user.Restrictions = toggleValue;
        Users.Users[userID] = user;

        string json = JsonUtility.ToJson(Users, true);
        Save(json);
    }

    public static void SaveUser(User user)
    {
        LoadData(Encryptor.Key);
        user.ID = Users.Users.Count;
        Users.Users.Add(user);

        string json = JsonUtility.ToJson(Users, true);
        Save(json);
    }

    public static void LoadData(string key)
    {
        if(!File.Exists(_path))
        {
            Debug.Log("Файл будет создан впервые");
            CreateAdmin();
        }
        else
        {
            byte[] encryptedbytes = File.ReadAllBytes(_path);
            byte[] dycr = Encryptor.Decrypt(encryptedbytes, key);
            string js = Encoding.UTF8.GetString(dycr);
            
            Users = JsonUtility.FromJson<ListUser>(js);
            Debug.Log("Файл был создан ранее " + Users.Users.Count);
        }    
    }

    public static void CreateAdmin()
    {
        User admin = new User();
        admin.Name = "admin";
        admin.Password = "1234";
        Users.Users.Add(admin);

        string json = JsonUtility.ToJson(Users, true);
        Save(json);
    }

    public static void ResetSettings()
    {
        File.Delete(_path);
        SceneManager.LoadScene(0);
    }

    private static void Save(string json)
    {
        byte[] ar = Encoding.UTF8.GetBytes(json);
        File.WriteAllBytes(_path, Encryptor.Encrypt(ar));
    }
}

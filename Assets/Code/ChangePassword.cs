using UnityEngine;
using System;
using System.Collections.Generic;

public static class ChangePassword
{
    public static bool Change(string oldPassword, string password1, string password2, int userID)
    {
        List<User> _listUser = SaveToJSON.Users.Users;

        bool mathSimbol = false;
        bool upper = false; //��������� �����
        bool downer = false; //�������� �����

        if(oldPassword != _listUser[userID].Password)
        {
            return false;
        }

        if (password1 != password2)
        {
            Debug.Log("������ ������");
            return false;
        }
            
        
        for(int i = 0; i < password1.Length; i++)
        {
            if (password1[i] == '+' || password1[i] == '-' || password1[i] == '*' || password1[i] == '/' || password1[i] == '=' 
                || password1[i] == '^' || password1[i] == '>' || password1[i] == '<')
            {
                mathSimbol = true;
            }  

            if(Char.IsUpper(password1[i]))
            {
                upper = true;
            }
            else
            {
                downer = true;
            }
        }

        if (!_listUser[userID].Restrictions)
            Debug.Log("� ������������ ��� �����������");
        if (!mathSimbol)
            Debug.Log("��� ����. �����");
        if (!upper)
            Debug.Log("��� ���������");
        if (!downer)
            Debug.Log("��� ��������");


        if ((mathSimbol && upper && downer) || !_listUser[userID].Restrictions)
        {
            SaveToJSON.SavePassword(userID, password1);
            return true;
        }

        return false;
    }
}

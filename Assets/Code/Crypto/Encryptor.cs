using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


public static class Encryptor
{
    public const string Key = "password";
    public static byte[] Encrypt(byte[] toEncrypt)
    {
        var des = CreateDes();
        var ct = des.CreateEncryptor();
        return ct.TransformFinalBlock(toEncrypt, 0, toEncrypt.Length);
    }

    public static byte[] Decrypt(byte[] toDecrypt, string pass)
    {
        var des = CreateDes(pass);
        var ct = des.CreateDecryptor();
        return ct.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
    }

    public static byte[] Decrypt(byte[] toDecrypt)
    {
        var des = CreateDes();
        var ct = des.CreateDecryptor();
        return ct.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
    }

    private static TripleDES CreateDes()
    {
        MD5 md5 = MD5.Create();
        TripleDES des = TripleDES.Create();
        var desKey = md5.ComputeHash(Encoding.UTF8.GetBytes(Key));
        des.Key = desKey;
        des.IV = new byte[des.BlockSize / 8];
        des.Padding = PaddingMode.PKCS7;
        des.Mode = CipherMode.ECB;
        return des;
    }

    private static TripleDES CreateDes(string pass)
    {
        MD5 md5 = MD5.Create();
        TripleDES des = TripleDES.Create();
        var desKey = md5.ComputeHash(Encoding.UTF8.GetBytes(pass));
        des.Key = desKey;
        des.IV = new byte[des.BlockSize / 8];
        des.Padding = PaddingMode.PKCS7;
        des.Mode = CipherMode.ECB;
        return des;
    }
}


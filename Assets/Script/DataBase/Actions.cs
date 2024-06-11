using UnityEngine;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;

public class Actions : MonoBehaviour
{
    private static Actions instance;
    public static Actions Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Actions>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<Actions>();
                    singletonObject.name = typeof(Actions).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // passage en parametre des valeur 
    // validation de si ya pas de SQL dans les valeur du User 
    public void createUser(string eMail, string username, string password, string userType){
        string query = $@"INSERT INTO game.User (eMail, username, password, userType)
        VALUES ('{eMail}', '{username}', '{password}', '{userType}')";

        DatabaseCall.Instance.ExecuteSql(query);
    }

    public bool getUser(string username, string password){
        string query = $@"SELECT * FROM game.User WHERE username = '{username}' AND password = '{password}'";
        List<Dictionary<string, object>> resultsList = DatabaseCall.Instance.SelectData(query);
        
        // Parcourir la liste de dictionnaires
        foreach (var result in resultsList)
        {
            if (result.ContainsKey("username") && result.ContainsKey("password") &&
                result["username"].ToString() == username && result["password"].ToString() == password)
            {
                return true;
            }
        }
        return false;
    }
}

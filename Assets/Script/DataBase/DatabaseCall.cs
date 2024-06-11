using UnityEngine;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;

public class DatabaseCall : MonoBehaviour
{
    private static DatabaseCall instance;

    private string host;
    private string port;
    private string username;
    private string password;
    private string database;

    public static DatabaseCall Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DatabaseCall>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<DatabaseCall>();
                    singletonObject.name = typeof(DatabaseCall).ToString() + " (Singleton)";
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

        // Initialize credentials
        host = Crediential.credentials["host"];
        port = Crediential.credentials["port"];
        username = Crediential.credentials["username"];
        password = Crediential.credentials["password"];
        database = Crediential.credentials["database"];
    }

    public void ExecuteSql(string query)
    {
        using (var conn = new NpgsqlConnection($"Host={host};Port={port};Username={username};Password={password};Database={database}"))
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                    Debug.Log("SQL query executed successfully");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error: {ex.Message}");
            }
        }
    }

 public List<Dictionary<string, object>> SelectData(string query)
{
    List<Dictionary<string, object>> resultsList = new List<Dictionary<string, object>>();

    using (var conn = new NpgsqlConnection($"Host={host};Port={port};Username={username};Password={password};Database={database}"))
    {
        try
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> result = new Dictionary<string, object>();

                        // Ajouter chaque paire clé-valeur au dictionnaire de résultat
                        for (int i = 0; i < reader.FieldCount; i++) {
                            string key = reader.GetName(i);
                            object value = reader.GetValue(i);
                            result.Add(key, value);
                        }

                        // Ajouter le dictionnaire de résultat à la liste
                        resultsList.Add(result);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error: {ex.Message}");
        }
    }

    return resultsList;
}

}

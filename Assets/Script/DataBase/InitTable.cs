using UnityEngine;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;

public class InitTable : MonoBehaviour {

    [SerializeField] private bool resetInventory = false;
    [SerializeField] private bool resetUser = false;
    [SerializeField] private string SelectQuery = "";

    void Start() {

        string sqlInventory = @"
            DROP TABLE IF EXISTS game.Inventory;
            
            CREATE TABLE game.Inventory (
                id          SERIAL          PRIMARY KEY,
                name        VARCHAR(255)    NOT NULL,
                quantity    INT             NOT NULL DEFAULT 0
            );
        ";

        string sqlUser = @"
            DROP TABLE IF EXISTS game.User;
            
            CREATE TABLE game.User (
                id          SERIAL          PRIMARY KEY,
                eMail       VARCHAR(255)    NOT NULL UNIQUE,
                userName    VARCHAR(255)    NOT NULL UNIQUE,
                password    VARCHAR(255)    NOT NULL,
                userType    VARCHAR(255)    NOT NULL,
                createdAt   DATE            NOT NULL DEFAULT NOW(),
                updatedAt   DATE            NOT NULL DEFAULT NOW()
            );
        ";


        if (resetInventory) {
            DatabaseCall.Instance.ExecuteSql(sqlInventory);
        }

        if (resetUser) {
            DatabaseCall.Instance.ExecuteSql(sqlUser);
        }
        
        if (!string.IsNullOrEmpty(SelectQuery)) {
            List<Dictionary<string, object>> resultsList = DatabaseCall.Instance.SelectData(SelectQuery);
        
            // Parcourir la liste de dictionnaires
            foreach (var result in resultsList)
            {
                Debug.Log("userName: " + result["username"].ToString());

            }
        }
    }
}

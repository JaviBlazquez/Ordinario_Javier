using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.Common;
using Mono.Data.Sqlite;
using System.Drawing;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    /*public GameObject player;
    public GameObject[] enemies;
    private float prevTime;
    private float prevSaveTime;
    private float logInterval = 1; //En segundos
    private float logSaveInterval = 5;
    private Positions playerPos;
    private Positions enemyPos;
    public GameObject player;*/
    public static DBManager Instance { get; private set; }
    private string dbUri = "URI=file:mydb.sqlite";
    private string SQL_COUNT_ELEMNTS = "SELECT count(*) FROM Posiciones;";
    private string SQL_CREATE_POSICIONES = "CREATE TABLE IF NOT EXISTS Posiciones "
        + "(Id INTEGER UNIQUE NOT NULL PRIMARY KEY, " +
        " Name TEXT NOT NULL, TimeStamp FLOAT, x FLOAT, y FLOAT, z FLOAT);";
    private IDbConnection dbConnection;

    // Start is called before the first frame update
    void Start()
    {
       /* if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }*/

        dbConnection = OpenDatabase();
        InitializeDB(dbConnection);
        dbConnection.Close();
    }
    /*void Update()
    {
        float currentTime = Time.realtimeSinceStartup;
        if (currentTime > prevTime + logInterval)
        {
            prevTime += logInterval;
            CharacterPosition cp = new CharacterPosition(player.name, currentTime, player.transform.position);
            playerPos.positions.Add(cp);
            foreach (GameObject enemy in enemies)
            {
                CharacterPosition en = new CharacterPosition(enemy.name, currentTime, enemy.transform.position);
                enemyPos.positions.Add(en);
            }
        }
    } */

    private IDbConnection OpenDatabase()
    {
        dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
        Debug.Log("Open");
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "PRAGMA foreign_keys = ON";
        dbCommand.ExecuteNonQuery();
        return dbConnection;
    }

    private void InitializeDB(IDbConnection dbConnection)
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = SQL_CREATE_POSICIONES;
        dbCmd.ExecuteReader();
    }

   /* public void SavePosition(CharacterPosition position)
    {
        string command = "INSERT INTO Posiciones (Name,TimeStamp,x,y,z) VALUES";
        foreach (CharacterPosition cp in playerPos.positions)
        {
            command += $"('{cp.name}'{};
        }
        foreach (CharacterPosition cp in enemyPos.positions)
        {
            data += cp.ToCSV() + "\n";
        }
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = command;
        dbCommand.ExecuteNonQuery();
    }*/

    private void OnDestroy()
    {
        dbConnection.Close();
    }
}

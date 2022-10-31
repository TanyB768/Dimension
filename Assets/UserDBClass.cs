using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDBClass
{
    public string username;
    public int scoreDB;

    public UserDBClass()
    {
    }

    public UserDBClass(string username, int score)
    {
        this.username = username;
        this.scoreDB = score;
    }
}

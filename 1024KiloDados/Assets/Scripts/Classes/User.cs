using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User {
    public string login;
    public string user_password;
    public string nick;
    public DateTime join_date;
    public DateTime last_login;
    public int active;
    public int banned;
    public int money_spent;
}

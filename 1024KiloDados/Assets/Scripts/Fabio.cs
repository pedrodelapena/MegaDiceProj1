using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fabio : MonoBehaviour {

    public static Fabio god;
    public REST rest;

    public static User user;

    public int tradeid1, tradeid2;
    public User tradeUser;

    public Character Equipchar;
    public bool equipArmor;


    

    private void Awake()
    {
        if (god == null)
        {
            god = this;
        }

        DontDestroyOnLoad(god);
    }

    private void Start()
    {
        /*
        User dummy = new User();
        dummy.login = "Dummyasa2";
        dummy.user_password = "123123321";
        dummy.nick = "XxXDUMMYKILLER2";
        dummy.join_date = System.DateTime.Today;
        dummy.last_login = System.DateTime.Today;
        dummy.active = 1;
        dummy.banned = 0;
        dummy.money_spent = 100;

        rest.AddUser(dummy);
        */
    }


    //Public field
    public static int GetAsyncId()
    {
        return god.rest.GetAsyncId();
    }

    public static bool CheckAsyncId(int id)
    {
        return !god.rest.asyncQueue[id];
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    //Rotinas
    IEnumerator Test()
    {
        print("Requested");
        yield return new WaitUntil(() => rest.user != null);
        user = rest.user;
        if(user == null)
        {
            Debug.LogError("USER IS NULL");
        }
        print(user.login);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterHandler : MonoBehaviour {

    public InputField passInput, loginInput, nickInput;

    public void SignUp()
    {
        StartCoroutine(SignUpRoutine());
    }


    IEnumerator SignUpRoutine()
    {
        string username = loginInput.text;
        string password = passInput.text;
        string nick = nickInput.text;

        //checar se user ja existe
        int asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetUserByUsername(username, asyncId);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        User user = Fabio.god.rest.user;
        if (user != null)
        {
            print("Username taken");
            //TRATAR
        } else
        {
            user = new User();
            user.login = username;
            user.user_password = password;
            user.nick = nick;
            user.join_date = System.DateTime.Today;
            user.last_login = System.DateTime.Today;
            user.active = 1;
            user.banned = 0;
            user.money_spent = 0;

            Fabio.god.rest.AddUser(user);
            Fabio.LoadScene("Login");
        }

    }
}

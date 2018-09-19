using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour {

    public InputField userInput, passInput;

    public void SignIn()
    {

        StartCoroutine(SignInRoutine());
    }


    IEnumerator SignInRoutine()
    {
        string username = userInput.text;
        string password = passInput.text;

        int asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetUserByUsername(username, asyncId);
        print("signing " + username);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        print("fetched");

        User user = Fabio.god.rest.user;
        if (user == null)
        {
            print("usuario n existe");
            //Tratar
        } else
        {
            if (CheckPassword(user.user_password, password)) {

                if (user.banned != 0)
                {
                    print("Usuario banido!");
                } else
                {
                    Fabio.god.rest.UpdateUserLastLogin(username);
                    print("updating login: " + username);
                    print("fetched");
                    print("LOGIN FOI");
                    Fabio.user = user;
                    Fabio.LoadScene("MainMenu");
                }
            }
            else
            {
                print("Senha invalida");
                //Tratar
            }
        }
    }

    public void signUp()
    {
        Fabio.LoadScene("Register");
    }

    bool CheckPassword(string userHash, string password)
    {
        return (userHash == password); //fazer um hash no futuro e colocar ele no fabio pra ser facil de salvar
    }
}

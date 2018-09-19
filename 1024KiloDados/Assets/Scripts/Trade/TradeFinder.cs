using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeFinder : MonoBehaviour {

    public InputField searchBar;

    public void Action()
    {
        StartCoroutine(ActionRoutine());
    }

    IEnumerator ActionRoutine()
    {
        User originalUser = Fabio.god.rest.user;

        int asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetUserByUsername(searchBar.text, asyncId);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        print("fetched");
        Fabio.god.tradeUser = Fabio.god.rest.user;
        Fabio.god.rest.user = originalUser;

        if (Fabio.god.tradeUser == null)
        {
            print("User not found");
        } else
        {
            Fabio.LoadScene("Trade2");
        }
    }

}

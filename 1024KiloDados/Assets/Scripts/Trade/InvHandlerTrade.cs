using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvHandlerTrade : MonoBehaviour {

    public GameObject[] slots;

    public Item[] items;

    Template currentTemplate;

    public bool trading;

    private void Start()
    {
        trading = false;
        StartCoroutine(GetItems());   
    }

    public void Delete(int id)
    {
        print("stop");
    }

    public void Trade(int id)
    {
        if (!trading)
        {
            trading = true;
            Fabio.god.tradeid2 = slots[id].GetComponent<InvSlot>().myItem.item_id;
            Fabio.god.rest.TradeItem(Fabio.god.tradeid1, Fabio.god.tradeid2, Fabio.user.login, Fabio.god.tradeUser.login);
            Fabio.LoadScene("MainMenu");
        }
    }

    //Routines
    IEnumerator GetItems()
    {
        //get items
        User oldUser = Fabio.user;
        Fabio.user = Fabio.god.tradeUser;

        int asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetItemsFromUser(asyncId);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        print("fetched");
        items = Fabio.god.rest.userItems;
        print(items.Length);

        Fabio.user = oldUser;

        //set inv screen
        for (int i = 0; i < items.Length && i < 15; i++)
        {

            asyncId = Fabio.GetAsyncId();
            Fabio.god.rest.GetTemplateById(items[i].template_id, asyncId);
            yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
            print("fetched");
            currentTemplate = Fabio.god.rest.template;

            print(items[i].template_id);
            slots[i].SetActive(true);
            slots[i].GetComponent<InvSlot>().Fill(items[i],currentTemplate);
            
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvHandler : MonoBehaviour {

    public GameObject[] slots;

    public Item[] items;

    Template currentTemplate;

    private void Start()
    {
        StartCoroutine(GetItems());   
    }

    public void Delete(int id)
    {
        StartCoroutine(DeleteItemRoutine(id));
    }

    public void Trade(int id)
    {
        Fabio.god.tradeid1 = slots[id].GetComponent<InvSlot>().myItem.item_id;
        Fabio.LoadScene("Trade1");
    }

    //Routines
    IEnumerator GetItems()
    {
        //get items
        int asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetItemsFromUser(asyncId);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        print("fetched");
        items = Fabio.god.rest.userItems;
        print(items.Length);

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

    IEnumerator DeleteItemRoutine(int id)
    {
        print("deleting" + slots[id].GetComponent<InvSlot>().myItem.item_id);
        Fabio.god.rest.DeleteItem(slots[id].GetComponent<InvSlot>().myItem.item_id);
        yield return new WaitForSeconds(0.1f);
        Fabio.LoadScene("Inv");
    }
}

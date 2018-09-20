using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvHandlerEquip : MonoBehaviour {

    public GameObject[] slots;

    public Item[] items;

    Template currentTemplate;

    public bool equiping;

    private void Start()
    {
        equiping = false;
        StartCoroutine(GetItems());   
    }

    public void Delete(int id)
    {
        print("stop");
    }

    public void Equip(int id)
    {
        if (!equiping)
        {
            equiping = true;
            int newitemid = slots[id].GetComponent<InvSlot>().myItem.item_id;
            Fabio.god.rest.EquipItem(newitemid,Fabio.god.Equipchar,Fabio.god.equipArmor);
            Fabio.LoadScene("MainMenu");
        }
    }

    //Routines
    IEnumerator GetItems()
    {
        int asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetFreeEquipFromUser(asyncId);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        print("fetched");
        items = Fabio.god.rest.userItems;
        Debug.LogWarning(items.Length);

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

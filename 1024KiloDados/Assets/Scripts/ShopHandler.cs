using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour {

    public GameObject[] slots;

    Template currentTemplate;
    public Template[] allTemplates;

    private void Start()
    {
        StartCoroutine(GenerateShop());
    }

    public void Buy(int id)
    {
        int templateId = slots[id].GetComponent<ShopCell>().myTemplate.template_id;
        Fabio.god.rest.AddItem(templateId);
        slots[id].GetComponent<ShopCell>().Sell();
    }


    IEnumerator GenerateShop()
    {
        //Get all templates
        int asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetTemplates(asyncId);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        print("fetched");
        allTemplates = Fabio.god.rest.templates;

        //assign
        for(int i = 0; i < 15; i++)
        {
            currentTemplate = allTemplates[Random.Range(0, allTemplates.Length)];
            slots[i].GetComponent<ShopCell>().Fill(currentTemplate);
        }
    }
}

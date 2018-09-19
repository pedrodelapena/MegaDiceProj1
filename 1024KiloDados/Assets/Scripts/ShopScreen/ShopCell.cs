using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour {

    public Template myTemplate;

    public Text[] elements;

    public GameObject soldout;

    public void Fill(Template template)
    {
        myTemplate = template;

        elements[0].text = template.item_name;
        elements[1].text = template.rarity.ToString();
        elements[2].text = template.item_type;
        elements[3].text = template.amount_1.ToString();
        elements[4].text = (template.rarity * 100).ToString();
    }

    public void Sell()
    {
        soldout.SetActive(true);
    }
}

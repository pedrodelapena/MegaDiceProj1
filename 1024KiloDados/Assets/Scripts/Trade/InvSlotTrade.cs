using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvSlotTrade : MonoBehaviour {

    public Item myItem;
    Template myTemplate;

    public Text[] elements;

    public void Fill(Item item, Template template)
    {
        myItem = item;
        myTemplate = template;
        elements[0].text = myTemplate.item_name;
        elements[1].text = myTemplate.rarity.ToString();
        elements[2].text = myTemplate.item_type;
        elements[3].text = myTemplate.amount_1.ToString();
    }


}

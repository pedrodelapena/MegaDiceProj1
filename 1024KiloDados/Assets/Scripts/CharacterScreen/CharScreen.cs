using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharScreen : MonoBehaviour {

    public Text[] elements;
    public int charid;
    public Dropdown ddWeapons, ddArmor;

    public CharHandler handler;

    public void FillElements(Character character)
    {
        charid = character.character_id;
        elements[0].text = "Clone N°" + character.character_id;
        elements[1].text = character.strenght.ToString();
        elements[2].text = character.vigor.ToString();
        elements[3].text = character.inteligence.ToString();
        elements[4].text = character.wisdom.ToString();
        elements[5].text = character.charisma.ToString();
        elements[6].text = character.dexterity.ToString();

        //StartCoroutine(main());

    }

    
    IEnumerator main()
    {
        yield return new WaitUntil(() => handler.flag_weapons);

        Debug.LogWarning(handler.weapons.Length);
        List<string> opts = new List<string>();
        foreach (Item i in handler.weapons)
        {
            opts.Add(i.item_id.ToString());
            Debug.LogWarning(i.item_id.ToString());
        }
        ddWeapons.AddOptions(opts);
    }
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharScreen : MonoBehaviour {

    public Text[] elements;
    public int charid;
    public Dropdown ddWeapons, ddArmor;
    public Character myCharacter;

    public Item armor, weapon;
    public Template armorTemplate, weaponTemplate;

    public CharHandler handler;
    public float delay;

    public void FillElements(Character character)
    {
        myCharacter = character;
        charid = character.character_id;
        elements[0].text = "Clone N°" + character.character_id;
        elements[1].text = character.strenght.ToString();
        elements[2].text = character.vigor.ToString();
        elements[3].text = character.inteligence.ToString();
        elements[4].text = character.wisdom.ToString();
        elements[5].text = character.charisma.ToString();
        elements[6].text = character.dexterity.ToString();

        StartCoroutine(main());

    }

    
    IEnumerator main()
    {
        //Get a little delay to avoid async problems
        yield return new WaitForSeconds(delay);
        //Get armor item
        print("armor" + myCharacter.armor);
        int asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetItemsById(myCharacter.armor, asyncId);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        print("fetched");
        armor = Fabio.god.rest.item;
        print("armor = " + armor);

        //Get weapon item
        asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetItemsById(myCharacter.weapon, asyncId);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        print("fetched");
        weapon = Fabio.god.rest.item;

        print("!!!");
        //print(armor.item_id);
        //print(weapon.item_id);
        print("!!!");

        //Get armor template
        if (armor != null)
        {
            asyncId = Fabio.GetAsyncId();
            Fabio.god.rest.GetTemplateById(armor.template_id, asyncId);
            yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
            print("fetched");
            armorTemplate = Fabio.god.rest.template;
        } else
        {
            armorTemplate = null;
        }

        //Get weapon template
        if (weapon != null)
        {
            asyncId = Fabio.GetAsyncId();
            Fabio.god.rest.GetTemplateById(weapon.template_id, asyncId);
            yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
            print("fetched");
            weaponTemplate = Fabio.god.rest.template;
        }
        else
        {
            weaponTemplate = null;
        }

        //Show info
        elements[7].text = armorTemplate == null ? "0" : armorTemplate.amount_1.ToString();
        elements[8].text = weaponTemplate == null ? "0" : weaponTemplate.amount_1.ToString();
        elements[9].text = armorTemplate == null ? "none" : armorTemplate.item_name;
        elements[10].text = weaponTemplate == null ? "none" : weaponTemplate.item_name;



    }
	
}

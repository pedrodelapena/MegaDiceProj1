﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharHandler : MonoBehaviour {

    public GameObject[] charactersScreens;

    public GameObject[] buttons;

    public Character[] characters;

    public Item[] armors, weapons;

    public bool flag_weapons;


    private void Start()
    {
        flag_weapons = false;

        foreach (GameObject obj in charactersScreens)
        {
            obj.SetActive(false);
        }
        StartCoroutine(CharHandlerRoutine());   
    }

    public void CreateCharacter()
    {
        print("creating a new char");
        Character newCharacter = new Character();
        newCharacter.login = Fabio.user.login;
        newCharacter.dexterity = Random.Range(0, 10);
        newCharacter.strenght = Random.Range(0, 10);
        newCharacter.vigor = Random.Range(0, 10);
        newCharacter.inteligence = Random.Range(0, 10);
        newCharacter.wisdom = Random.Range(0, 10);
        newCharacter.charisma = Random.Range(0, 10);

        StartCoroutine(CreateCharacter(newCharacter));
    }

    public void DeleteCharacter(int id)
    {
        print("deleting char");
        Character character= charactersScreens[id].GetComponent<CharScreen>().myCharacter;
        StartCoroutine(DeleteCharacterRoutine(character));
    }

    public void EquipArmor(int id)
    {
        Character character = charactersScreens[id].GetComponent<CharScreen>().myCharacter;
        Fabio.god.Equipchar = character;
        Fabio.god.equipArmor = true;
        Fabio.LoadScene("EquipItem");
    }

    public void EquipWeapon(int id)
    {
        Character character = charactersScreens[id].GetComponent<CharScreen>().myCharacter;
        Fabio.god.Equipchar = character;
        Fabio.god.equipArmor = false;
        Fabio.LoadScene("EquipItem");
    }

    //Routines
    IEnumerator CharHandlerRoutine()
    {
        //Get characters
        int asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetCharactersFromUser(asyncId);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        characters = Fabio.god.rest.userCharacters;

        //Get items
        asyncId = Fabio.GetAsyncId();
        Fabio.god.rest.GetWeaponsFromUser(asyncId);
        yield return new WaitUntil(() => Fabio.CheckAsyncId(asyncId));
        weapons = Fabio.god.rest.userWeapons;

        flag_weapons = true;

        //fill screens
        for (int i = 0; i < characters.Length && i < 3; i++)
        {
            charactersScreens[i].SetActive(true);
            charactersScreens[i].GetComponent<CharScreen>().FillElements(characters[i]);
        }

        if(characters.Length < 3)
        {
            buttons[characters.Length].SetActive(true);
        }
       
    }

    IEnumerator CreateCharacter(Character newCharacter)
    {
        Fabio.god.rest.AddCharacter(newCharacter);
        yield return new WaitForSeconds(0.1f); //wait a little then reload to show new char
        Fabio.LoadScene("Characters");
    }

    IEnumerator DeleteCharacterRoutine(Character character)
    {
        if(character.armor != null)
        {
            StartCoroutine(Fabio.god.rest.ChangeItemStatus(character.armor, 0));
        }
        if (character.weapon != null)
        {
            StartCoroutine(Fabio.god.rest.ChangeItemStatus(character.weapon, 0));
        }

        Fabio.god.rest.DeleteCharacter(character.character_id);

        yield return new WaitForSeconds(0.1f); //wait a little then reload to show new spot
        Fabio.LoadScene("Characters");
    }
}

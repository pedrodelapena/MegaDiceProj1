using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class REST : MonoBehaviour {

    //Async Queue
    public bool[] asyncQueue = new bool[64];

    public int GetAsyncId()
    {
        for (int i = 0; i < 64; i++)
        {
            if (!asyncQueue[i])
            {
                asyncQueue[i] = true;
                return i;
            }
        }
        Debug.LogError("ERROR: async Queue was Full");
        return -1;
    }

    //Get All Users
    public User[] allUsers;
    public void GetUsers()
    {
        StartCoroutine(GetUsersRoutine());
    }

    IEnumerator GetUsersRoutine()
    {
        string restUrl = "http://localhost:8001/api/users";
        UnityWebRequest req = UnityWebRequest.Get(restUrl);

        req.chunkedTransfer = false;

        yield return req.SendWebRequest();

        if(req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            if (req.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                //print(jsonResult);
                allUsers = JsonHelper.GetJsonArray<User>(jsonResult);
                print(allUsers.Length);
                
            }
        }

        print(req.ToString());
        yield return null;
    }

    //Get All Users END

    //Get User By Username
    public User user;
    public void GetUserByUsername(string username,int asyncId)
    {
        StartCoroutine(GetUserByUsernameRoutine(username, asyncId));
    }

    IEnumerator GetUserByUsernameRoutine(string username,int asyncId)
    {
        string restUrl = "http://localhost:8001/api/user/" + username;
        UnityWebRequest req = UnityWebRequest.Get(restUrl);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();
   
        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            if (req.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                //print(jsonResult);
                User[] result = JsonHelper.GetJsonArray<User>(jsonResult);
                if(result.Length > 0)
                {
                    user = result[0];
                } else
                {
                    user = null;
                }
                asyncQueue[asyncId] = false;
            }
        }

        print(req.ToString());
        yield return null;
    }
    //Get User By Username END

    //AddUser 
    public void AddUser(User newUser)
    {
        StartCoroutine(AddUserRoutine(newUser));
    }

    IEnumerator AddUserRoutine(User newUser)
    {

        //Stringizachions
        WWWForm form = new WWWForm();
        /*
        form.AddField("name", newUser.login);
        form.AddField("password", newUser.user_password);
        form.AddField("nick", newUser.nick);
        print(newUser.join_date.ToString());
        form.AddField("joindate", newUser.join_date.ToString());
        form.AddField("lastlogin", newUser.last_login.ToString());
        form.AddField("active", newUser.active);
        form.AddField("ban", newUser.banned);
        form.AddField("money", newUser.money_spent);
        */

        string restUrl = "http://localhost:8001/api/newuser/" + newUser.login +"/"+ newUser.user_password + "/" 
            + newUser.nick + "/" + newUser.join_date.ToString("yyyy-MM-dd") + "/" 
            + newUser.last_login.ToString("yyyy-MM-dd") + "/" + newUser.active + "/" + newUser.banned + "/" + newUser.money_spent;

        print(restUrl);
        UnityWebRequest req = UnityWebRequest.Post(restUrl,form);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }

        yield return null;
    }

    //update user last login
    public void UpdateUserLastLogin(string username)
    {
        StartCoroutine(UpdateUserLastLoginRoutine(username));
    }

    IEnumerator UpdateUserLastLoginRoutine(string username)
    {
        ///userlogin/:username/:date
        string restUrl = "http://localhost:8001/api/userlogin/" + username + "/" + System.DateTime.Today.ToString("yyyy-MM-dd");
        print(restUrl);
        WWWForm dummy = new WWWForm();
        UnityWebRequest req = UnityWebRequest.Post(restUrl, dummy);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }

        print(req.ToString());
        yield return null;
    }
    //update user last login END

    //Get characters from username
    public Character[] userCharacters;
    public void GetCharactersFromUser(int asyncId)
    {
        StartCoroutine(GetCharactersFromUserRoutine(asyncId));
    }

    IEnumerator GetCharactersFromUserRoutine(int asyncId)
    {
        string restUrl = "http://localhost:8001/api/user/" + Fabio.user.login + "/characters";
        UnityWebRequest req = UnityWebRequest.Get(restUrl);

        req.chunkedTransfer = false;

        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            if (req.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                userCharacters = JsonHelper.GetJsonArray<Character>(jsonResult);
                asyncQueue[asyncId] = false;

            }
        }

        print(req.ToString());
        yield return null;
    }
    //Get characters from username END

    //Add Character
    public void AddCharacter(Character newCharacter)
    {
        StartCoroutine(AddCharacterRoutine(newCharacter));
    }

    IEnumerator AddCharacterRoutine(Character newCharacter)
    {
        //Stringizachions
        WWWForm form = new WWWForm();

        ///newcharacter/:ownername/:str/:dex/:vig/:int/:wis/:cha
        string restUrl = "http://localhost:8001/api/newcharacter/" + user.login + "/" + newCharacter.strenght + "/" + newCharacter.dexterity + "/" + newCharacter.vigor + "/" + newCharacter.inteligence + "/" + newCharacter.wisdom + "/" + newCharacter.charisma ;

        print(restUrl);
        UnityWebRequest req = UnityWebRequest.Post(restUrl, form);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
    }
    //Add Character END

    //Delete Character
    public void DeleteCharacter(int id)
    {
        StartCoroutine(DeleteCharacterRoutine(id));
    }

    IEnumerator DeleteCharacterRoutine(int id)
    {

        //Stringizachions

        ///deletecharacter/:id
        string restUrl = "http://localhost:8001/api/deletecharacter/" + id;

        print(restUrl);
        UnityWebRequest req = UnityWebRequest.Delete(restUrl);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
    }
    //Delete Character END

    //Get Items from user
    public Item[] userItems;
    public void GetItemsFromUser(int asyncId)
    {
        StartCoroutine(GetItemsFromUserRoutine(asyncId));
    }

    IEnumerator GetItemsFromUserRoutine(int asyncId)
    {
        string restUrl = "http://localhost:8001/api/item/" + Fabio.user.login;
        UnityWebRequest req = UnityWebRequest.Get(restUrl);

        req.chunkedTransfer = false;

        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            if (req.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                userItems = JsonHelper.GetJsonArray<Item>(jsonResult);
                asyncQueue[asyncId] = false;

                foreach(Item i in userItems)
                {
                    print(i.item_id);
                }

            }
        }

        print(req.ToString());
        yield return null;
    }

    //Get Items from user END

    //Get weapons from current user
    //Get Items from user
    public Item[] userWeapons;
    public void GetWeaponsFromUser(int asyncId)
    {
        StartCoroutine(GetWeaponsFromUserRoutine(asyncId));
    }

    IEnumerator GetWeaponsFromUserRoutine(int asyncId)
    {
        string restUrl = "http://localhost:8001/api/item/" + Fabio.user.login + "/weapons";
        UnityWebRequest req = UnityWebRequest.Get(restUrl);

        req.chunkedTransfer = false;

        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            if (req.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                userWeapons = JsonHelper.GetJsonArray<Item>(jsonResult);
                asyncQueue[asyncId] = false;

                foreach (Item i in userWeapons)
                {
                    print(i.item_id);
                }

            }
        }

        print(req.ToString());
        yield return null;
    }
    //Get weapons from current user END

    //Delete item
    public void DeleteItem(int id)
    {
        StartCoroutine(DeleteItemRoutine(id));
    }

    IEnumerator DeleteItemRoutine(int id)
    {

        //Stringizachions

        ///deletecharacter/:id
        string restUrl = "http://localhost:8001/api/deleteitem/" + id;

        print("((((((" + restUrl);
        UnityWebRequest req = UnityWebRequest.Delete(restUrl);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
    }
    //Delete item END

    //Get all templates
    public Template[] templates;
    public void GetTemplates(int asyncId)
    {
        StartCoroutine(GetTemplatesRoutine(asyncId));
    }

    IEnumerator GetTemplatesRoutine(int asyncId)
    {
        string restUrl = "http://localhost:8001/api/templates/";
        UnityWebRequest req = UnityWebRequest.Get(restUrl);

        req.chunkedTransfer = false;

        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            if (req.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                templates = JsonHelper.GetJsonArray<Template>(jsonResult);
                asyncQueue[asyncId] = false;

                foreach (Template i in templates)
                {
                    print(i.item_name);
                }

            }
        }

        print(req.ToString());
        yield return null;
    }

    //Get all templates END

    //Get all templates
    public Template template;
    public void GetTemplateById(int id,int asyncId)
    {
        StartCoroutine(GetTemplateByIdRoutine(id,asyncId));
    }

    IEnumerator GetTemplateByIdRoutine(int id, int asyncId)
    {
        string restUrl = "http://localhost:8001/api/template/" + id;
        UnityWebRequest req = UnityWebRequest.Get(restUrl);
        print("a" + restUrl);
        req.chunkedTransfer = false;

        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
        else
        {
            if (req.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                template = JsonHelper.GetJsonArray<Template>(jsonResult)[0];
                asyncQueue[asyncId] = false;

            }
        }

        print(req.ToString());
        yield return null;
    }

    //Get all templates END

    //Add item to user
    public void AddItem(int id)
    {
        StartCoroutine(AddItemRoutine(id));
    }

    IEnumerator AddItemRoutine(int id)
    {
        //Stringizachions
        WWWForm form = new WWWForm();

        ///newcharacter/:ownername/:str/:dex/:vig/:int/:wis/:cha
        string restUrl = "http://localhost:8001/api/newitem/" + id + "/" + Fabio.user.login + "/" + System.DateTime.Today.ToString("yyyy-MM-dd");

        print(restUrl);
        UnityWebRequest req = UnityWebRequest.Post(restUrl, form);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
    }
    //Add item to user END



    //Trade Item
    bool flag_error = false;
    bool flag_continue;
    public void TradeItem(int id1,int id2, string login1, string login2)
    {
        flag_error = false;
        flag_continue = true;
        StartCoroutine(TradeItemRoutine(id1, id2, login1, login2));
    }

    IEnumerator TradeItemRoutine(int id1, int id2, string login1, string login2)
    {
        StartCoroutine(Start_Transaction());
        flag_continue = false;
        yield return new WaitUntil(() => flag_continue);
        if (flag_error)
        {
            StartCoroutine(Rollback());
        } else
        {
            StartCoroutine(ChangeOwner(id1,login2));
            flag_continue = false;
            yield return new WaitUntil(() => flag_continue);
            if (flag_error)
            {
               StartCoroutine(Rollback());
            } else
            {
                StartCoroutine(ChangeOwner(id2, login1));
                flag_continue = false;
                yield return new WaitUntil(() => flag_continue);
                if (flag_error)
                {
                    StartCoroutine(Rollback());
                } else
                {
                    StartCoroutine(Commit());
                }
            }
        }

    }
    //Trade Item END

    //Functions for Trade
    IEnumerator Start_Transaction()
    {
        //Stringizachions
        WWWForm form = new WWWForm();

        ///newcharacter/:ownername/:str/:dex/:vig/:int/:wis/:cha
        string restUrl = "http://localhost:8001/api/trade/start/";

        print(restUrl);
        UnityWebRequest req = UnityWebRequest.Post(restUrl, form);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            flag_error = true;
            Debug.Log(req.error);
        }
        flag_continue = true;
    }

    IEnumerator Rollback()
    {
        //Stringizachions
        WWWForm form = new WWWForm();

        ///newcharacter/:ownername/:str/:dex/:vig/:int/:wis/:cha
        string restUrl = "http://localhost:8001/api/trade/rollback/";

        print(restUrl);
        UnityWebRequest req = UnityWebRequest.Post(restUrl, form);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            Debug.Log(req.error);
        }
    }

    IEnumerator Commit()
    {
        //Stringizachions
        WWWForm form = new WWWForm();

        ///newcharacter/:ownername/:str/:dex/:vig/:int/:wis/:cha
        string restUrl = "http://localhost:8001/api/trade/commit/";

        print(restUrl);
        UnityWebRequest req = UnityWebRequest.Post(restUrl, form);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            flag_error = true;
            Debug.Log(req.error);
        }

        flag_continue = true;
    }

    IEnumerator ChangeOwner(int id, string login)
    {
        //Stringizachions
        WWWForm form = new WWWForm();

        ///newcharacter/:ownername/:str/:dex/:vig/:int/:wis/:cha
        string restUrl = "http://localhost:8001/api/trade/" + id.ToString() + "/" + login;

        print(restUrl);
        UnityWebRequest req = UnityWebRequest.Post(restUrl, form);

        req.chunkedTransfer = false;
        yield return req.SendWebRequest();

        if (req.isHttpError || req.isNetworkError)
        {
            flag_error = true;
            Debug.Log(req.error);
        }
        flag_continue = true;
    }
    //Functions for Trade END



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonHelper {

    public static T[] GetJsonArray<T>(string json)
    {
        //string newJson = "{ \"array\": " + json + "}";
        Debug.Log(json);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.result;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] result;
    }

}

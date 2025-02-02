using NUnit.Framework;
using PersonalizedOffersSdk.Tool;
using System.Collections.Generic;
using UnityEngine;

namespace PersonalizedOffersSdk.Tool
{
    public static class JsonHelper
    {
        public static List<T> FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(List<T> items)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = items;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(List<T> items, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = items;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public List<T> Items;
        }
    }
}
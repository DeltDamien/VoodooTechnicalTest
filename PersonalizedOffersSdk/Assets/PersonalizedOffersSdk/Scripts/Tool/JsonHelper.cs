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
            WrapperContainer<T> wrapper = JsonUtility.FromJson<WrapperContainer<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(List<T> items)
        {
            WrapperContainer<T> wrapper = new WrapperContainer<T>();
            wrapper.Items = items;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(List<T> items, bool prettyPrint)
        {
            WrapperContainer<T> wrapper = new WrapperContainer<T>();
            wrapper.Items = items;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class WrapperContainer<T>
        {
            public List<T> Items;
        }
    }
}
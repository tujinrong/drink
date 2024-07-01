using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using System.Web.Mvc.Properties;
using System.Web.Script.Serialization;

namespace DrinkService.Controllers.Common
{
    public sealed class DyJsonValueProviderFactory : ValueProviderFactory
    {
        private static void AddToBackingStore(EntryLimitedDictionary backingStore, string prefix, object value)
        {
            IDictionary<string, object> dictionary = value as IDictionary<string, object>;
            if (dictionary != null)
            {
                foreach (KeyValuePair<string, object> pair in dictionary)
                {
                    string introduced5 = MakePropertyKey(prefix, pair.Key);
                    AddToBackingStore(backingStore, introduced5, pair.Value);
                }
            }
            else
            {
                IList list = value as IList;
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        AddToBackingStore(backingStore, MakeArrayKey(prefix, i), list[i]);
                    }
                }
                else
                {
                    backingStore.Add(prefix, value);
                }
            }
        }

        private static object GetDeserializedObject(ControllerContext controllerContext)
        {
            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            string str = new StreamReader(controllerContext.HttpContext.Request.InputStream).ReadToEnd();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.DeserializeObject(str);
        }

        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            object deserializedObject = GetDeserializedObject(controllerContext);
            if (deserializedObject == null)
            {
                return null;
            }
            Dictionary<string, object> innerDictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            EntryLimitedDictionary backingStore = new EntryLimitedDictionary(innerDictionary);
            AddToBackingStore(backingStore, string.Empty, deserializedObject);
            return new DictionaryValueProvider<object>(innerDictionary, CultureInfo.CurrentCulture);
        }

        private static string MakeArrayKey(string prefix, int index)
        {
            return (prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]");
        }

        private static string MakePropertyKey(string prefix, string propertyName)
        {
            if (!string.IsNullOrEmpty(prefix))
            {
                return (prefix + "." + propertyName);
            }
            return propertyName;
        }

        private class EntryLimitedDictionary
        {
            private readonly IDictionary<string, object> _innerDictionary;
            private int _itemCount;
            private static int _maximumDepth = GetMaximumDepth();

            public EntryLimitedDictionary(IDictionary<string, object> innerDictionary)
            {
                this._innerDictionary = innerDictionary;
            }

            public void Add(string key, object value)
            {
                if (++this._itemCount > _maximumDepth)
                {
                    //throw new InvalidOperationException(MvcResources.JsonValueProviderFactory_RequestTooLarge);
                }
                this._innerDictionary.Add(key, value);
            }

            private static int GetMaximumDepth()
            {
                NameValueCollection appSettings = ConfigurationManager.AppSettings;
                if (appSettings != null)
                {
                    int num;
                    string[] values = appSettings.GetValues("aspnet:MaxJsonDeserializerMembers");
                    if (((values != null) && (values.Length > 0)) && int.TryParse(values[0], out num))
                    {
                        return num;
                    }
                }
                return 0x3e8;
            }
        }
    }
}
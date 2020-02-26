using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Localization
{
    public static class Localize
    {
        private static readonly ConcurrentDictionary<string, string> _dictionary = new ConcurrentDictionary<string, string>();

        public static string Translate(string key, string defaultTranslate)
        {
            if (_dictionary.TryGetValue(key, out string value))
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return defaultTranslate;
        }

        public static void Load(string path, string language)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return;
            }


            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                var translations = XElement.Load(fileStream);
                var selectedTranslation = translations.Descendants("Language")
                                            .Where(l => (string)l.Attribute("lang") == language)
                                            .FirstOrDefault();

                if (selectedTranslation != null)
                {
                    foreach (var item in selectedTranslation.Elements("Translate").ToList())
                    {
                        _dictionary.TryAdd(item.Attribute("key").Value, item.Value);
                    }
                }
            }
        }

    }
}

using System;

namespace Lab.Utility.MyCustomAttribute
{
    /// <summary>
    /// Attrubute for values used in resouces such as DB and a xml file.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ResourceValueAttribute : Attribute
    {
        public ResourceValueAttribute(string value) { Value = value; }
        public string Value { get; set; }
    }
}

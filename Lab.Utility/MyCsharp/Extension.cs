using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab.Utility.Csharp
{
	public static class Extension
	{
		public static T DeepClone<T>(this T obj)
		{
			using (var ms = new MemoryStream())
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(ms, obj);
				ms.Position = 0;

				return (T)formatter.Deserialize(ms);
			}
		}
        
        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }
    }
    public enum Season
    {
        [Display(Name = "It's autumn")]
        Autumn,

        [Display(Name = "It's winter")]
        Winter,

        [Display(Name = "It's spring")]
        Spring,

        [Display(Name = "It's summer")]
        Summer
    }
    public class Foo
    {
        public Season Season = Season.Summer;

        public void DisplayName()
        {
            var seasonDisplayName = Season.GetAttribute<DisplayAttribute>();
            Console.WriteLine("Which season is it?");
            Console.WriteLine(seasonDisplayName.Name);
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class Display : Attribute
    {
        public Display() { }
        Display(string Name) { Name = name; }
        public string Name { get; set;}
    }
}

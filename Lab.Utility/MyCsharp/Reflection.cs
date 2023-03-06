using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility.CsharpExperiment
{
	public class Reflection
	{
		public static void CompareWithGenericType<T>()
		{
			var sampleA = new SampleClassA();
			var sampleB = new SampleClassA();
			var answer = sampleA.GetType() == typeof(T);
			Console.WriteLine(answer);
		}
	}

	public class SampleClassA : ISample
	{
		
	}
	public class SampleClassB : ISample
	{

	}

	public interface ISample
	{
		
	}
}

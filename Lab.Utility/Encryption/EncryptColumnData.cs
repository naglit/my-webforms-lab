using Lab.Utility.Configuration;
using System;
using System.Collections;
using System.Linq;


namespace Lab.Utility.Encryption
{
    public class EncryptColumnData
    {
		/// <summary>
		/// Encrypt
		/// </summary>
		/// <param name="queriedTable"></param>
		/// <param name="input"></param>
		/// <remarks>
		/// The type of arguments should be Hashtable.
		/// That is because it'd be bad performance,
		/// if the sets of the arguments are 2 string type values for table and column names.
		/// It'll take as much loops as the number of columns.
		/// Otherwise, passing input as Hashtable, the loop count will be 
		/// the number of SensitiveDataColumnsSetting.GetInstance.SensitiveDataColumns.Values.
		/// </remarks>
		public static void Encrypt(string queriedTable, Hashtable input)
        {
			// I'm supposed to deep copy the argument, "input."
			// But I don't here because i haven't created a function for it.
			var deepCopiedInput = new Hashtable();

			var tables = SensitiveDataColumnsSetting.GetInstance
				.SensitiveDataColumns.Keys.ToArray();

			// Check if the queried table has any sensitive data column.
			var tableWhichContainsAnySensitiveDataColumn = tables.FirstOrDefault(t => (t == queriedTable));
			if (string.IsNullOrEmpty(tableWhichContainsAnySensitiveDataColumn)) return;

			// if yes, encrypt values of columns which has sensitive data.
			var columns = SensitiveDataColumnsSetting.GetInstance
				.SensitiveDataColumns[tableWhichContainsAnySensitiveDataColumn];
			foreach(var column in columns)
            {
				// Check if the column needs to be encrypted
				var isColumnWhichContainsSensitiveData = input.ContainsValue(column);
				if (isColumnWhichContainsSensitiveData) continue;

				// Query the IV from DB
				var iv = Convert.FromBase64String("aaaaa");

				// Put the cipher text into the deep copied input
				deepCopiedInput[column] =  Encryption.Encrypt((string)input[column], "11111");
			}
		}

		public static int GetRequiredSizeOfColumnValueForEncryptedDataStoring(int plainTextLength)
        {
			var size = 37 + (plainTextLength / 16 + 1) * 16;
				return size;
		}

		public static bool IsSensitiveDataColumn(string tableName, string columnName)
		{
			var isTableWhichContainsSensitiveDataColumn = SensitiveDataColumnsSetting.GetInstance
				.SensitiveDataColumns.ContainsKey(tableName);
			if (isTableWhichContainsSensitiveDataColumn) return false;
			
			var isColumnWhichContainsSensitiveData = SensitiveDataColumnsSetting.GetInstance
				.SensitiveDataColumns[tableName]
					.Any(col => (col == columnName));
			return isColumnWhichContainsSensitiveData;
		}
	}
}
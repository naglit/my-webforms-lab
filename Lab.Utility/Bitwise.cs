using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility
{
	public enum DesignCategory
	{
		DesignCategoryA = 1 << 0,
		DesignCategoryB = 1 << 1,
		DesignCategoryC = 1 << 2,
		DesignCategoryD = 1 << 3
	}
	public class Bitwise
	{
		const int _designCategoryA = 1 << 0;
		const int _designCategoryB = 1 << 1;
		const int _designCategoryC = 1 << 2;
		const int _designCategoryD = 1 << 3;

		public void Try()
		{
			// 0110
			var designCategoryAvailable = DesignCategory.DesignCategoryA | DesignCategory.DesignCategoryC;

			var isAvailableToSetDesignCategoryA = ((designCategoryAvailable & DesignCategory.DesignCategoryA) > 0);
			var isAvailableToSetDesignCategoryB = ((designCategoryAvailable & DesignCategory.DesignCategoryB) > 0);
			var isAvailableToSetDesignCategoryC = ((designCategoryAvailable & DesignCategory.DesignCategoryC) > 0);
			var isAvailableToSetDesignCategoryD = ((designCategoryAvailable & DesignCategory.DesignCategoryD) > 0);
		}
	}
}

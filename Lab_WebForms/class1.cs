

namespace Model
{
	/// <summary>
	/// 接客サービス用商品情報拡張モデル
	/// </summary>
	public class Product
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Product()
		{
		}
		public int ProductId { get; set; }
		/// <summary>ブランド名</summary>
		public string ProductName {get;set;}
		/// <summary>JANコード</summary>
		public int Price { get; set; }
		public bool IsAvailable { get; set; }
	}
}

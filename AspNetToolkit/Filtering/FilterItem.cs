namespace AspNetToolkit.Filtering {
	public class FilterItem {
		public string Name { get; set; }
		public string Label { get; set; }
		public FilterOperator Operator { get; set; }
		public object Value { get; set; }
	}
}

namespace Backender.Translator
{
	public class Block
	{
		public string Type { get; set; }
		public string Value { get; set; }
		public List<string> Options { get; set; } = new List<string>();
		public List<Block> InnerBlocks { get; set; } = new List<Block>();
	}
}
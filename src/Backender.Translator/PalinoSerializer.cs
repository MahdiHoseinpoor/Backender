namespace Backender.Translator
{
	public static class PalinoSerializer
	{
		public static Block BlockBuilder(string Block)
		{
			var block = new Block();
			var blockLines = Block.Split("\n").Where(p => p != string.Empty).ToList();

			var blockInfo = blockLines[0].Trim().Split(' ').ToList();
			block.Type = blockInfo[0];
			blockInfo.Remove(blockInfo[0]);
			if (blockInfo.Any())
			{
				block.Value = blockInfo[0];
				blockInfo.Remove(blockInfo[0]);
			}

			block.Options = blockInfo;

			if (blockLines.Count > 1)
			{
				for (int i = 1; i < blockLines.Count; i++)
				{
					var innerBlock = BlockBuilder(blockLines[i]);
					block.InnerBlocks.Add(innerBlock);
				}
			}
			return block;
		}
		public static Config Deserialize(string palino)
		{
			List<Block> Blocks = ExtractBlocks(palino);
			var config = new Config();
			config.Version = Blocks.FirstOrDefault(p=>p.Type == "Version").Value;
			config.SavePath = Blocks.FirstOrDefault(p=>p.Type == "SavePath").Value;
			config.SoltionNameSpace = Blocks.FirstOrDefault(p=>p.Type == "NameSpace").Value;
			config.SolutionName = Blocks.FirstOrDefault(p=>p.Type == "Solution").Value;
			var domains = new Domains();
			var entityBlocks = Blocks.Where(p=>p.Type=="Entity");
			var EnumBlocks = Blocks.Where(p=>p.Type=="Enum");
			var realationsBlock = Blocks.FirstOrDefault(p=>p.Type== "Realations");
			domains.Entites = new List<Entity>();
			foreach (var entityBlock in entityBlocks)
			{
				var entity = new Entity();
				entity.EntityName = entityBlock.Value;
				foreach (var entityInnerBlock in entityBlock.InnerBlocks)
				{
					var col = new Col()
					{
						ColName = entityInnerBlock.Type,
						ColType=entityInnerBlock.Value,
						Options=string.Join(' ',entityInnerBlock.Options)
					};
					entity.Cols.Add(col);
				}
				domains.Entites.Add(entity);
			}
			foreach (var realationblock in realationsBlock.InnerBlocks)
			{
				var realtion = new RealationShip()
				{
					Entity1 = realationblock.Value.Split('-')[0],
					Entity2 = realationblock.Value.Split('-')[1],
					RealationShipType = realationblock.Type
				};
				domains.RealationShips.Add(realtion);
			}

			foreach (var EnumBlock in EnumBlocks)
			{
				var enum_ = new Enum_();
				enum_.EnumName = EnumBlock.Value;

				foreach (var enumValue in EnumBlock.InnerBlocks)
				{
					var enumValue_ = new EnumValue_()
					{
						Name = enumValue.Type,
						Value =Convert.ToInt32(enumValue.Value)
					};
					enum_.EnumValues.Add(enumValue_);
				}
				domains.Enums.Add(enum_);
			}

			config.Domains = domains;

			return config;
		}

		private static List<Block> ExtractBlocks(string palino)
		{
			var BlocksCode = new List<string>();
			BlocksCode = palino.Replace("\r", "").Split('$').Where(p => p != string.Empty).ToList();
			var Blocks = new List<Block>();
			foreach (var Blockcode in BlocksCode)
			{
				if (Blockcode != string.Empty)
				{
					var block = BlockBuilder(Blockcode);
					Blocks.Add(block);
				}
			}

			return Blocks;
		}
	}
}
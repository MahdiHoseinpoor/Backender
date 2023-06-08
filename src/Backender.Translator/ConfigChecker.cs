using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator
{
	public static class ConfigChecker
	{
		public static List<Message> Messages { get; private set; } = new List<Message>();
		public static List<Message> GetMessages()
		{
			return Messages.OrderBy(p => p.MessageType).ToList();
		}
		
		public static void Run(Config config)
		{
			var Messages = new List<Message>();
			SolutionValidation(config, Messages);
			EntityValidation(config, Messages);
			EnumValidation(config, Messages);
			relationShipsValidation(config, Messages);
			ConfigChecker.Messages.AddRange(Messages);
			
		}

		private static void relationShipsValidation(Config config, List<Message> Messages)
		{
			foreach (var relationship in config.Domains.RelationShips)
			{	
				if (string.IsNullOrEmpty(relationship.RelationShipType))
				{
					Messages.Add(new Message("BA001", MessageType.Error, $"RelationShipType in relationShips can't be null"));
				}
				if (string.IsNullOrEmpty(relationship.Entity1))
				{
					Messages.Add(new Message("BA001", MessageType.Error, $"Entity1 in relationShips can't be null"));
				}
				if (string.IsNullOrEmpty(relationship.Entity2))
				{
					Messages.Add(new Message("BA001", MessageType.Error, $"Entity2 in relationShips can't be null"));
				}

				if (relationship.RelationShipType != "O2M" && relationship.RelationShipType != "M2M" && relationship.RelationShipType != "O2P")
				{
					Messages.Add(new Message("BA005", MessageType.Error, $"'{relationship.RelationShipType}' is not a relationType"));
				}
				if (!config.Domains.Entites.Any(p => p.EntityName == relationship.Entity1) && !string.IsNullOrEmpty(relationship.Entity1))
				{
					Messages.Add(new Message("BA006", MessageType.Error, $"'{relationship.Entity1}' which you defined in the Entity1 of the relation, does not exist"));
				}
				if (!config.Domains.Entites.Any(p => p.EntityName == relationship.Entity2) && !string.IsNullOrEmpty(relationship.Entity2))
				{
					Messages.Add(new Message("BA006", MessageType.Error, $"'{relationship.Entity2}' which you defined in the Entity2 of the relation, does not exist"));
				}
			}
		}
		private static void EntityValidation(Config config, List<Message> Messages)
		{
			using (CSharpCodeProvider codeProvider = new CSharpCodeProvider())
			{
				foreach (var Entity in config.Domains.Entites)
				{
					if (string.IsNullOrEmpty(Entity.EntityName))
					{
						Messages.Add(new Message("BA001", MessageType.Error, $"EntityName in Entities can't be null"));
					}
					if (!codeProvider.IsValidIdentifier(Entity.EntityName))
					{
						Messages.Add(new Message("BA002", MessageType.Error, $"EntityName:{Entity.EntityName} is an identifier"));
					}
					foreach (var item in Entity.Cols.GroupBy(p => p.ColName).Where(p => p.Count() > 1))
					{
						Messages.Add(new Message("BA003", MessageType.Error, $"A ColName in '{Entity.EntityName}' entity named '{item.FirstOrDefault().ColName}' is already defined in this scope"));
					}
					foreach (var EntityCols in Entity.Cols)
					{
						if (string.IsNullOrEmpty(EntityCols.ColName))
						{
							Messages.Add(new Message("BA001", MessageType.Error, $"ColName in Cols can't be null"));
						}
						if (string.IsNullOrEmpty(EntityCols.ColType))
						{
							Messages.Add(new Message("BA001", MessageType.Error, $"ColType in Cols can't be null"));
						}
						if (!codeProvider.IsValidIdentifier(EntityCols.ColName))
						{
							Messages.Add(new Message("BA002", MessageType.Error, $"ColName:{EntityCols.ColName} in {Entity.EntityName} entity is an identifier"));
						}
					}
				}
			}
			foreach (var item in config.Domains.Entites.GroupBy(p => p.EntityName).Where(p => p.Count() > 1))
			{
				Messages.Add(new Message("BA003", MessageType.Error, $"A EntityName named '{item.FirstOrDefault().EntityName}' is already defined in this scope"));
			}
		}
		private static void SolutionValidation(Config config, List<Message> Messages)
		{
			if (string.IsNullOrEmpty(config.SolutionName))
			{
				Messages.Add(new Message("BA001", MessageType.Error, $"SolutionName Can't Be null"));
			}
			if (string.IsNullOrEmpty(config.SavePath))
			{
				Messages.Add(new Message("BA901", MessageType.Warning, $"The SavePath is null, your Solution will Create in 'Documents/Backender 2023/Sources'"));
			}
		}
		private static void EnumValidation(Config config, List<Message> Messages)
		{
			using (CSharpCodeProvider codeProvider = new CSharpCodeProvider())
			{
				foreach (var Enum in config.Domains.Enums)
				{
					if (string.IsNullOrEmpty(Enum.EnumName))
					{
						Messages.Add(new Message("BA001", MessageType.Error, $"EnumName in Enums can't be null"));
					}
					foreach (var item in Enum.EnumValues.GroupBy(p => p.Name).Where(p => p.Count() > 1))
					{
						Messages.Add(new Message("BA003", MessageType.Error, $"A EnumName in '{Enum.EnumName}' enum named '{item.FirstOrDefault().Name}' is already defined in this scope"));
					}
					foreach (var item in Enum.EnumValues.GroupBy(p => p.Value).Where(p => p.Count() > 1))
					{
						Messages.Add(new Message("BA004", MessageType.Error, $"'{item.FirstOrDefault().Value}' in '{Enum.EnumName}' enum used for '{item.Count()}' items"));
					}
					if (!codeProvider.IsValidIdentifier(Enum.EnumName))
					{
						Messages.Add(new Message("BA002", MessageType.Error, $"EnumName:{Enum.EnumName} is an identifier"));
					}
				}
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator
{
	public static class ConfigChecker
	{
		public static List<Message> Run(Config config)
		{
			var Messages = new List<Message>();
			RealationShipsValidation(config, Messages);
			SolutionValidation(config, Messages);
			return Messages.OrderBy(p=>p.MessageType).ToList();
		}

		private static void RealationShipsValidation(Config config, List<Message> Messages)
		{
			foreach (var RealationShip in config.Domains.RealationShips)
			{
				if (string.IsNullOrEmpty(RealationShip.RealationShipType))
				{
					Messages.Add(new Message("", MessageType.Error, $"RealationShipType in RealationShips can't be null"));
				}
				if (string.IsNullOrEmpty(RealationShip.Entity1))
				{
					Messages.Add(new Message("", MessageType.Error, $"Entity1 in RealationShips can't be null"));
				}
				if (string.IsNullOrEmpty(RealationShip.Entity2))
				{
					Messages.Add(new Message("", MessageType.Error, $"Entity2 in RealationShips can't be null"));
				}

				if (RealationShip.RealationShipType != "O2M" && RealationShip.RealationShipType != "M2M" && RealationShip.RealationShipType != "O2P")
				{
					Messages.Add(new Message("", MessageType.Error, $"'{RealationShip.RealationShipType}' is not a RealationType"));
				}
				if (!config.Domains.Entites.Any(p=>p.EntityName == RealationShip.Entity1) && !string.IsNullOrEmpty(RealationShip.Entity1))
				{
					Messages.Add(new Message("", MessageType.Error, $"'{RealationShip.Entity1}' which you defined in the Entity1 of the relation, does not exist"));
				}
				if (!config.Domains.Entites.Any(p => p.EntityName == RealationShip.Entity2) &&  !string.IsNullOrEmpty(RealationShip.Entity2))
				{
					Messages.Add(new Message("", MessageType.Error, $"'{RealationShip.Entity2}' which you defined in the Entity2 of the relation, does not exist"));
				}
			}
		}
		private static void SolutionValidation(Config config, List<Message> Messages)
		{
			if (string.IsNullOrEmpty(config.SolutionName))
			{
				Messages.Add(new Message("", MessageType.Error, $"SolutionName Can't Be null"));
			}
			if (string.IsNullOrEmpty(config.SavePath))
			{
				Messages.Add(new Message("", MessageType.Warning, $"The SavePath is null, your Solution will Create in 'Documents/Backender 2023/Sources'"));
			}


		}
	}
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RHTools.Randomizer.Rules;
using RHTools.Randomizer.Test.Utils;
using RHTools.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer.Test
{
	[TestClass]
	public class RandomizerTest
	{
		[TestMethod]
		public void RandomizeWithNoRules()
		{
			var settings = GetSettings(new List<Rule>(), new List<Rule>());
			var randomizer = new RhcRandomizer(settings);
			randomizer.Randomize();
		}

		[TestMethod]
		public void RandomizeWithRules()
		{
			var noteRules = new List<Rule>()
			{
				new ForceStartingColumnForFootRule(Foot.Left, 0),
				new ForceStartingColumnForFootRule(Foot.Right, 2),
				new MaxRepetitionsEveryXNotesRule(0, 1),
				new MaxRepetitionsEveryXNotesRule(1, 2),
				new DisableColumnForFootRule(Foot.Left, 2),
				new DisableColumnForFootRule(Foot.Right, 0),
				new MaxSpanRule(2, 0),
				new DisableAdjacentRowsRule(),
				//new MaxDistancePerStepRule(1, 1)
			};

			var mineRules = new List<Rule>()
			{
				new DisableNotesAndAdjacentRowsForLastXFeetRule(2)
			};

			var settings = GetSettings(noteRules, mineRules);

			//var rhcFiles = new List<string>()
			//{
			//	"5506dd22-69ed-4c36-8a80-abbafc05bfdb",
			//	"409fba50-fb01-41df-bfa5-2a399f3de856",
			//	"24e0375d-d9bd-4be9-a98d-e2f281bf4886",
			//	"062a6c08-6152-4706-b3bd-bbea6b7ff757",
			//	"cce473c7-93b9-470b-abbd-007af0dc5fab",
			//	"3a6bd710-2e11-4cc1-8895-72c33397707d",
			//	"6492f546-71ce-48ec-95ca-6a0d726f44e7",
			//	"9cfff546-a39d-40ab-8702-c609ab97c019",
			//	"b81e744e-b8a8-49c1-8b7a-1dd8286682b5",
			//	"243834e5-7424-4244-9c8f-3b7a8ab2e772",
			//	"6ab0b8bb-168e-4de6-b881-5ecc0ded4f02",
			//	"01874219-d818-49bd-9f3b-647e111484b3",
			//	"78c29d65-e811-4cd4-8519-60921fbfd3d5",
			//	"ea8aa06c-6577-47a5-91ee-f46a1fc8d51d",
			//	"6d8ee106-bff6-4b77-b741-cb1ad7cc694f",
			//	"d7f7a45f-a113-42d9-9f56-e1269ae38e86",
			//	"7e8e414c-6f2a-42d3-8f6c-353a728c5162",
			//	"532bd9bd-e62c-4045-8f06-9c1885d83354",
			//	"cc34639f-ede4-4a06-b520-3dbbf9ee2189",
			//	"d7c9d3fc-8e62-42ef-98d0-26ddca866247",
			//	"baecea4a-022c-400f-bd87-cad7e0269d7e",
			//	"ee4d3e41-a213-4a55-a40b-1ace95487110",
			//	"be8fd8ac-88b0-41e7-86fa-02e1c35d3181",
			//	"f6bf2971-fdfc-4bdd-b944-11fc3bcc70d8",
			//	"f38611e9-0d95-41e3-86d6-44bf7627cc68",
			//	"ac9c3b48-c02b-4acd-976a-5c86be77c401",
			//	"26065425-8b7b-4f74-a832-052c40c60a3b",
			//	"f27735ec-1a8c-4dab-b61e-32d229615443",
			//	"192b1b2b-1b9e-49ca-8e1f-e060acd3ac99",
			//	"ed39c54b-d1ff-47ae-b7f8-07cc0fa5b12d",
			//	"92d8e528-6e76-4292-b747-ac6bdcddb353",
			//	"f0e668c1-a777-454b-b2fc-cc8308ddd0e5",
			//	"e44b1212-3247-48b5-b9b1-f4c51c5dc0e7",
			//	"21f7e876-97e4-498f-93f3-dbfb9bab15a1",
			//	"7f423e08-beb7-4c56-8ab8-c160b4797542",
			//	"279be7ae-f5ec-4c33-9fc7-88e32aa6e1f2",
			//	"10ae1f77-fdf6-4ade-be83-a213f213fe17",
			//	"f6249b22-3964-421f-92bb-b54abe1c38b8",
			//	"9e4ce686-c91e-4fa0-b18c-9300036abdd8",
			//	"d911922b-412c-4bce-be8a-d692f6f970b6",
			//	"72ed2319-afb9-4b9f-b9e5-3767e452b935",
			//	"75ad7257-578c-4e74-b7ef-9a7300a7c3a5",
			//	"827ff1d8-5cfd-415e-b0dc-cd39e56fb5db",
			//	"d8f91996-78a5-4c1c-bcd9-5e777b913934"
			//};

			//foreach (var file in rhcFiles)
			//{
			//	settings.rhPath = $@"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\{file}.rhc";
			//	var randomizer = new RhcRandomizer(settings);
			//	randomizer.Randomize();
			//}

			var randomizer = new RhcRandomizer(settings);
			randomizer.Randomize();
		}

		private RandomizerSettings GetSettings(List<Rule> noteRules, List<Rule> mineRules)
		{
			var settings = new RandomizerSettings();

			//var panelConfig = new bool[,]
			//{
			//	{ true, false, true, false, false, false },
			//	{ true, false, true, false, false, false },
			//	{ true, false, true, false, false, false }
			//};
			//settings.panelConfig = panelConfig;

			settings.rhPath = @"C:\Users\Tyler\AppData\Roaming\Rhythm Horizon\GameData\1c4823c6-f77c-4a20-9c5c-4877f5e80799.rhc";
			settings.panelConfig = RandomizerTestUtil.config9Panel;
			settings.random = new Random();
			settings.noteRules = noteRules;
			settings.mineRules = mineRules;
			settings.disableJumps = false;
			settings.disableMines = false;
			settings.disableHolds = false;

			return settings;
		}
	}
}

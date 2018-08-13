using RHTools.Randomizer.Conditions;
using RHTools.Randomizer.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Randomizer
{
	public class RuleGroup
	{
		public List<Rule> rules;
		public List<Condition> conditions;

		public RuleGroup()
		{
			rules = new List<Rule>();
			conditions = new List<Condition>();
		}

		public RuleGroup(List<Rule> rules, List<Condition> conditions)
		{
			this.rules = rules;
			this.conditions = conditions;
		}
	}
}

﻿using System;
using DotNetRuleEngine.Core;
using DotNetRuleEngine.Core.Interface;
using DotNetRuleEngine.Core.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.Rules
{
    class ProductRule : Rule<Product>
    {
        public override void Initialize()
        {
            AddRules(new ProductReactiveRule());
        }

        public override void BeforeInvoke()
        {
            TryAdd("Key", "Value");
        }

        public override IRuleResult Invoke()
        {
            Model.Description = "Product Description";
            TryAdd("Ticks", DateTime.Now.Ticks);
            return new RuleResult { Name = "ProductRule", Result = Model.Description, Data = { { "Key", TryGetValue("Key") }, { "Ticks", TryGetValue("Ticks") } } };
        }
    }
}
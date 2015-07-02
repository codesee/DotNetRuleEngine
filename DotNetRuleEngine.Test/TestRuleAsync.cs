﻿using System.Linq;
using DotNetRuleEngine.Core;
using DotNetRuleEngine.Test.AsyncRules;
using DotNetRuleEngine.Test.Models;
using DotNetRuleEngine.Test.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetRuleEngine.Test
{
    [TestClass]
    public class TestRuleAsync
    {
        [TestMethod]
        public void TestInvokeAsync()
        {
            var ruleEngineExecutor = new RuleEngineExecutor<Product>(new Product());
            ruleEngineExecutor.AddRules(new ProductRuleAsync());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.AreEqual("Product Description", ruleResults.First().Result);
        }

        [TestMethod]
        public void TestBeforeInvokeAsync()
        {
            var ruleEngineExecutor = new RuleEngineExecutor<Product>(new Product());
            ruleEngineExecutor.AddRules(new ProductRuleAsync());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;

            object value;
            ruleResults.First().Data.TryGetValue("Key", out value);
            Assert.AreEqual("Value", value);
        }

        [TestMethod]
        public void TestAfterInvokeAsync()
        {
            var ruleEngineExecutor = new RuleEngineExecutor<Product>(new Product());
            ruleEngineExecutor.AddRules(new ProductTerminateAsyncA(), new ProductTerminateAsyncB());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.AreEqual(1, ruleResults.Length);
        }

        [TestMethod]
        public void TestSkipAsync()
        {
            var ruleEngineExecutor = new RuleEngineExecutor<Product>(new Product());
            ruleEngineExecutor.AddRules(new ProductSkipAsync());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.IsFalse(ruleResults.Any());
        }


        [TestMethod]
        public void TestTerminateAsync()
        {
            var ruleEngineExecutor = new RuleEngineExecutor<Product>(new Product());
            ruleEngineExecutor.AddRules(new ProductTerminateAsyncA(), new ProductTerminateAsyncB());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.AreEqual(1, ruleResults.Length);
        }


        [TestMethod]
        public void TestConstraintAsync()
        {
            var ruleEngineExecutor = new RuleEngineExecutor<Product>(new Product());
            ruleEngineExecutor.AddRules(new ProductConstraintAsyncA(), new ProductConstraintAsyncB());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.AreEqual(1, ruleResults.Length);
        }


        [TestMethod]
        public void TestTryAddTryGetValueAsync()
        {
            var ruleEngineExecutor = new RuleEngineExecutor<Product>(new Product());
            ruleEngineExecutor.AddRules(new ProductTryAddAsync(), new ProductTryGetValueAsync());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.AreEqual("Product Description", ruleResults.First().Result);
        }
    }
}

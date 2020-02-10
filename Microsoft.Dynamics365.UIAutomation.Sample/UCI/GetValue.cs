﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using System;

namespace Microsoft.Dynamics365.UIAutomation.Sample.UCI
{
    [TestClass]
    public class GetValueUci : TestsBase
    {
        [TestInitialize]
        public override void InitTest() => base.InitTest();

        [TestCleanup]
        public override void FinishTest() => base.FinishTest();

        public override void NavigateToHomePage() => NavigateTo(UCIAppName.Sales);

        [TestMethod]
        public void UCITestGetValueFromOptionSet()
        {
            _xrmApp.Navigation.OpenSubArea("Contacts");

            _xrmApp.Grid.OpenRecord(0);

            string option = _xrmApp.Entity.GetValue(new OptionSet {Name = "preferredcontactmethodcode"});
            Assert.IsNotNull(option);
        }

        [TestMethod]
        public void UCITestGetValueFromLookup()
        {
            _xrmApp.Navigation.OpenSubArea("Accounts");

            _xrmApp.Grid.OpenRecord(0);

            _xrmApp.ThinkTime(2000);
            string lookupValue = _xrmApp.Entity.GetValue(new LookupItem {Name = "primarycontactid"});
            Assert.IsNotNull(lookupValue);
        }

        [TestMethod]
        public void UCITestActivityPartyGetValue()
        {
            _xrmApp.Navigation.OpenSubArea("Activities");

            _xrmApp.Grid.SwitchView("All Phone Calls");
            _xrmApp.ThinkTime(500);

            _xrmApp.Grid.OpenRecord(0);
            _xrmApp.ThinkTime(500);

            var to = _xrmApp.Entity.GetValue(new [] {new LookupItem {Name = "to"}});
            Assert.IsNotNull(to);
            _xrmApp.ThinkTime(500);
        }

        [TestMethod]
        public void UCITestGetValueFromDateTime()
        {
            _xrmApp.Navigation.OpenSubArea("Opportunities");
            _xrmApp.ThinkTime(500);

            _xrmApp.CommandBar.ClickCommand("New");

            _xrmApp.Entity.SetValue("name", "Test EasyRepro Opportunity");

            var dateTime = DateTime.Today.AddHours(10).AddMinutes(15);
            _xrmApp.Entity.SetHeaderValue("estimatedclosedate", dateTime);
            _xrmApp.ThinkTime(500);

            var estimatedclosedate = _xrmApp.Entity.GetHeaderValue(new DateTimeControl("estimatedclosedate"));
            Assert.AreEqual(dateTime, estimatedclosedate);
            _xrmApp.ThinkTime(500);
        }
    }
}
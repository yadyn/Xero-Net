﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Model;

namespace CoreTests.Integration.ManualJournals
{
    [TestFixture]
    public class Create : ManualJournalsTest
    {
        [Test]
        public async Task create_manual_journal()
        {
            await ManualJournalsSetUp();

            const string narration = "We know what we want to do";
            const int amount = 50;

            var manual = await Given_a_manual_journal(narration, amount);

            Assert.AreEqual(DateTime.Now.Date, manual.Date);
            Assert.AreEqual(narration, manual.Narration);
            Assert.True(manual.Lines.All(p => amount == Math.Abs(p.Amount)));
            Assert.NotNull(manual.Lines.Single(p => Revenue.Code == p.AccountCode));
            Assert.NotNull(manual.Lines.Single(p => Sales.Code == p.AccountCode));
        }

        [Test]
        public async Task create_complex_manual_journal()
        {
            await ManualJournalsSetUp();

            const string narration = "We know what we want to do";

            var manual = await Api.CreateAsync(new ManualJournal
            {
                Narration = narration,
                Lines = new List<Line>
                {
                    new Line
                    {
                        Amount = 12.5m,
                        AccountCode = Revenue.Code
                    },
                    new Line
                    {
                        Amount = 25,
                        AccountCode = Revenue.Code
                    },
                    new Line
                    {
                        Amount = 12.5m,
                        AccountCode = Sales.Code
                    },
                    new Line
                    {
                        Amount = -50m,
                        AccountCode = Sales.Code
                    }
                }
            });

            Assert.AreEqual(0, manual.Lines.Sum(p => p.Amount));
            Assert.AreEqual(DateTime.Now.Date, manual.Date);
            Assert.AreEqual(narration, manual.Narration);
        }        
    }
}

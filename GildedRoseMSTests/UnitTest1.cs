using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GildedRose.Console;

namespace GildedRoseMSTests
{
    [TestClass]
    public class GildedRoseTests
    {
        private Program app;
        private Item oldIron = new Item { Name = "Any Old Iron", Quality = 10, SellIn = 10 };
        private Item sulfuras = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
        private Item agedBrie = new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 };
        private Item upperQualityLimit = new Item { Name = "Upper Quality Limit", Quality = 50, SellIn = 10 };
        private Item lowerQualityLimit = new Item { Name = "Lower Quality Limit", Quality = 1, SellIn = 10 };
        private Item backstagePass = new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  };
        private Item backstagePass2 = new Item
        {
            Name = "Backstage passes to a Pearl Jam concert",
            SellIn = 15,
            Quality = 20
        };

        private Item conjuredItem = new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 };


        [TestInitialize]
        public void TestInitialize()
        {
            app = new Program();
            Program.Items.Clear();
        }

        [TestMethod]
        public void TestQualityLimitsForRegularItem()
        {
            Program.Items.Add(this.oldIron);
            app.UpdateQuality();

            Assert.IsTrue(Program.Items[0].Quality <= 50);
            Assert.IsTrue(Program.Items[0].Quality >= 0);

            app.UpdateQualityWithNumberOfDays(100);

            Assert.IsTrue(Program.Items[0].Quality <= 50);
            Assert.IsTrue(Program.Items[0].Quality >= 0);
        }

        [TestMethod]
        public void TestRegularItem()
        {
            Program.Items.Add(this.oldIron);

            app.UpdateQuality();

            Assert.AreEqual(9, Program.Items[0].SellIn);
            Assert.AreEqual(9, Program.Items[0].Quality);
            
        }

        [TestMethod]
        public void TestBrieItem()
        {
            Program.Items.Add(this.agedBrie);
            app.UpdateQuality();

            Assert.AreEqual(1, Program.Items[0].SellIn);
            Assert.AreEqual(1, Program.Items[0].Quality);

            int daysPassed = 100;
            int currentSellIn = Program.Items[0].SellIn;
            int expectedSellIn = currentSellIn - daysPassed;

            app.UpdateQualityWithNumberOfDays(daysPassed);

            Assert.AreEqual(50, Program.Items[0].Quality);
            Assert.AreEqual(expectedSellIn, Program.Items[0].SellIn);
        }

        [TestMethod]
        public void TestSulfurasItem()
        {
            Program.Items.Add(this.sulfuras);
            app.UpdateQualityWithNumberOfDays();

            Assert.AreEqual(80, Program.Items[0].Quality);
            Assert.AreEqual(0, Program.Items[0].SellIn);
        }

        [TestMethod]
        public void TestBackstagePassItem()
        {
            Program.Items.Add(this.backstagePass);
            Program.Items.Add(this.backstagePass2);

            app.UpdateQuality();

            foreach (Item item in Program.Items)
            {
                Assert.AreEqual(14, item.SellIn);
                Assert.AreEqual(21, item.Quality);
            }

            app.UpdateQualityWithNumberOfDays(4);

            foreach (Item item in Program.Items)
            {
                Assert.AreEqual(10, item.SellIn);
                Assert.AreEqual(25, item.Quality);
            }

            app.UpdateQuality();

            foreach (Item item in Program.Items)
            {
                Assert.AreEqual(9, item.SellIn);
                Assert.AreEqual(27, item.Quality);
            }

            app.UpdateQualityWithNumberOfDays(4);

            foreach (Item item in Program.Items)
            {
                Assert.AreEqual(5, item.SellIn);
                Assert.AreEqual(35, item.Quality);
            }

            app.UpdateQuality();

            foreach (Item item in Program.Items)
            {
                Assert.AreEqual(4, item.SellIn);
                Assert.AreEqual(38, item.Quality);
            }

            app.UpdateQualityWithNumberOfDays(5);

            foreach (Item item in Program.Items)
            {
                Assert.AreEqual(0, item.Quality);
            }
        }

        [TestMethod]
        public void TestConjuredItem() 
        {
            Program.Items.Add(this.conjuredItem);
            app.UpdateQuality();

            Assert.AreEqual(2, Program.Items[0].SellIn);
            Assert.AreEqual(4, Program.Items[0].Quality);
        }
    }
}

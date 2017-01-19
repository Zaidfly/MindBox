using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Journey.Tests
{
    [TestClass]
    public class JourneyTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SortNullJourney()
        {
            // Arrange
            var journey = new Journey();
            List<TicketCard> unsortedCards = null;

            // Act
            var actual = journey.SortCards(unsortedCards);

            // Assert
        }

        [TestMethod]
        public void SortEmptyListJourney()
        {
            // Arrange
            var journey = new Journey();
            var unsortedCards = new TicketCard[0];
            
            // Act
            var actual = journey.SortCards(unsortedCards);

            // Assert
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void SortSingleCardJourney()
        {
            // Arrange
            var journey = new Journey();
            var unsortedCards = new [] {new TicketCard("Москва","Париж") };

            // Act
            var actual = journey.SortCards(unsortedCards).ToArray();

            // Assert
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(unsortedCards[0], actual[0]);
        }

        [TestMethod]
        public void SortPrimitiveJourney()
        {
            // Arrange
            var journey = new Journey();
            var unsortedCards = new[]
            {
                new TicketCard("Мельбурн", "Кельн"),
                new TicketCard("Москва","Париж"),
                new TicketCard("Лондон", "Майами"),
                new TicketCard("Париж", "Лондон"),
                new TicketCard("Кельн", "Москва")
            };
            var expected = new[]
            {
                new TicketCard("Мельбурн", "Кельн"),
                new TicketCard("Кельн", "Москва"),
                new TicketCard("Москва","Париж"),
                new TicketCard("Париж", "Лондон"),
                new TicketCard("Лондон", "Майами")
            };

            // Act
            var actual = journey.SortCards(unsortedCards).ToArray();

            // Assert
            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void SortLongRandomJourney()
        {
            // Arrange
            var journey = new Journey();
            var rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const int cardsCount = 100*1000;
            const int nameLength = 10;

            var expected = new TicketCard[cardsCount];

            var rndCity1 = new string(Enumerable.Range(0, nameLength).Select(s => chars[rnd.Next(chars.Length)]).ToArray());
            var uniqueNameChecker = new HashSet<string> { rndCity1 };
            for (var i = 0; i < cardsCount; i++)
            {
                var rndCity2 = new string(Enumerable.Range(0, nameLength).Select(s => chars[rnd.Next(chars.Length)]).ToArray());
                while (!uniqueNameChecker.Add(rndCity2))
                {
                    rndCity2 = new string(Enumerable.Range(0, nameLength).Select(s => chars[rnd.Next(chars.Length)]).ToArray());
                }
                expected[i] = new TicketCard(rndCity1, rndCity2);
                rndCity1 = rndCity2;
            }

            var unsortedCards = expected.OrderBy(x => rnd.Next()).ToArray();

            // Act
            var actual = journey.SortCards(unsortedCards).ToArray();

            // Assert
            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SortJourneyWithSameCityRoute()
        {
            // Arrange
            var journey = new Journey();
            var unsortedCards = new[]
            {
                new TicketCard("Москва", "Париж"),
                new TicketCard("Кельн","Кельн"),
                new TicketCard("Лондон", "Лондон"),
                new TicketCard("Париж", "Майами")
            };

            // Act
            var actual = journey.SortCards(unsortedCards);

            // Assert
            //Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SortJourneyWithCityNameEmpty()
        {
            // Arrange
            var journey = new Journey();
            var unsortedCards = new[]
            {
                new TicketCard("Москва", "Париж"),
                new TicketCard(" ","Кельн"),  
                new TicketCard("Париж", "Майами")
            };

            // Act
            var actual = journey.SortCards(unsortedCards);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SortCyclicJourney()
        {
            // Arrange
            var journey = new Journey();
            var unsortedCards = new[]
            {
                new TicketCard("Мельбурн", "Кельн"),
                new TicketCard("Москва","Париж"),
                new TicketCard("Лондон", "Майами"),
                new TicketCard("Париж", "Лондон"),
                new TicketCard("Кельн", "Москва"),
                new TicketCard("Майами", "Мельбурн")
            };

            // Act
            var actual = journey.SortCards(unsortedCards);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SortJourneyWithCycleInTheMiddle()
        {
            // Arrange
            var journey = new Journey();
            var unsortedCards = new[]
            { 
                new TicketCard("Мельбурн", "Кельн"),
                new TicketCard("Москва","Париж"),
                new TicketCard("Лондон", "Майами"),
                new TicketCard("Париж", "Кельн"),
                new TicketCard("Кельн", "Москва"),
                new TicketCard("Кельн", "Лондон")
            };

            // Act
            var actual = journey.SortCards(unsortedCards).ToArray();

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SortJourneyWithGapInTheMiddle()
        {
            // Arrange
            var journey = new Journey();
            var unsortedCards = new[]
            {
                new TicketCard("Мельбурн", "Кельн"),
                new TicketCard("Лондон", "Майами"),
                new TicketCard("Париж", "Лондон"),
                new TicketCard("Кельн", "Москва")
            };

            // Act
            var actual = journey.SortCards(unsortedCards).ToArray();

            // Assert
        }
    }
}

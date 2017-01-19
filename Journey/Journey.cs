using System;
using System.Collections.Generic;
using System.Linq;

namespace Journey
{
    public class Journey
    {
        /// <summary>
        /// Сортирует карты в порядке следования путешествия
        /// Сложность алогоритма сортировки O(n).
        /// </summary>
        /// <param name="unsortedCards">Несортированный список карт поездки</param>
        /// <returns>Список отсортированных карт</returns>
        public IEnumerable<TicketCard> SortCards(IEnumerable<TicketCard> unsortedCards)
        {
            if (unsortedCards == null)
                throw new ArgumentNullException(nameof(unsortedCards) + "; Список карт маршрута не должен быть null.");

            var journal = new Dictionary<string, TicketCard>();
            var routeValidator = new Dictionary<string, int>();
            
            //Проверим консистентность списка карт и заполним журнал отправлений
            foreach (var card in unsortedCards)
            {
                if (card.Departure == card.Arrival)
                    throw new ArgumentException($"В карте город отправления должен отличаться от города назначения: {card}");
                if (string.IsNullOrWhiteSpace(card.Arrival) || string.IsNullOrWhiteSpace(card.Departure))
                    throw new ArgumentException($"Наименования городов в карте не могут быть пустыми: {card}");

                journal.Add(card.Departure, card);

                if (routeValidator.ContainsKey(card.Departure))
                    routeValidator[card.Departure]++;
                else
                    routeValidator.Add(card.Departure, 1);

                if (routeValidator.ContainsKey(card.Arrival))
                    routeValidator[card.Arrival]--;
                else
                    routeValidator.Add(card.Arrival, -1);
            }

            if (journal.Count < 2) return unsortedCards.ToList();

            if (routeValidator.Count > journal.Count + 1) throw new ArgumentException("В списке карт маршрута не должно быть разрывов.");
            if (routeValidator.Count < journal.Count + 1) throw new ArgumentException("В маршруте не должно быть циклов.");

            //Найдем начало маршрута (Начало = 1, Конец = -1)
            var nextCard = routeValidator.Where(x => x.Value == 1).Select(x => journal[x.Key]).FirstOrDefault();
            if (nextCard == null) throw new ArgumentException("В списке карт маршрута не найден стартовый город.");

            var sortedCards = new List<TicketCard> { nextCard };
            while (sortedCards.Count < journal.Count)
            {
                nextCard = journal[nextCard.Arrival];
                sortedCards.Add(nextCard);
            }
            return sortedCards;
        }
    }
}

using System;

namespace Journey
{
    public class TicketCard : IEquatable<TicketCard>
    {
        public string Departure { get; set; }
        public string Arrival { get; set; }

        public TicketCard(string deprature, string arrival)
        {
            Departure = deprature;
            Arrival = arrival;
        }

        public override string ToString()
        {
            return $"{Departure} → {Arrival}";
        }

        public bool Equals(TicketCard other)
        {
            return !ReferenceEquals(other, null) 
                && Departure == other.Departure 
                && Arrival == other.Arrival;
        }

        public override bool Equals(object Obj)
        {
            return Equals(Obj as TicketCard);
        }

        public static bool operator ==(TicketCard card1, TicketCard card2)
        {
            if (ReferenceEquals(card1, null))
            {
                return ReferenceEquals(card2, null);
            }

            return card1.Equals(card2);
        }

        public static bool operator !=(TicketCard card1, TicketCard card2)
        {
            return !(card1 == card2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
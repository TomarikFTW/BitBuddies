﻿namespace BlackJack
{
    internal class Card
    {
        public int Value;
        public string Face;
        public bool IsRoyal;
        public bool IsAce;

        public Card(int value, string face, bool isRoyal = false, bool isAce = false)
        {
            Value = value;
            Face = face;
            IsRoyal = isRoyal;
            IsAce = isAce;
        }
    }
}
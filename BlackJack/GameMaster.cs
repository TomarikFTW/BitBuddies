﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BlackJack
{
    class GameMaster
    {
        public static bool CheckPlayersBet(Player player, int bet)
        {
            if (bet <= player.Funds && bet > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void OutputInfo(int balance, int bet, Player player)
        {
            int val = CheckValue(player);
            Console.Write($"Balance: ${balance}\nBet: ${bet}");

            Console.Write($"\nHand: ");
            foreach (Card card in player.DrawnCards)
            {
                Console.Write($"{card.Face}  ");
            }
            Console.WriteLine($"\nTotal Value: {val}\n");
        }

        public static Card HandleAces(Card cardAce, Player player)
        {
            bool makingChoice = true;
            player.countAce += 1;
            do
            {
                Console.Write($"Default ace value is 1. Would you rather play your {cardAce.Face} with the value 11? (Y/N)\nYour choice: ");
                ConsoleKeyInfo chooseAce = Console.ReadKey();
                Console.Clear();
                if (chooseAce.Key == ConsoleKey.Y)
                {
                    cardAce.Value = 11;
                    makingChoice = false;
                }
                else if (chooseAce.Key == ConsoleKey.N)
                {
                    cardAce.Value = 1;
                    makingChoice = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Enter (Y) for yes, or (N) for no\n");
                    makingChoice = true;
                }
            } while (makingChoice == true);

            Console.WriteLine();
            return cardAce;
        }

        public static bool Bust(Player player)
        {
            bool bust = false;
            int z = 0;
            int i = CheckValue(player);
            int x = player.countAce;

            // given player busts, change aces from value 11 to value 1, one at a time. Check each time the value of an ace changes, resume game if player falls bellow 21, bust if still greater than 21 after all aces changed to 1.
            if (i > 21)
            {
                foreach (Card card in player.DrawnCards)
                {
                    if (card.IsAce == true)
                    {
                        card.Value = 1;
                        if (x == z)
                        {
                            bust = true;
                            break;
                        }
                        z += 1;
                    }
                    bust = true;
                    int y = CheckValue(player);
                    if (y < 21)
                    {
                        bust = false;
                        break;
                    }
                }
            }

            return bust;
        }

        public static bool PlayAgain()
        {
            bool playAgain = true;
            bool hasSelected = false;

            while (hasSelected == false)
            {
                Console.Write("Would you like to play again? (Y/N)\nYour choice: ");
                ConsoleKeyInfo playChoice = Console.ReadKey();
                if (playChoice.Key == ConsoleKey.N)
                {
                    hasSelected = true;
                    playAgain = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Must type (Y) for yes, or (N) for no.\n");
                }
            }
            return playAgain;
        }

        public static int CheckValue(Player player)
        {
            int val = 0;
            foreach (Card card in player.DrawnCards)
            {
                val += card.Value;
            }

            return val;
        }

        public static void PlayerDrawsCard(Player player, List<Card> shuffledDeck)
        {
            // 'deal' the card out of shuffled deck, remove card from shuffled deck to eliminate repeats
            var newCard = shuffledDeck[1];
            shuffledDeck.RemoveAt(1);

            // handle ace
            if (newCard.IsAce == true && player.bot == false)
            {
                GameMaster.HandleAces(newCard, player);
            }

            // Add cards to drawn pile
            player.DrawnCards.Add(newCard);

            GameMaster.Bust(player);
        }

        public static bool CheckBlackjack(Player player, int bet)
        {
            bool blackjack = false;
            int val = CheckValue(player);
            if (val == 21)
            {
                blackjack = true;
                player.GotBlackJack = true;
            }
            return blackjack;
        }
    }
}

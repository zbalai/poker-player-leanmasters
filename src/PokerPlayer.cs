﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "Enjoy";

        public static int BetRequest(JObject gameState)
        {
            bool hasPair = false;
            int ownCardRankSum = 0;
            int pairRank = 0;
            int myIndex = (int)gameState["in_action"];
            int ownStack = 0;
            string firstCard = "";
            string secondCard = "";
            foreach (var x in gameState["players"])
            {
                if ((int)x["id"] == myIndex)
                {
                    /*System.Console.WriteLine("***");
                    System.Console.WriteLine(x);*/
                    ownStack = int.Parse(x["stack"].ToString());
                    foreach (var card in x["hole_cards"])
                    {
                        ownCardRankSum += getCard(card["rank"].ToString());
                        if (firstCard == "")
                        {
                            firstCard = card["rank"].ToString();
                        }
                        else
                        {
                            secondCard = card["rank"].ToString();
                            if (firstCard == secondCard)
                            {
                                hasPair = true;
                                pairRank = getCard(card["rank"].ToString());
                                System.Console.WriteLine("haspair");
                            }
                        }
                    }
                }
            }
            int raisingCards = 0;
            foreach (var comCard in gameState["community_cards"])
            {
                if (comCard["rank"].ToString() == firstCard
                    || comCard["rank"].ToString() == secondCard)
                {
                    raisingCards++;
                }
            }
            //System.Console.WriteLine(testj["players"]);

            if (hasPair && pairRank >= 5 || raisingCards > 1)
            {
                return 10000; // + (int)gameState["minimum_raise"];
            }
            else
            if (hasPair || ownCardRankSum > 20 || ((raisingCards > 0) && ownCardRankSum > 14))
            {
                return (int)gameState["current_buy_in"];
            }
            else
            {
                if ((int)gameState["current_buy_in"] > (ownStack / 10))
                {
                    return 0;
                }
                else
                {
                    return (int)gameState["current_buy_in"]; // + (int)gameState["minimum_raise"];
                }
            }

            /*if ((int)gameState["current_buy_in"] > 100)
            {
                return 0;
            }
            else
            {
                return (int)gameState["current_buy_in"]; // + (int)gameState["minimum_raise"];
            }*/
            //TODO: Use this method to return the value You want to bet

            //return 10000;
        }

        public static int getCard(string s)
        {
            switch (s)
            {
                case "1": return 1; break;
                case "2": return 2; break;
                case "3": return 3; break;
                case "4": return 4; break;
                case "5": return 5; break;
                case "6": return 6; break;
                case "7": return 7; break;
                case "8": return 8; break;
                case "9": return 9; break;
                case "10": return 10; break;
                case "J": return 11; break;
                case "Q": return 12; break;
                case "K": return 13; break;
                case "A": return 14; break;
            }
            return 0;
        }


        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}



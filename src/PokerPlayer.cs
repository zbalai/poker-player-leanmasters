using Newtonsoft.Json.Linq;
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

            int handScore = GetHandScore(firstCard, secondCard);
            

            int raisingCards = 0;
            bool CommunityCardsOut = false;
            foreach (var comCard in gameState["community_cards"])
            {
                if (comCard["rank"].ToString() == firstCard
                    || comCard["rank"].ToString() == secondCard)
                {
                    raisingCards++;
                }
                CommunityCardsOut = true;
            }
            System.Console.WriteLine("testRunning " + handScore.ToString());

            //System.Console.WriteLine(testj["players"]);

            if (CommunityCardsOut)
            {
                if(handScore > 66)
                {
                    //return 10000; // + (int)gameState["minimum_raise"];
                    return (int)gameState["current_buy_in"] + ownStack / 10;
                }
                if (handScore > 15)
                {
                    if ((int)gameState["current_buy_in"] < (ownStack / 3))
                    {
                        return (int)gameState["current_buy_in"] + (int)gameState["minimum_raise"];
                    }
                }
            }

            // TODO: raisingcards reevaluate
            if (hasPair && pairRank >= 6 || raisingCards > 1)
            {
                //return 10000; // + (int)gameState["minimum_raise"];
                return (int)gameState["current_buy_in"] + ownStack / 10;
            }
            else
            if (hasPair || ownCardRankSum > 20 || ((raisingCards > 0) && ownCardRankSum > 13))
            {
                return (int)gameState["current_buy_in"];
            }
            else
            {
                if ((int)gameState["current_buy_in"] > (ownStack / 8))
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

        private static int GetHandScore(string card1, string card2)
        {
            //1st card: 2
            if (card1 == "2" && card2 == "2") return 24;
            if (card1 == "2" && card2 == "3") return 1;
            if (card1 == "2" && card2 == "4") return 1;
            if (card1 == "2" && card2 == "5") return 1;
            if (card1 == "2" && card2 == "6") return 1;
            if (card1 == "2" && card2 == "7") return 1;
            if (card1 == "2" && card2 == "8") return 1;
            if (card1 == "2" && card2 == "9") return 2;
            if (card1 == "2" && card2 == "10") return 2;
            if (card1 == "2" && card2 == "J") return 3;
            if (card1 == "2" && card2 == "Q") return 6;
            if (card1 == "2" && card2 == "K") return 10;
            if (card1 == "2" && card2 == "A") return 23;
            //1st card: 3
            if (card1 == "3" && card2 == "2") return 1;
            if (card1 == "3" && card2 == "3") return 33;
            if (card1 == "3" && card2 == "4") return 1;
            if (card1 == "3" && card2 == "5") return 1;
            if (card1 == "3" && card2 == "6") return 1;
            if (card1 == "3" && card2 == "7") return 2;
            if (card1 == "3" && card2 == "8") return 2;
            if (card1 == "3" && card2 == "9") return 2;
            if (card1 == "3" && card2 == "10") return 3;
            if (card1 == "3" && card2 == "J") return 4;
            if (card1 == "3" && card2 == "Q") return 6;
            if (card1 == "3" && card2 == "K") return 11;
            if (card1 == "3" && card2 == "A") return 24;
            //1st card: 4
            if (card1 == "4" && card2 == "2") return 1;
            if (card1 == "4" && card2 == "3") return 2;
            if (card1 == "4" && card2 == "4") return 41;
            if (card1 == "4" && card2 == "5") return 2;
            if (card1 == "4" && card2 == "6") return 2;
            if (card1 == "4" && card2 == "7") return 2;
            if (card1 == "4" && card2 == "8") return 2;
            if (card1 == "4" && card2 == "9") return 2;
            if (card1 == "4" && card2 == "10") return 3;
            if (card1 == "4" && card2 == "J") return 5;
            if (card1 == "4" && card2 == "Q") return 7;
            if (card1 == "4" && card2 == "K") return 11;
            if (card1 == "4" && card2 == "A") return 26;
            //1st card: 5
            if (card1 == "5" && card2 == "2") return 2;
            if (card1 == "5" && card2 == "3") return 2;
            if (card1 == "5" && card2 == "4") return 2;
            if (card1 == "5" && card2 == "5") return 49;
            if (card1 == "5" && card2 == "6") return 2;
            if (card1 == "5" && card2 == "7") return 2;
            if (card1 == "5" && card2 == "8") return 2;
            if (card1 == "5" && card2 == "9") return 3;
            if (card1 == "5" && card2 == "10") return 4;
            if (card1 == "5" && card2 == "J") return 5;
            if (card1 == "5" && card2 == "Q") return 8;
            if (card1 == "5" && card2 == "K") return 12;
            if (card1 == "5" && card2 == "A") return 28;
            //1st card: 6
            if (card1 == "6" && card2 == "2") return 2;
            if (card1 == "6" && card2 == "3") return 2;
            if (card1 == "6" && card2 == "4") return 2;
            if (card1 == "6" && card2 == "5") return 3;
            if (card1 == "6" && card2 == "6") return 48;
            if (card1 == "6" && card2 == "7") return 3;
            if (card1 == "6" && card2 == "8") return 3;
            if (card1 == "6" && card2 == "9") return 4;
            if (card1 == "6" && card2 == "10") return 4;
            if (card1 == "6" && card2 == "J") return 5;
            if (card1 == "6" && card2 == "Q") return 8;
            if (card1 == "6" && card2 == "K") return 13;
            if (card1 == "6" && card2 == "A") return 28;
            //1st card: 7
            if (card1 == "7" && card2 == "2") return 2;
            if (card1 == "7" && card2 == "3") return 2;
            if (card1 == "7" && card2 == "4") return 3;
            if (card1 == "7" && card2 == "5") return 3;
            if (card1 == "7" && card2 == "6") return 4;
            if (card1 == "7" && card2 == "7") return 67;
            if (card1 == "7" && card2 == "8") return 4;
            if (card1 == "7" && card2 == "9") return 4;
            if (card1 == "7" && card2 == "10") return 5;
            if (card1 == "7" && card2 == "J") return 6;
            if (card1 == "7" && card2 == "Q") return 9;
            if (card1 == "7" && card2 == "K") return 14;
            if (card1 == "7" && card2 == "A") return 31;
            //1st card: 8
            if (card1 == "8" && card2 == "2") return 2;
            if (card1 == "8" && card2 == "3") return 2;
            if (card1 == "8" && card2 == "4") return 3;
            if (card1 == "8" && card2 == "5") return 4;
            if (card1 == "8" && card2 == "6") return 5;
            if (card1 == "8" && card2 == "7") return 6;
            if (card1 == "8" && card2 == "8") return 80;
            if (card1 == "8" && card2 == "9") return 5;
            if (card1 == "8" && card2 == "10") return 6;
            if (card1 == "8" && card2 == "J") return 7;
            if (card1 == "8" && card2 == "Q") return 10;
            if (card1 == "8" && card2 == "K") return 15;
            if (card1 == "8" && card2 == "A") return 36;
            //1st card: 9
            if (card1 == "9" && card2 == "2") return 3;
            if (card1 == "9" && card2 == "3") return 3;
            if (card1 == "9" && card2 == "4") return 3;
            if (card1 == "9" && card2 == "5") return 4;
            if (card1 == "9" && card2 == "6") return 5;
            if (card1 == "9" && card2 == "7") return 6;
            if (card1 == "9" && card2 == "8") return 8;
            if (card1 == "9" && card2 == "9") return 96;
            if (card1 == "9" && card2 == "10") return 7;
            if (card1 == "9" && card2 == "J") return 9;
            if (card1 == "9" && card2 == "Q") return 12;
            if (card1 == "9" && card2 == "K") return 18;
            if (card1 == "9" && card2 == "A") return 41;
            //1st card: 10
            if (card1 == "10" && card2 == "2") return 4;
            if (card1 == "10" && card2 == "3") return 4;
            if (card1 == "10" && card2 == "4") return 5;
            if (card1 == "10" && card2 == "5") return 5;
            if (card1 == "10" && card2 == "6") return 6;
            if (card1 == "10" && card2 == "7") return 7;
            if (card1 == "10" && card2 == "8") return 9;
            if (card1 == "10" && card2 == "9") return 11;
            if (card1 == "10" && card2 == "10") return 120;
            if (card1 == "10" && card2 == "J") return 12;
            if (card1 == "10" && card2 == "Q") return 15;
            if (card1 == "10" && card2 == "K") return 23;
            if (card1 == "10" && card2 == "A") return 53;
            //1st card: J
            if (card1 == "J" && card2 == "2") return 6;
            if (card1 == "J" && card2 == "3") return 6;
            if (card1 == "J" && card2 == "4") return 7;
            if (card1 == "J" && card2 == "5") return 7;
            if (card1 == "J" && card2 == "6") return 7;
            if (card1 == "J" && card2 == "7") return 9;
            if (card1 == "J" && card2 == "8") return 10;
            if (card1 == "J" && card2 == "9") return 13;
            if (card1 == "J" && card2 == "10") return 18;
            if (card1 == "J" && card2 == "J") return 160;
            if (card1 == "J" && card2 == "Q") return 16;
            if (card1 == "J" && card2 == "K") return 25;
            if (card1 == "J" && card2 == "A") return 68;
            //1st card: Q
            if (card1 == "Q" && card2 == "2") return 8;
            if (card1 == "Q" && card2 == "3") return 9;
            if (card1 == "Q" && card2 == "4") return 10;
            if (card1 == "Q" && card2 == "5") return 10;
            if (card1 == "Q" && card2 == "6") return 11;
            if (card1 == "Q" && card2 == "7") return 11;
            if (card1 == "Q" && card2 == "8") return 13;
            if (card1 == "Q" && card2 == "9") return 16;
            if (card1 == "Q" && card2 == "10") return 22;
            if (card1 == "Q" && card2 == "J") return 25;
            if (card1 == "Q" && card2 == "Q") return 239;
            if (card1 == "Q" && card2 == "K") return 29;
            if (card1 == "Q" && card2 == "A") return 96;
            //1st card: K
            if (card1 == "K" && card2 == "2") return 13;
            if (card1 == "K" && card2 == "3") return 14;
            if (card1 == "K" && card2 == "4") return 15;
            if (card1 == "K" && card2 == "5") return 16;
            if (card1 == "K" && card2 == "6") return 17;
            if (card1 == "K" && card2 == "7") return 19;
            if (card1 == "K" && card2 == "8") return 20;
            if (card1 == "K" && card2 == "9") return 24;
            if (card1 == "K" && card2 == "10") return 31;
            if (card1 == "K" && card2 == "J") return 36;
            if (card1 == "K" && card2 == "Q") return 43;
            if (card1 == "K" && card2 == "K") return 477;
            if (card1 == "K" && card2 == "A") return 166;
            //1st card: A
            if (card1 == "A" && card2 == "2") return 29;
            if (card1 == "A" && card2 == "3") return 31;
            if (card1 == "A" && card2 == "4") return 33;
            if (card1 == "A" && card2 == "5") return 36;
            if (card1 == "A" && card2 == "6") return 35;
            if (card1 == "A" && card2 == "7") return 40;
            if (card1 == "A" && card2 == "8") return 45;
            if (card1 == "A" && card2 == "9") return 52;
            if (card1 == "A" && card2 == "10") return 70;
            if (card1 == "A" && card2 == "J") return 92;
            if (card1 == "A" && card2 == "Q") return 137;
            if (card1 == "A" && card2 == "K") return 277;
            if (card1 == "A" && card2 == "A") return 500;

            return 0;
        }
    }
}



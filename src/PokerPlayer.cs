using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "Default C# folding player";

        public static int BetRequest(JObject gameState)
        {
            bool hasPair = false;
            int myIndex = (int)gameState["in_action"];
            foreach (var x in gameState["players"])
            {
                if ((int)x["id"] == myIndex)
                {
                    /*System.Console.WriteLine("***");
                    System.Console.WriteLine(x);*/
                    string firstCard = "";
                    foreach (var card in x["hole_cards"])
                    {
                        if (firstCard == "")
                        {
                            firstCard = card["rank"].ToString();
                        }
                        else
                        {
                            if (firstCard == card["rank"].ToString())
                            {
                                hasPair = true;
                                //System.Console.WriteLine("haspair");
                            }
                        }
                        /*System.Console.WriteLine(card["rank"]);
                        if(card["rank"].ToString()=="K")
                        {
                            System.Console.WriteLine("KING");
                        }*/
                    }
                }
            }
            //System.Console.WriteLine(testj["players"]);

            if (hasPair)
            {
                return (int)gameState["current_buy_in"]; // + (int)gameState["minimum_raise"];
            }
            else
            {
                if ((int)gameState["current_buy_in"] > 100)
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

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }
    }
}


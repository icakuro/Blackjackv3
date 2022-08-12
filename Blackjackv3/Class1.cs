using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjackv3
{
    internal class Blackjackv3
    {
       public static int point = 100;

        public static void Main(string[] args)
        {
            main.start();
        }

    }
    public class main
    {
      public static void start()
        {
            while (true)
            {
                if (Blackjackv3.point < 0)
                {
                    Console.WriteLine("Game Over");
                    Console.WriteLine("CONTINUE ANY KEY");
                    Console.ReadLine();
                    Console.WriteLine("reset +coin 100");
                    Blackjackv3.point = 100;
                }
                else if (Blackjackv3.point > 0)
                {
                    Console.WriteLine("現在コイン" + Blackjackv3.point + "ベットする金額を決めてください。1-100");
                    int point = Convert.ToInt32(Console.ReadLine());
                    if (point <= 100 && Blackjackv3.point >= point)
                    {
                        Blackjackv3.point -= point;

                        judge.admin(point); 


                    }
                    else if (point > 100 && Blackjackv3.point <= point)
                    {
                        Console.WriteLine("その数はベットできません。");
                    }
                }

            }
        }
    }

    enum result
    {
        win,
        lose,
        draw
    }
    public enum Action
    {
        raze,
        forld,
        bet
    }

    class Card
    {
        public String[] num = new string[14];
        public String[] Mark = new string[4];

        public int[] points = new int[14];

        public String numpointer;

        public String markpointer;

        public int pointpointer;



        private Card cardpointer;


        public Card()
        {
            for (int i = 1; i <= 13; i++)
            {
                num[i] = i.ToString();
            }

            Mark[0] = "スペード";
            Mark[1] = "ダイヤ";
            Mark[2] = "ハート";
            Mark[3] = "クラブ";

            for (int i = 1; i <= 13; i++)
            {
                points[i] = i;
            }
        }

        public Card(String num, String mark, int point)
        {
            numpointer = num;

            markpointer = mark;

            pointpointer = point;
        }

        public Card clcard(String num, String mark, int point)
        {

            cardpointer = new Card(num, mark, point);

            return cardpointer;
        }

    }

    class Deck
    {
        public Queue<Card> queue = new Queue<Card>();

        public Deck()
        {
            Card card = new Card();

            for (int i = 1; i <= 13; i++)
            {
                queue.Enqueue(card.clcard(card.num[i], card.Mark[0], card.points[i]));
            }
            for (int i = 1; i <= 13; i++)
            {
                queue.Enqueue(card.clcard(card.num[i], card.Mark[1], card.points[i]));
            }
            for (int i = 1; i <= 13; i++)
            {
                queue.Enqueue(card.clcard(card.num[i], card.Mark[2], card.points[i]));
            }
            for (int i = 1; i <= 13; i++)
            {
                queue.Enqueue(card.clcard(card.num[i], card.Mark[3], card.points[i]));
            }
        }

        public Queue<Card> SuffleDeck(Deck deck)
        {
            Card[] cards = deck.queue.ToArray();
            Random rand = new Random();
            int n = cards.GetLength(0);
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                Card card = cards[k];
                cards[k] = cards[n];
                cards[n] = card;
            }

            Queue<Card> queue = new Queue<Card>(cards);

            return queue;

        }


    }
    public class judge
        {
    
        public static void admin(int money)
        {
            result result = Fight();
            Action action = judge.GetAction();


            if(result.win == result)
            {
                if (Action.raze == action)
                {
                    Console.WriteLine(money + "raze:x2\n" + money * 2);
                    money *= 2;
                }
                if (Action.forld == action)
                {
                    Console.WriteLine(money + "forld:\n" + money);
                }
                if (Action.bet == action)
                {
                    Console.WriteLine(money + "bet\n" + money + money);
                    money += money;
                }
            }
                
            else if(result.lose ==result)
            {
                if (Action.raze == action)
                {
                    Console.WriteLine(money + "raze:-x2\n" + money * 2);
                    money -= money * 2;
                }
                if (Action.forld == action)
                {
                    Console.WriteLine(money + "forld:\n" + money);
                }
                if (Action.bet == action)
                {
                    Console.WriteLine(money + "bet\n" + money + money);
                    money -= money;
                }

            }
            else if(result.draw ==result)
            {
                if (Action.raze == action)
                {
                    Console.WriteLine(money + "raze:x2\n" + (money * 2)/2);
                    money -= (money * 2)/2;
                }
                if (Action.forld == action)
                {
                    Console.WriteLine(money + "forld:\n" + money);
                }
                if (Action.bet == action)
                {
                    Console.WriteLine(money + "bet\n" + money + money/2);
                    money += money/2;
                }
            }

            Blackjackv3.point += money;
        }
     
            bool playersflag = true;
            bool dealersflag = true;
        
       



        public int dealerscard;
        public int playerscard;
        public static Action actionpointer;


        static Action GetAction()
        {
            return actionpointer;
        }


            static result Fight()
        {
            judge judge = new judge();
            Deck deck = new Deck();
            deck.queue = deck.SuffleDeck(deck);
            while (judge.playersflag || judge.dealersflag)
            {
                if (judge.playersflag)
                {
                    judge.playersturn(deck.queue.Dequeue());
                }
                if (judge.dealersflag)
                {
                    judge.dealersturn(deck.queue.Dequeue());
                }
            }
            if (judge.playerscard <= 21 && judge.dealerscard <= 21)
            {
                if (judge.playerscard <= judge.dealerscard)
                {
                    Console.WriteLine("相手の合計");
                    Console.WriteLine(judge.dealerscard);
                    Console.WriteLine("あなたの合計");
                    Console.WriteLine(judge.playerscard);
                    Console.WriteLine("あなたの負けです...");
                    return result.lose;
                }
                else if (judge.playerscard >= judge.dealerscard)
                {
                    Console.WriteLine("あなたの合計");
                    Console.WriteLine(judge.playerscard);
                    Console.WriteLine("相手の合計");
                    Console.WriteLine(judge.dealerscard);
                    Console.WriteLine("あなたの勝ちです！");
                    return result.win;
                }





            }
            else if (judge.playerscard > 21 || judge.dealerscard > 21)
            {
                if (judge.playerscard > 21)
                {
                    Console.WriteLine("バースト！");
                    Console.WriteLine("あなたの負けです...");
                    return result.lose;
                }
                if (judge.dealerscard > 21)
                {
                    Console.WriteLine("バースト！");
                    Console.WriteLine("あなたの勝ちです！");
                    return result.win;

                }

            }
            else if(judge.playerscard == 21 && judge.dealerscard == 21)
            {
                Console.WriteLine("引き分け");
                return result.draw;
            }
            return result.draw;
        }

        public bool firstmatch = true;

        void dealersturn(Card card)
        {
            if(16 < dealerscard)
            {
                Console.WriteLine("引かない");
                dealersflag = false;
                Console.WriteLine("ディーラー合計" + dealerscard);
            }else if(16 > dealerscard)
            {
                Console.WriteLine(card.markpointer +"の"+ card.numpointer+"を"+"引いた");
                dealerscard += card.pointpointer;
                Console.WriteLine("ディーラー合計" + dealerscard);

            }
        }
         void playersturn(Card card)
        {

             if (firstmatch)
            {
                Console.WriteLine("アクション? 賭けに属性を付けれます。\nraze:期待値二倍。しかし負けると負債も二倍\nforld:何もかけない。引かないと同じ？これがどのような効果を出すかは分からない。\nbet:通常。");

                switch (Console.ReadLine())
                {
                    case "raze":
                        {
                            actionpointer = Action.raze;
                        }
                        break;
                    case "forld":
                        {
                            actionpointer = Action.forld;
                        }
                        break;
                    case "bet":
                        {
                            actionpointer = Action.bet;
                        }
                        break;
                }
            }
            firstmatch = false;


            Console.WriteLine("引く 引かない");
            String str = Console.ReadLine();
            if (str.Equals("引く"))
            {
                Console.WriteLine(card.markpointer + "の" + card.numpointer + "を引いた！");
                playerscard += card.pointpointer;
                Console.WriteLine("プレイヤー合計" + playerscard);
            }
            else if (str.Equals("引かない"))
            {
                Console.WriteLine("スタンド");
                playersflag = false;
                Console.WriteLine("プレイヤー合計" + playerscard);
            }
        }



    
    
    }


}

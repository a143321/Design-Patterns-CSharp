using System.Collections.Generic;

namespace Strategy_Pattern_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 通常のアヒルを生成
            Duck duck = new Duck(new SmallQuack());

            // アメリカホシハジロを生成
            Duck redHead = new RedHeadDuck(new Quack());

            // ゴムのアヒルを生成
            Duck rubber = new RubberDuck(new Squeak());

            // 偽物のアヒルを生成
            Duck decoy = new DecoyDuck(new MuteQuack());

            // アヒルの鳴き声
            duck.quack();

            // アメリカホシハジロの鳴き声
            redHead.quack();

            // ゴムのアヒルの鳴き声
            rubber.quack();

            // 偽物のアヒルの鳴き声
            decoy.quack();

            System.Console.WriteLine("------------");

            // リストに格納して実行することもできます
            List<Duck> duckList = new List<Duck>();
            duckList.Add(duck);
            duckList.Add(redHead);
            duckList.Add(rubber);
            duckList.Add(decoy);

            foreach (var item in duckList)
            {
                item.quack();
            }
        }
    }

    /// <summary>
    /// 鳴き声インタフェース
    /// </summary>
    public interface QuackBehavior
    {
        void sound();
    }

    /// <summary>
    /// 静かな(アヒルなどの)ガーガー鳴き声クラス
    /// </summary>
    public class SmallQuack : QuackBehavior
    {
        public void sound()
        {
            System.Console.WriteLine("Normal sound : kua! kua!");
        }
    }

    /// <summary>
    /// (アヒルなどの)ガーガー鳴き声クラス
    /// </summary>
    public class Quack : QuackBehavior
    {
        public void sound()
        {
            System.Console.WriteLine("Noisy Sound : !!!!gua-gua!!!!");
        }
    }

    /// <summary>
    /// キュッキュッ鳴る鳴き声クラス
    /// </summary>
    public class Squeak : QuackBehavior
    {
        public void sound()
        {
            System.Console.WriteLine("Cute sound : kyu-kyu");
        }
    }

    /// <summary>
    /// 無音鳴き声クラス
    /// </summary>
    public class MuteQuack : QuackBehavior
    {
        public void sound()
        {
            System.Console.WriteLine("No sound : <<mute>>");
        }
    }

    /// <summary>
    /// アヒルクラス
    /// </summary>
    public class Duck
    {
        /// <summary>
        /// 鳴き声
        /// </summary>
        private QuackBehavior quackBehavior;

        public Duck(QuackBehavior behavior)
        {
            quackBehavior = behavior;
        }

        /// <summary>
        /// 鳴き声を発する
        /// </summary>
        public void quack()
        {
            quackBehavior.sound();
        }
    }

    /// <summary>
    /// アメリカホシハジロ
    /// </summary>
    public class RedHeadDuck : Duck
    {
        public RedHeadDuck(QuackBehavior behavior) : base(behavior)
        {
        }
    }

    /// <summary>
    /// ゴムのアヒル
    /// </summary>
    public class RubberDuck : Duck
    {
        public RubberDuck(QuackBehavior behavior) : base(behavior)
        {
        }
    }

    /// <summary>
    /// 偽物のアヒル
    /// </summary>
    public class DecoyDuck : Duck
    {
        public DecoyDuck(QuackBehavior behavior) : base(behavior)
        {
        }
    }
}

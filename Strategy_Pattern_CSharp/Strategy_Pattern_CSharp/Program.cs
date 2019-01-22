using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_Pattern_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 通常のアヒルを生成
            Duck duck = new Duck();

            // アメリカホシハジロを生成
            Duck redHead = new RedHeadDuck();

            // ゴムのアヒルを生成
            Duck rubber = new RubberDuck();

            // 偽物のアヒルを生成
            Duck decoy = new DecoyDuck();

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
        void quack();
    }

    /// <summary>
    /// ガーガー鳴く
    /// </summary>
    public class Quack : QuackBehavior
    {
        public void quack()
        {
            System.Console.WriteLine("gua-gua");
        }
    }

    /// <summary>
    /// チューチュー鳴く
    /// </summary>
    public class Squeak : QuackBehavior
    {
        public void quack()
        {
            System.Console.WriteLine("kyu-kyu");
        }
    }

    /// <summary>
    /// 何も鳴かない
    /// </summary>
    public class MuteQuack : QuackBehavior
    {
        public void quack()
        {
            System.Console.WriteLine("<<mute>>");
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
        protected QuackBehavior cry = new Quack();

        public Duck() { }

        /// <summary>
        /// 鳴き声を発する
        /// </summary>
        public void quack()
        {
            cry.quack();
        }
    }

    /// <summary>
    /// アメリカホシハジロ
    /// </summary>
    public class RedHeadDuck : Duck
    {
        public RedHeadDuck()
        {
            cry = new Quack();
        }
    }

    /// <summary>
    /// ゴムのアヒル
    /// </summary>
    public class RubberDuck : Duck
    {
        public RubberDuck()
        {
            cry = new Squeak();
        }
    }

    /// <summary>
    /// 偽物のアヒル
    /// </summary>
    public class DecoyDuck : Duck
    {
        public DecoyDuck()
        {
            cry = new MuteQuack();
        }
    }
}

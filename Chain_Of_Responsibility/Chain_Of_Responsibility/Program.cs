using System;

namespace ConsoleApp10
{
    public enum PRICE_MANAGER_APPROVE : int
    {
        MANAGER = 100000,
        DIRECTOR = 200000,
        PRESIDENT = 300000
    }

    class Program
    {
        static void Main(string[] args)
        {
            Purchase item1 = new Purchase(1, "PC", 100000);
            Purchase item2 = new Purchase(2, "High Spec PC", 200000);
            Purchase item3 = new Purchase(3, "Super High Spec PC", 300000);
            Purchase item4 = new Purchase(4, "Ultra High Spec PC", 400000);
            Authorizer manager = new GeneralManager("Ichiro");
            Authorizer director = new Director("Tom");
            Authorizer president = new President("Jerry");

            manager.SetNextApprove(director);
            director.SetNextApprove(president);
            president.SetNextApprove(null);

            // マネージャーにPC購入の承認を判定してもらう
            manager.JudgeApproveItem(item1);
            Console.WriteLine();

            // マネージャーにHigh Spec PC購入の承認を判定してもらう
            manager.JudgeApproveItem(item2);
            Console.WriteLine();

            // マネージャーにSuper High Spec PC購入の承認を判定してもらう
            manager.JudgeApproveItem(item3);
            Console.WriteLine();

            // マネージャーにUltra High Spec PC購入の承認を判定してもらう
            manager.JudgeApproveItem(item4);
        }
    }

    /// <summary>
    /// 承認者クラス
    /// </summary>
    public abstract class Authorizer
    {
        /// <summary>
        /// 承認者名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 次の承認者
        /// </summary>
        protected Authorizer next = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="someName"></param>
        public Authorizer(string someName)
        {
            this.Name = someName;
        }

        /// <summary>
        /// 次の承認者をセットする
        /// </summary>
        /// <param name="someNext"></param>
        public abstract void SetNextApprove(Authorizer someNext);

        /// <summary>
        /// 購入物を承認するか判定する
        /// </summary>
        /// <param name="somePurchase"></param>
        public abstract void JudgeApproveItem(Purchase somePurchase);
    }

    /// <summary>
    /// 部長クラス
    /// </summary>
    public class GeneralManager : Authorizer
    {
        public GeneralManager(string someName) : base(someName) { }

        public override void SetNextApprove(Authorizer someNext)
        {
            this.next = someNext;
        }

        public override void JudgeApproveItem(Purchase somePurchase)
        {
            if (somePurchase.Price <= (int)PRICE_MANAGER_APPROVE.MANAGER)
            {
                Console.WriteLine("Manager has approved this item({0} : {1}yen)].", somePurchase.Name, somePurchase.Price);
            }
            else
            {
                Console.WriteLine("Manager can't judge this item({0} : {1}yen)].", somePurchase.Name, somePurchase.Price);
                next.JudgeApproveItem(somePurchase);
            }
        }
    }

    /// <summary>
    /// 取締役クラス
    /// </summary>
    public class Director : Authorizer
    {
        public Director(string someName) : base(someName) { }

        public override void SetNextApprove(Authorizer someNext)
        {
            this.next = someNext;
        }

        public override void JudgeApproveItem(Purchase somePurchase)
        {
            if (somePurchase.Price <= (int)PRICE_MANAGER_APPROVE.DIRECTOR)
            {
                Console.WriteLine("Director has approved this item({0} : {1}yen)].", somePurchase.Name, somePurchase.Price);
            }
            else
            {
                Console.WriteLine("Director can't judge this item({0} : {1}yen)].", somePurchase.Name, somePurchase.Price);
                next.JudgeApproveItem(somePurchase);
            }
        }
    }

    /// <summary>
    /// 社長クラス
    /// </summary>
    public class President : Authorizer
    {
        public President(string someName) : base(someName) { }

        public override void SetNextApprove(Authorizer someNext)
        {
            this.next = someNext;
        }


        public override void JudgeApproveItem(Purchase somePurchase)
        {
            if (somePurchase.Price <= (int)PRICE_MANAGER_APPROVE.PRESIDENT)
            {
                Console.WriteLine("President has approved this item({0} : {1}yen)].", somePurchase.Name, somePurchase.Price);
            }
            else
            {
                Console.WriteLine("President can't judge this item({0} : {1}yen)].", somePurchase.Name, somePurchase.Price);
            }
        }
    }

    /// <summary>
    /// 購入品クラス
    /// </summary>
    public class Purchase
    {
        public int Id { get; private set; } = 0;
        public string Name { get; private set; } = null;
        public int Price { get; private set; } = 0;

        public Purchase(int someId, string someName, int somePrice)
        {
            this.Id = someId;
            this.Name = someName;
            this.Price = somePrice;
        }
    }
}


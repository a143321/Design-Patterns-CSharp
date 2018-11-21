using State_Pattern_CSharp.State;
using System;
using System.Timers;

namespace State_Pattern_CSharp
{
    public enum HeaterMode
    {
        Low,
        High
    }

    public class Program
    {
        /// <summary>
        /// 外気温温度[℃] (固定)
        /// </summary>
        private readonly static int OutsideTemperature = 20;

        /// <summary>
        /// 加熱による温度上昇 (固定)
        /// </summary>
        private readonly static int EffectOfHeating = 5;

        /// <summary>
        /// 外気による温度減少 (固定)
        /// </summary>
        private readonly static int EffectOfOutsideAir = 3;

        /// <summary>
        /// ヒーター温度最小値 (弱モード時)
        /// </summary>
        private readonly static int MinTempHeaterLowMode = 30;

        /// <summary>
        /// ヒーター温度最大値 (弱モード時)
        /// </summary>
        private readonly static int MaxTempHeaterLowMode = 60;

        /// <summary>
        /// ヒーター温度最小値 (強モード時)
        /// </summary>
        private readonly static int MinTempHeaterHighMode = 70;

        /// <summary>
        /// ヒーター温度最大値 (強モード時)
        /// </summary>
        private readonly static int MaxTempHeaterHighMode = 100;

        static void Main(string[] args)
        {
            PotContext context = new PotContext(new HeaterContext(MinTempHeaterHighMode, MaxTempHeaterHighMode, HeaterMode.High));

            // 開始時に間隔を指定する
            var timer = new Timer(1000/*msec*/);

            // Elapsedイベントにタイマー発生時の処理を設定する
            timer.Elapsed += (sender, e) =>
            {
                ChangeWaterTemperatureEventHandler(context);
            };

            // タイマーを開始する
            timer.Start();

            while (true)
            {
                ShowCommand();

                string line = Console.ReadLine();

                if (int.TryParse(line, out int value))
                {
                    switch (value)
                    {
                        case 0:
                            context.ShowCurrentState();
                            break;
                        case 1:
                            context.PushPowerBtn();
                            Console.WriteLine("電源ボタンが押されました。");
                            context.ShowCurrentState();
                            break;
                        case 2:
                            context.PushHeatBtn();
                            Console.WriteLine("加熱ボタンが押されました。");
                            context.ShowCurrentState();
                            break;
                        case 3:
                            context.PushStopBtn();
                            Console.WriteLine("停止ボタンが押されました。");
                            context.ShowCurrentState();
                            break;
                        case 4:
                            context.HeaterContext.MinTemperature = MinTempHeaterLowMode;
                            context.HeaterContext.MaxTemperature = MaxTempHeaterLowMode;
                            context.HeaterContext.HeaterMode = HeaterMode.Low;
                            Console.WriteLine("ヒーター設定が弱に変更されました");
                            break;
                        case 5:
                            context.HeaterContext.MinTemperature = MinTempHeaterHighMode;
                            context.HeaterContext.MaxTemperature = MaxTempHeaterHighMode;
                            context.HeaterContext.HeaterMode = HeaterMode.High;
                            Console.WriteLine("ヒーター設定が強に変更されました");
                            break;

                        default:
                            Console.WriteLine("コマンドの数字を入力してください。");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("コマンドの数字を入力してください。");
                }
            }
        }

        /// <summary>
        /// 水温を変化させるイベントハンドラ
        /// </summary>
        /// <remarks>タイマーにより、一定間隔で呼び出される</remarks>
        /// <param name="context"></param>
        public static void ChangeWaterTemperatureEventHandler(PotContext context)
        {
            context.MeasureTemperature(context.HeaterContext);

            if (context.GetState().GetType() == typeof(PowerOffState))
            {
                Thermometer.CurrentTemp = CalculateInfluenceOutsideTemp(Thermometer.CurrentTemp);
            }
            if (context.GetState().GetType() == typeof(IdleState))
            {
                Thermometer.CurrentTemp = CalculateInfluenceOutsideTemp(Thermometer.CurrentTemp);
            }
            if (context.GetState().GetType() == typeof(HeatState))
            {
                int dif = Thermometer.CurrentTemp + EffectOfHeating;

                Thermometer.CurrentTemp = (dif > context.HeaterContext.MaxTemperature) ? context.HeaterContext.MaxTemperature : dif;
            }
            if (context.GetState().GetType() == typeof(WarmState))
            {
                Thermometer.CurrentTemp = CalculateInfluenceOutsideTemp(Thermometer.CurrentTemp);
            }
        }

        /// <summary>
        /// 外気温による水温の変化を計算する
        /// </summary>
        /// <param name="currentTemp"></param>
        /// <remarks>外気温より水温は低くなることはないものとする</remarks>
        /// <returns></returns>
        public static int CalculateInfluenceOutsideTemp(int currentTemp)
        {
            int dif = currentTemp - EffectOfOutsideAir;
            return (dif > OutsideTemperature) ? dif : OutsideTemperature;
        }

        /// <summary>
        /// コマンドを表示する
        /// </summary>
        public static void ShowCommand()
        {
            Console.WriteLine("コマンドを指定してください。");
            Console.WriteLine("0 : 現在の状態を表示する");
            Console.WriteLine("1 : 電源ボタンを押す");
            Console.WriteLine("2 : 加熱ボタンを押す");
            Console.WriteLine("3 : 停止ボタンを押す");
            Console.WriteLine("4 : ヒーターモードを弱に変更する(固定(Min:{0}度, Max:{1}度))", MinTempHeaterLowMode, MaxTempHeaterLowMode);
            Console.WriteLine("5 : ヒーターモードを強に変更する(固定(Min:{0}度, Max:{1}度))", MinTempHeaterHighMode, MaxTempHeaterHighMode);
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 水温温度計クラス。
    /// </summary>
    public static class Thermometer
    {
        /// <summary>
        /// 現在の電気ポットの水温
        /// </summary>
        public static int CurrentTemp { get; set; }
    }

    /// <summary>
    /// 電気ポット状態を表すクラス
    /// </summary>
    /// <remarks>
    /// 状態毎に振舞いが異なるメソッドのインタフェースを定義
    /// </remarks>
    public interface IState
    {
        IState PushPowerBtnEvent();
        IState PushStopBtnEvent();
        IState PushHeatBtnEvent();
        IState MeasureTemperatureEvent(HeaterContext someHeaterContext);
    }

    public class HeaterContext
    {
        /// <summary>
        /// 設定温度の最小値
        /// </summary>
        public int MinTemperature { get; set; }

        /// <summary>
        /// 設定温度の最大値
        /// </summary>
        public int MaxTemperature { get; set; }

        /// <summary>
        /// ヒーターモード
        /// </summary>
        public HeaterMode HeaterMode { get; set; }

        public HeaterContext(int someMinValue, int someMaxValue, HeaterMode someMode)
        {
            this.MinTemperature = someMinValue;
            this.MaxTemperature = someMaxValue;
            this.HeaterMode = someMode;
        }
    }


    /// <summary>
    /// 電気ポット制御情報クラス
    /// </summary>
    /// <remarks>
    /// ヒーター設定が変更された場合は、ヒーター設定パラメータクラスのインタスタンスのみを更新する
    /// </remarks>
    public class PotContext
    {
        /// <summary>
        ///　電気ポットの稼働状態
        /// </summary>
        private IState State = null;

        /// <summary>
        /// ヒーター設定パラメーター
        /// </summary>
        public HeaterContext HeaterContext { get; set; } = null;

        public PotContext(HeaterContext someHeaterContext)
        {
            this.HeaterContext = someHeaterContext;

            new PowerOffState();
            new IdleState();
            new WarmState();
            new HeatState();

            if (State == null)
            {
                State = PowerOffState.sIntance;
            }
        }

        public IState GetState()
        {
            return this.State;
        }

        public void ShowCurrentState()
        {
            Console.WriteLine("[IState : {0}] [Current Temperature : {1}] [Heater Mode : {2}]\n\n\n", this.State.ToString(), Thermometer.CurrentTemp, this.HeaterContext.HeaterMode.ToString());
        }

        public void PushPowerBtn()
        {
            this.State = this.State.PushPowerBtnEvent();
        }

        public void PushStopBtn()
        {
            this.State = this.State.PushStopBtnEvent();
        }

        public void PushHeatBtn()
        {
            this.State = this.State.PushHeatBtnEvent();
        }

        public void MeasureTemperature(HeaterContext someHeaterContext)
        {
            this.State = this.State.MeasureTemperatureEvent(someHeaterContext);
        }
    }
}

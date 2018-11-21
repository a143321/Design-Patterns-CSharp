namespace State_Pattern_CSharp.State
{
    /// <summary>
    /// 電源OFF状態
    /// </summary>
    public class PowerOffState : IState
    {
        /// <summary>
        /// 自己状態変数
        /// </summary>
        public static readonly IState sIntance = new PowerOffState();

        public PowerOffState()
        {
        }

        public IState PushPowerBtnEvent()
        {
            return IdleState.sIntance;
        }

        public IState PushStopBtnEvent()
        {
            return this;
        }

        public IState PushHeatBtnEvent()
        {
            return this;
        }

        public IState MeasureTemperatureEvent(HeaterContext someHeaterContext)
        {
            return this;
        }
    }
}

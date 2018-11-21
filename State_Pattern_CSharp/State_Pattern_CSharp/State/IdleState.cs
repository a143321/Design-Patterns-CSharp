namespace State_Pattern_CSharp.State
{
    /// <summary>
    /// 待機状態 (非加熱状態)
    /// </summary>
    public class IdleState : IState
    {
        /// <summary>
        /// 自己状態変数
        /// </summary>
        public static readonly IState sIntance = new IdleState();

        public IdleState()
        {
        }

        public IState PushPowerBtnEvent()
        {
            return PowerOffState.sIntance;
        }

        public IState PushStopBtnEvent()
        {
            return this;
        }

        public IState PushHeatBtnEvent()
        {
            return HeatState.sIntance;
        }

        public IState MeasureTemperatureEvent(HeaterContext someHeaterContext)
        {
            return this;
        }
    }
}

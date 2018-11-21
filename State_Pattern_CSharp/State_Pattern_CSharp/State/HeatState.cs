namespace State_Pattern_CSharp.State
{
    /// <summary>
    /// 加熱状態
    /// </summary>
    public class HeatState : IState
    {
        /// <summary>
        /// 自己状態変数
        /// </summary>
        public static readonly IState sIntance = new HeatState();

        public HeatState()
        {
        }

        public IState PushPowerBtnEvent()
        {
            return this;
        }

        public IState PushStopBtnEvent()
        {
            return IdleState.sIntance;
        }

        public IState PushHeatBtnEvent()
        {
            return this;
        }

        public IState MeasureTemperatureEvent(HeaterContext someHeaterContext)
        {
            if (Thermometer.CurrentTemp >= someHeaterContext.MaxTemperature)
            {
                return WarmState.sIntance;
            }

            return this;
        }
    }
}

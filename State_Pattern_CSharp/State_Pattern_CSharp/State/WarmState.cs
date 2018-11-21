namespace State_Pattern_CSharp.State
{
    /// <summary>
    /// 保温状態 (非加熱状態)
    /// </summary>
    public class WarmState : IState
    {
        /// <summary>
        /// 自己状態変数
        /// </summary>
        public static readonly IState sIntance = new WarmState();

        public WarmState()
        {
        }

        public IState PushPowerBtnEvent()
        {
            return null;
        }

        public IState PushStopBtnEvent()
        {
            return IdleState.sIntance;
        }

        public IState PushHeatBtnEvent()
        {
            return HeatState.sIntance;
        }

        public IState MeasureTemperatureEvent(HeaterContext someHeaterContext)
        {
            if (Thermometer.CurrentTemp <= someHeaterContext.MinTemperature)
            {
                return HeatState.sIntance;
            }

            return sIntance;
        }
    }
}

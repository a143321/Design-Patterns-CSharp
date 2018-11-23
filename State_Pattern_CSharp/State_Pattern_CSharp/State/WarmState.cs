namespace State_Pattern_CSharp.State
{
    /// <summary>
    /// 保温状態 (非加熱状態)
    /// </summary>
    public class WarmState : IState
    {
        public WarmState()
        {
        }

        public IState PushPowerBtnEvent()
        {
            return this;
        }

        public IState PushStopBtnEvent()
        {
            return new IdleState();
        }

        public IState PushHeatBtnEvent()
        {
            return new HeatState();
        }

        public IState MeasureTemperatureEvent(HeaterContext someHeaterContext)
        {
            if (Thermometer.CurrentTemp <= someHeaterContext.MinTemperature)
            {
                return new HeatState();
            }

            return this;
        }
    }
}

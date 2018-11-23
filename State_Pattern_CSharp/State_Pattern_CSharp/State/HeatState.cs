namespace State_Pattern_CSharp.State
{
    /// <summary>
    /// 加熱状態
    /// </summary>
    public class HeatState : IState
    {
        public HeatState()
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
            return this;
        }

        public IState MeasureTemperatureEvent(HeaterContext someHeaterContext)
        {
            if (Thermometer.CurrentTemp >= someHeaterContext.MaxTemperature)
            {
                return new WarmState();
            }

            return this;
        }
    }
}

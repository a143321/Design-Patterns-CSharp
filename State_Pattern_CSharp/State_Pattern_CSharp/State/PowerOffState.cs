namespace State_Pattern_CSharp.State
{
    /// <summary>
    /// 電源OFF状態
    /// </summary>
    public class PowerOffState : IState
    {
        public PowerOffState()
        {
        }

        public IState PushPowerBtnEvent()
        {
            return new IdleState();
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

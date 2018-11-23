namespace State_Pattern_CSharp.State
{
    /// <summary>
    /// 待機状態 (非加熱状態)
    /// </summary>
    public class IdleState : IState
    {
        public IdleState()
        {
        }

        public IState PushPowerBtnEvent()
        {
            return new PowerOffState();
        }

        public IState PushStopBtnEvent()
        {
            return this;
        }

        public IState PushHeatBtnEvent()
        {
            return new HeatState();
        }

        public IState MeasureTemperatureEvent(HeaterContext someHeaterContext)
        {
            return this;
        }
    }
}

namespace Assets.Scripts.PlayerUnits.UnitFiniteStateMachine
{
    internal abstract class FSMState
    {
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}
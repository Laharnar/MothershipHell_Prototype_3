using UnityEngine;

[CreateAssetMenu]
public class GameConditionalEvent : GameEvent {
    [SerializeField] IntReference Value1;
    [SerializeField] Operators Operator;
    [SerializeField] IntReference Value2;
    [SerializeField] bool lastResult;

    public override void Raise()
    {
        lastResult = false;
        switch (Operator)
        {
            case Operators.Equals:
                if (Value1.Value == Value2.Value) {
                    lastResult = true;
                }
                break;
            case Operators.LessThanOrEquals:
                if (Value1.Value <= Value2.Value) {
                    lastResult = true;
                }
                break;
            case Operators.MoreThanOrEquals:
                if (Value1.Value >= Value2.Value)
                {
                    lastResult = true;
                }
                break;
            case Operators.MoreThan:
                if (Value1.Value > Value2.Value)
                {
                    lastResult = true;
                }
                break;
            case Operators.LessThan:
                if (Value1.Value < Value2.Value)
                {
                    lastResult = true;
                }
                break;
            case Operators.NotEquals:
                if (Value1.Value != Value2.Value)
                {
                    lastResult = true;
                }
                break;
            default:

                break;
        }
        if(lastResult)
            base.Raise();
    }
}


public class EnemyAttribute : BaseAttribute
{
    protected override float GetBaseValue(AttributeType attr)
    {
        switch (attr)
        {
            case AttributeType.CurHp:
                return 30;
            case AttributeType.MaxHp:
                return 30;
            default:
                return 0;
        }
    }
}

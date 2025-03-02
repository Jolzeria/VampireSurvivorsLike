
public class CharacterAttribute : BaseAttribute
{
    protected override float GetBaseValue(AttributeType attr)
    {
        switch (attr)
        {
            case AttributeType.CurHp:
                return 50;
            case AttributeType.MaxHp:
                return 50;
            case AttributeType.HpLevel:
                return 0;
            case AttributeType.Exp:
                return 0;
            case AttributeType.Level:
                return 1;
            case AttributeType.MoveSpeed:
                return 2;
            case AttributeType.MoveSpeedLevel:
                return 0;
            case AttributeType.PickupRange:
                return 1.5f;
            case AttributeType.PickupRangeLevel:
                return 0;
            case AttributeType.MaxWeapons:
                return 3;
            case AttributeType.MaxWeaponsLevel:
                return 0;
            default:
                return 0;
        }
    }
}

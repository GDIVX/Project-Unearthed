public static class DropChanceDeterminer
{
    public static bool IsItemDrop(IEnemyDropItem dropItem)
    {
        return dropItem.WillDrop();
    }
}

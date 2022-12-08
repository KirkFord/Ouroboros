using UnityEngine;

public class chestanimationhandle : MonoBehaviour
{
    [SerializeField] private Puzzle2 puz2;

    public void ChestOpen()
    {
        puz2.SpawnRewardCoins();
    }
}

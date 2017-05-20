using System.Collections;

using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public void Start()
    {

    }
    public void PlayerHitCheckpoint()
    {

    }
    private IEnumerator PlayerHitCheckPointCo(int bonus)
    {
        yield break;
    }
    public void PlayerLeftCheckpoint()
    {

    }
    public void SpawnPlayer(GameObject Ship)
    {
        Ship.GetComponent<ShipController>().RespawnAt(transform);
    }
    public void AssignObjectToCheckPoint()
    {

    }
}

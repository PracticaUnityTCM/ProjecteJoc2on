using UnityEngine;

public class InstaKill : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ship") { 
        var player = collision.GetComponent<ShipController>();
        if (player == null)
            return;
        LevelManager.Instance.KillPlayer(true);
        }
       
    }
}

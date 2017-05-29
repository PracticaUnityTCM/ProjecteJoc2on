using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HeathBarEnemyType{
    Turret,
    Boat
}
public class HealthBarEnemy : MonoBehaviour {
     EnemyController Enemy;
   TurretController Obj;
    public HeathBarEnemyType HealthBarType;
    public Transform ForegroundSprite;
    public SpriteRenderer ForegroundRenderer;
    public Color MaxHealthColor = new Color(255 / 255f, 63 / 255f, 63 / 255f);

    public Color MinHealthColor = new Color(64 / 255f, 137 / 255f, 255 / 255f);
    // Use this for initialization
    void Start() {
        if(HealthBarType==HeathBarEnemyType.Boat)
        Enemy = transform.parent.parent.GetComponent<EnemyController>();
        else if(HealthBarType == HeathBarEnemyType.Turret)
            Obj = transform.parent.parent.GetComponent<TurretController>();
    }
    float HealthPercent;
    // Update is called once per frame
    void Update () {
        if (HealthBarType == HeathBarEnemyType.Boat)
            HealthPercent = Enemy.Health / (float)Enemy.MaxHealth;
        else if (HealthBarType == HeathBarEnemyType.Turret)
             HealthPercent = Obj.Health / (float)Obj.MaxHealth;

        //TextHealth.text = string.Format("{0} %", HealthPercent * 100);
        ForegroundSprite.localScale = new Vector3(HealthPercent, 1, 1);
        ForegroundRenderer.color = Color.Lerp(MaxHealthColor, MinHealthColor, HealthPercent);
    }
}

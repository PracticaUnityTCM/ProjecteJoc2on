using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour {
    public ShipController Ship;
    public Transform ForegroundSprite;
    public SpriteRenderer ForegroundRenderer;
    public Text TextHealth;
    public Color MaxHealthColor = new Color(255 / 255f, 63 / 255f, 63 / 255f);

    public Color MinHealthColor = new Color(64 / 255f, 137 / 255f, 255 / 255f);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float HealthPercent = Ship.Health / (float)Ship.MaxHealth;
        TextHealth.text = string.Format("{0} %",HealthPercent*100);
        ForegroundSprite.localScale = new Vector3(HealthPercent, 1, 1);
        ForegroundRenderer.color = Color.Lerp(MaxHealthColor, MinHealthColor, HealthPercent);
	}
}

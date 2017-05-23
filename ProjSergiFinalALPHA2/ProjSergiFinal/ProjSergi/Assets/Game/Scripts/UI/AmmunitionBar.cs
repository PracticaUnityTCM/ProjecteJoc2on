    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmunitionBar : MonoBehaviour {
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
        float AmunitionPercent = Ship.amunnition / (float)Ship.maxAmunition;
        TextHealth.text = string.Format("{0} ",Ship.amunnition);
        ForegroundSprite.localScale = new Vector3(AmunitionPercent, 1, 1);
        ForegroundRenderer.color = Color.Lerp(MaxHealthColor, MinHealthColor, AmunitionPercent);
	}
}

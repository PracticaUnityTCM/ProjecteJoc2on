using UnityEngine;
using System.Collections;

public class AgentSpawner : MonoBehaviour {

    public GameObject m_agentPrefab;
    public int m_amountToSpawn;
    protected WaypointManager m_waypointManager;
    public float spawnInterval = 1.25f;
    protected int m_spawned = 0;
    Transform m_spawnPoint;
    public bool m_infinite = false;

	// Use this for initialization
	void Start () {
         m_waypointManager = GetComponent<WaypointManager>();
        int index = transform.GetSiblingIndex();
         m_spawnPoint = transform.parent.GetChild(index + 1).gameObject.transform;
        SpawnEnemi();
        
	}
    public void SpawnEnemi()
    {
        StartCoroutine(Spawn());
    } 
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnInterval);

        m_waypointManager.AddEntity((GameObject)Instantiate(m_agentPrefab, m_spawnPoint.position, m_spawnPoint.rotation));
    }

}

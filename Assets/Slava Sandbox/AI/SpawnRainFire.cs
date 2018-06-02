using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunningRiot;

[RequireComponent(typeof(RunningRiot.Boss))]
public class SpawnRainFire : MonoBehaviour {
    public GameObject fireball;
    public Vector3 center;
    public Vector3 size;
    public float heightOfInstantiating = 20f;
    public float timeOfInstantiating = 2f;
    public float timeToSwitchToPhase = 30f;

    private RunningRiot.Boss boss;
	// Use this for initialization
	void Start () {
        boss = GetComponent<RunningRiot.Boss>();
        StartCoroutine(SpawnFireBalls());
        StartCoroutine(SwitchToPhaseThree());
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    IEnumerator SpawnFireBalls()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x/2,size.x/2),0, Random.Range(-size.z / 2, size.z / 2));
        GameObject fireballCopy = Instantiate(fireball,pos,Quaternion.identity);
        fireballCopy.GetComponentInChildren<SimpleProjectile>().speed = 15;
        Destroy(fireballCopy,3);
        yield return new WaitForSeconds(timeOfInstantiating);
        if(enabled)
            StartCoroutine(SpawnFireBalls());
    }
    IEnumerator SwitchToPhaseThree()
    {
        yield return new WaitForSeconds(timeToSwitchToPhase);
        GetComponent<RunningRiot.Boss>().currPhase = RunningRiot.Boss.Phase.Third;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255,164,0,0.5f);
        Gizmos.DrawCube(center,size);
    }
}

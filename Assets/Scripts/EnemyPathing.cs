using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    WaveConfig waveConfig;
     List<Transform> waypoint;

    int waypointIndex = 0;
	// Use this for initialization
	void Start () 
    {
        waypoint = waveConfig.GetWayPoints();
        transform.position = waypoint[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }


    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void Move()
    {
        if (waypointIndex <= waypoint.Count - 1)
        {
            var targetPos = waypoint[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);

            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

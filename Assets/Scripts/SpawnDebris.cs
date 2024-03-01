using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnDebris : MonoBehaviour
{
    [SerializeField] GameObject debris;
    [SerializeField] GameObject player;
    [SerializeField] GravityManager gravity;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(gravity.Objects.Count > 0){
            for(int i = 0; i < gravity.Objects.Count; i++)
            {
                GameObject debrisInstance = (GameObject)gravity.Objects[i];

                if(Vector2.Distance(Vector2.zero, debrisInstance.transform.position) < 10)
                {
                    if((Object)gravity.Objects[i] == player){
                        continue;
                    }

                    gravity.Objects.RemoveAt(i);
                    Destroy(debrisInstance);
                }
            }
        }
    }

    void Spawn(){
        // calculate player's angle from origin
        float player_angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg;

        //randomly spawn debris outside radius 30 of origin
        float angle = Random.Range(player_angle - 45, player_angle + 45);
        float radius = 30;
        Vector2 spawnPos = new Vector2(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad));

        // rotation should face origin around z axis
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-spawnPos.y, -spawnPos.x) * Mathf.Rad2Deg + 90);
            
        GameObject debrisInstance = Instantiate(debris, spawnPos, rotation, this.transform);
        Rigidbody2D rb = debrisInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.Perpendicular(new Vector2(-spawnPos.normalized.x, -spawnPos.normalized.y) * 300), ForceMode2D.Impulse);
        gravity.Objects.Add(debrisInstance);
    }
}

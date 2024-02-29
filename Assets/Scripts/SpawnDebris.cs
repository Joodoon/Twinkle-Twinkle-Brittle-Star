using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnDebris : MonoBehaviour
{
    [SerializeField] GameObject debris;

    ArrayList debrisList = new ArrayList();

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //randomly spawn debris outside the view of the camera
            Vector3 spawnPos = new Vector3(transform.position.x + UnityEngine.Random.Range(10, 20), transform.position.y + UnityEngine.Random.Range(10, 20), 0);

            // rotation should face origin around z axis
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-spawnPos.y, -spawnPos.x) * Mathf.Rad2Deg);
            
            GameObject debrisInstance = Instantiate(debris, spawnPos, rotation, this.transform);
            Rigidbody2D rb = debrisInstance.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-spawnPos.normalized.y, -spawnPos.normalized.x), ForceMode2D.Impulse);
            debrisList.Add(debrisInstance);
        }

        for(int i = 0; i < debrisList.Count; i++)
        {
            GameObject debrisInstance = (GameObject)debrisList[i];
            Rigidbody2D rb = debrisInstance.GetComponent<Rigidbody2D>();

            Vector2 forceDirection = new Vector2(-debrisInstance.transform.position.normalized.x, -debrisInstance.transform.position.normalized.y) * .5f;
            Vector2 globalForceDirection = rb.GetRelativeVector(forceDirection);

            rb.AddForce(globalForceDirection, ForceMode2D.Force);

            // Update velocity directly
            Vector2 newVelocity = new Vector2(1, 0); // replace with your desired velocity
            Vector2 globalVelocity = rb.GetRelativeVector(newVelocity);
            rb.velocity = globalVelocity;

            // Adjust velocity based on the world's rotation speed
            float worldRotationSpeed = 1; // replace with your world's rotation speed
            rb.velocity += new Vector2(-worldRotationSpeed * debrisInstance.transform.position.y, worldRotationSpeed * debrisInstance.transform.position.x);
        }
    }
}

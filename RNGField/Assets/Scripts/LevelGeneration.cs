using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    //Array of game objects that can be instantiated
    //Recommended that the objects are arranged by increasing height just cause it looks good in order
    public GameObject[] obj;

    void Start()
    {
        int rand = Random.Range(0, obj.Length);
        float size = obj[rand].GetComponent<Renderer>().bounds.size.y; //get the height of the object
        if (rand > 0) //element 0 of the array should be a dublicate of another object already in the array
        {
            if (size > 1) //move the y position of the object so it aligns with the ground (it doesnt collide with the ground)
            {
                Instantiate(obj[rand], new Vector3(transform.position.x, transform.position.y + ((size - 1) / 2), 0), Quaternion.identity); 
            }
            else //no need for replacing the position since its a 1x1 cube 
            {
                Instantiate(obj[rand], transform.position, Quaternion.identity);
            }
            Debug.Log("Size = " + size);
        }
        else Debug.Log("Size = 0");
    }
}

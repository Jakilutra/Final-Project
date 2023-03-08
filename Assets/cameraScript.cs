using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject playerdiamond; // Making a public variable for Player

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerdiamond.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerdiamond.transform.position + offset; // Make Camera to player's transform position
    }
}

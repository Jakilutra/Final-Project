using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour

   // Making a public variable for Player.

{
    public GameObject playerDiamond; 

    private Vector3 offset;

    // Start is called before the first frame update.

    void Start()
    {
        offset = transform.position - playerDiamond.transform.position;
    }

    // Update is called once per frame.

    void LateUpdate()
    {
        // Make Camera match player's transform position.

        transform.position = playerDiamond.transform.position + offset;

    }
}

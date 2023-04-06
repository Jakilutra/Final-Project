using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour

   // Making a public variable for player.

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

    // Camera shake method.

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }
}

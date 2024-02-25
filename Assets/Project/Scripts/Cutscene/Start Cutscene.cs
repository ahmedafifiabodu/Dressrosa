using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STARTcUTsCENE : MonoBehaviour
{
    public Cutscene scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            scene.StartCutscene();
            Destroy(this.gameObject);
        }
    }
}

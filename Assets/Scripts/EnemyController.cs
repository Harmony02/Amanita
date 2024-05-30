using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Interactable
{
    // Start is called before the first frame update

    void OnDestroy()
    {
        Debug.Log("i am ded");
        
    }
}

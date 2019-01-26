using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Singleton.Init();

        yield return new WaitForSeconds(1);

        Singleton.instance.gameEvent.gameOver.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

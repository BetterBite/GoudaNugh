using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureCabinet : FutureObject
{
    private CabinetVariables vars;
    public LockableObject door;
    public LockableObject drawer;

    public GameObject doorUnlocks;
    public GameObject drawerUnlocks;
    public override void Setup()
    {
       vars = networkObject.GetComponent<CabinetVariables>();
        vars.isLocked.OnValueChanged += Unlock;
    }

    private void Unlock(bool wasLocked, bool isLocked)
    {
        if (!isLocked)
        {
            door.Unlock();
            drawer.Unlock();

            doorUnlocks.SetActive(true);
            drawerUnlocks.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

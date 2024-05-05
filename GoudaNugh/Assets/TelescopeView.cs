using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelescopeView : MonoBehaviour
{
    //public Transform upstairs;    // transform to teleport player to when moving upstairs
    //public Transform player;
    public int imageindex;

    public Image[] blockScreen;
    private Camera mainCamera;
    public Camera telescopeCamera;
    public FadeScreen fadeScreen;
    void Start()
    {

        mainCamera = VRRigReferences.Singleton.mainCamera;
        fadeScreen = mainCamera.GetComponentInChildren<FadeScreen>();
        //imageindex = 0;
        ////Color color = blockScreen.color;
        ////color.a = 1;
        //for (int i = 0; i < 3; i++ )
        //{
        //    blockScreen[i].CrossFadeAlpha(0, 0f, true);
        //}
        
    }

    public void TelescopeCameraView()
    {
        fadeScreen.Fade(0,1);
        mainCamera.enabled = false;
        telescopeCamera.enabled = true;
        fadeScreen.Fade(1, 0);
    }

    public void MainCameraView()
    {
        fadeScreen.Fade(0, 1);
        telescopeCamera.enabled = false;
        mainCamera.enabled = true;
        fadeScreen.Fade(1, 0);
    }

    private void Update()
    {
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadioTimer : MonoBehaviour
{
    public float time;
    public Image[] Fill;
    public TMP_Text[] numbers;
   
    public float Max;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    public void ResetTimers()
    {
       foreach(TMP_Text text in numbers)
        {
            text.color = Color.yellow;
        }

       foreach(Image image in Fill)
        {
            image.fillAmount = 0;
        }
    }

    public void NextNumber(int level)
    {
        if (level > 0) numbers[level-1].color = Color.green;
        //Fill[level].enabled = true;
        //fill2(level);
        StartCoroutine(fill(level));
        return;
    }

    private IEnumerator fill(int level)
    {
        Debug.Log("filling" + level);
        time = 0;
        Image image = Fill[level];
        while (time < Max)
        {
            //Debug.Log("in coroutine");
            time += Time.deltaTime;
            image.fillAmount = time / Max;
            yield return null;
        }
    }

    private void fill2(int level)
    {
        time = 0;
        
        while (time < Max)
        {
            Debug.Log(Fill[level].fillAmount);
            time += Time.deltaTime;
            Fill[level].fillAmount = time / Max;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

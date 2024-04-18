using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureBook : FutureObject
{

    private BookVariables vars;
    private Book book;
    public GameObject lockObject;
    public override void Setup()
    {
        vars = networkObject.GetComponent<BookVariables>();
        book = GetComponent<Book>();

        vars.isLocked.OnValueChanged += Solve;
    }

    private void Solve(bool wasLocked, bool isLocked)
    {
        if (!isLocked)
        {
            book.Unlock();
            lockObject.SetActive(false);
        }
    }


    
}

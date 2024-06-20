using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Item : MonoBehaviour
{
    public string itemname;
    public string Req1;
    public string Req2;

    public int Req1amount;
    public int Req2amount;
    public int NumOfReq;

    public Check_Item(string name, int Num, string req1, int req1amount, string req2, int req2amount)
    {
        itemname = name;
        NumOfReq = Num;
        Req1amount = req1amount;
        Req2amount = req2amount;
        Req1 = req1;
        Req2 = req2;

    }


 }

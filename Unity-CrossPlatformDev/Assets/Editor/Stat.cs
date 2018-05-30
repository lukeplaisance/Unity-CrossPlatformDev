using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public string Name { get; set; }
    public int Value { get; set; }
    public string Description { get; set; }

    public Stat()
    {

    }
    public override string ToString()
    {
        var data = "Name: " + Name + "\n" +
                   "Value: " + Value + "\n";
        return data;
    }
}

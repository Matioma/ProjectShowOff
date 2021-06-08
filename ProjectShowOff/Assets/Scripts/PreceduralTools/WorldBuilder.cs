using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour, IProcedural
{
    [SerializeField]
    public static int seed;


    public static int SEED{
            get{return seed;}
            private set { seed = value; }
    }


    System.Random r;


    [SerializeField]
    GameObject terrain;

    private void Awake()
    {
        r = new System.Random(SEED);
    }


    public void Generate()
    {

    }

}

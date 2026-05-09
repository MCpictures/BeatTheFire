using UnityEngine;

public class Attackable : MonoBehaviour
{
    public void Attacked()
    {
        Debug.Log(name + " was attacked!");
    }
}

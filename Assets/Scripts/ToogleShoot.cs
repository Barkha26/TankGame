using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleShoot : MonoBehaviour
{
    public void ToggleTankShooting()
    {
        FindObjectOfType<PlayerManager>().ToggleTankShooting();
    }
}

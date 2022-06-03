using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public void SelectLevel()
    {
        LevelSelector.instance.Select(transform.name);
    }
}

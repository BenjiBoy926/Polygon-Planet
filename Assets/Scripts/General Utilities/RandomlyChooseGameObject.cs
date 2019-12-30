using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS RandomlyChooseGameObject
 * ------------------------------
 * Very simple class is given a list of game objects and chooses one
 * from the list to activate and deactivates the rest
 * ------------------------------
 */ 

public class RandomlyChooseGameObject : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objects;

    public void Choose()
    {
        if (objects.Count > 0)
        {
            int selectedObject = Random.Range(0, objects.Count);

            for (int index = 0; index < objects.Count; ++index)
            {
                if (index != selectedObject)
                {
                    objects[index].SetActive(false);
                }
            }

            objects[selectedObject].SetActive(true);
        }
    }
}

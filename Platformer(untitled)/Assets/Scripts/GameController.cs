using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int[][] level = new int[][]
    {
        new int[] {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,3,0,0,0,0,0,0,0,0,3,3,3,0,0,0,0,0,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1},
        new int[] {1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1},
        new int[] {1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,0,0,0,0,0,3,0,3,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,1,1,1,1,1,1,0,0,0,1,1,1,1,0,0,0,0,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,3,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
        new int[] {1,0,0,0,0,0,0,0,3,0,0,0,0,0,0,1,1,1,1},
        new int[] {1,0,0,0,0,0,0,1,1,1,1,0,0,1,1,1,1,1,1},
        new int[] {1,0,0,0,3,0,0,0,0,0,0,0,0,1,1,1,1,1,1},
        new int[] {1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1},
        new int[] {1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,0,2,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1},
        new int[] {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
    };
    
    [Header("Object Reference")]
    public Transform wall;
    public Transform character;
    //public GameObject character;
    public Transform orb;


    //builds the level
    void BuildLevel()
    {
        //Get the Dynamic Objects and make it the parent 
        GameObject dynamicParent = GameObject.Find("Dynamic Objects");
        //Go through each element inside our level variable
        for(int yPos = 0; yPos < level.Length; yPos++)
        {
            for(int xPos = 0; xPos < (level[yPos]).Length; xPos++)
            {
                //do nothing if the value is 0
                //if the value is 1, we want a wall
                /*if(level[yPos][xPos] == 1)
                {
                    //create the wall 
                    Transform newObject = Instantiate(wall, new Vector3(xPos, (level.Length - yPos), 0), wall.rotation) as Transform;
                    //set the object's parent to the DynamicObjects variable so it doesn't clutter
                    newObject.parent = dynamicParent.transform;
                }*/

                //try switch method
                Transform toCreate = null;
                switch(level[yPos][xPos])
                {
                    case 0:
                        //Do nothing 
                        break;
                    case 1:
                        toCreate = wall;
                        break;
                    case 2:
                        toCreate = character;
                        break;
                    case 3:
                        toCreate = orb;
                        break;
                    default:
                        print("Invalid Number: " + (level[yPos][xPos]).ToString());
                        break;
                }
                if(toCreate != null)
                {
                    Transform newObject = Instantiate(toCreate, new Vector3(xPos, (level.Length - yPos), 0), toCreate.rotation) as Transform;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       // Instantiate(character, character.transform.position, Quaternion.identity);
        BuildLevel();
    }
}

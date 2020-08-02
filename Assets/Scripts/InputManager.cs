using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> TileList;
    SpawnerScript SpawnerManager;


    void Start()
    {
        TileList = this.GetComponentInParent<SpawnerScript>().TileList;
        SpawnerManager = this.GetComponentInParent<SpawnerScript>();

    }

    // Update is called once per frame


    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if ((touch.phase == TouchPhase.Began)){
                print("touching");
                print(touch.position);
                Ray raypos = Camera.main.ScreenPointToRay(touch.position);
                
                RaycastHit2D hit = Physics2D.Raycast(raypos.origin,raypos.direction);
                if (hit.collider!=null){
                    if(hit.collider.gameObject.GetComponent<SpriteRenderer>() && hit.collider.gameObject.GetComponent<TileController>().hit == false){
                        //Debug.DrawLine(hit.point, hit.point, Color.red, 5.0f, true);
                        hit.collider.gameObject.GetComponent<TileController>().Hit();
                        SpawnerManager.score++;
                    }

                }
            }
        }
    }
}

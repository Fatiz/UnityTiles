using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    // Start is called before the first frame update
    SpawnerScript manager;
    public SpriteRenderer spriterender;
    GameObject highesttile;
    Vector2 size;
    public bool hit;
    void Start()
    {
        manager = this.GetComponentInParent<SpawnerScript>();
        spriterender = this.GetComponent<SpriteRenderer>();
        highesttile = manager.HighestTile;
        size = manager.Size;
        hit = false;
    }


    public void Hit(){
        hit = true;
        spriterender.color = new Vector4(0f,0f,0f,0.5f);
    }
    public void Reset(){
        hit = false;
        spriterender.color = new Vector4(0f,0f,0f,1f);
    }

   
}

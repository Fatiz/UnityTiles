using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<float> InitHorizontalPoints = new List<float>();
    public List<float> InitVerticalPoints = new List<float>();
    public List<GameObject> TileList = new List<GameObject>();
    public Texture2D tex;
    public int score = 0;
    Text scoretext;
    public GameObject textobj;
    public float TileSpeed;
    public GameObject Tile;
    public GameObject HighestTile;


    Vector3 targetPos;

    Canvas maincanvas;
    RectTransform canvasTrans;
    public Vector2 Size;

    void TileSpeedUp(){
        if(score>0){
            if(TileSpeed < 15f){
                TileSpeed = TileSpeed * 1.1f;
                foreach(GameObject tile in TileList){
                    BoxCollider2D boxcollider =tile.GetComponent<BoxCollider2D>();
                    boxcollider.offset = new Vector2(boxcollider.offset.x, boxcollider.offset.y + TileSpeed*0.004f);
                }
            } else{
                TileSpeed = 15f;
            }
        }
    }
    void Start()
    {
        InvokeRepeating ("TileSpeedUp", 2, 5);
        maincanvas = gameObject.GetComponent<Canvas>();
        canvasTrans = gameObject.GetComponent<RectTransform>();
        Size = (Camera.main.ScreenToWorldPoint(canvasTrans.transform.position))*2;
        //print(Size);
        SetSpawnPoints(Size);
        scoretext = textobj.GetComponent<Text>();
        for(int i=0; i<8; i++){
            GameObject tileobj = Instantiate(Tile);
            SpriteRenderer renderer = tileobj.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0f, 0f), 32.0f);
            tileobj.transform.SetParent(maincanvas.transform);
            
            tileobj.transform.localScale = new Vector3(canvasTrans.rect.width/4, canvasTrans.rect.height/4,1);
            //print(canvasTrans.rect.width/4);
            //Size = Camera.main.ScreenToWorldPoint(tileobj.transform.localScale);
            //print((Size.y/2)-(i* (Size.y/4)));
            tileobj.transform.position = new Vector2(InitHorizontalPoints[Random.Range(0,4)], (Size.y/2)-(i* (Size.y/4)));
            TileList.Add(tileobj);
            tileobj.GetComponent<TileController>().enabled = true;
            if(i == 7){
                HighestTile = tileobj;
            }
        }
    
    }

    void SetSpawnPoints(Vector3 size){
        float TileWidth = size.x / -4;
        float TileHeight = size.y / -4;

        for(int i=0; i<4; i++){
            InitVerticalPoints.Add((-size.y/2)+TileHeight*i);
            InitHorizontalPoints.Add((size.x/2)+TileWidth*i);
        }

    }
    void ListSwap(){
        GameObject removedtemp = TileList[0];
        TileList.RemoveAt(0);
        TileList.Add(removedtemp);

    }
    void FixedUpdate(){
        float step = TileSpeed * Time.deltaTime;
        scoretext.text = score.ToString();
        GameplayContext.Score = score;
        if (score > 0){
            
            for(int i =0; i<TileList.Count; i++){
                Vector3 targetPos = new Vector3(TileList[i].transform.position.x, (Size.y)/2 + (Size.y/4), TileList[i].transform.position.z);
                //print(HighestTile.transform.position + "Highest tile pos");
                
                if(Vector3.Distance(targetPos, TileList[i].transform.position)<0.001f){ // WE KNOW THIS IS THE BOTTOM BLOCK
                    if(TileList[i].GetComponent<SpriteRenderer>().color.a == 1f){
                        SceneManager.LoadScene("GameOver");
                    }
                    //print("Hit position of targetPos: " + targetPos + " Reading position "+ TileList[i].transform.position );
                    //print(TileList.IndexOf(TileList[i]));
                    TileList[i].transform.position = new Vector3(InitHorizontalPoints[Random.Range(0,4)], HighestTile.transform.position.y-(Size.y/4), TileList[i].transform.position.z);
                    //print("Change is "+ (HighestTile.transform.position - (tile.transform.position)) +" Supposed to be" + Size.y/4);
                    //print("Setting position to " + tile.transform.position);
                    HighestTile = TileList[i];
                    TileList[i].GetComponent<TileController>().Reset();
                    ListSwap();
                    break;

                }
                

                TileList[i].transform.position = Vector3.MoveTowards(TileList[i].transform.position, targetPos, step);
                //print(tile.transform.position - before);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCanvas : MonoBehaviour {

    public float brushsize = 20;

    private Texture2D newTex;
    public Texture2D oldTex;
    public UnityEngine.UI.Text textPercent;
    public UnityEngine.UI.Image buttonContinue;

    public int scanned = 0;

    Vector2 lastpos = new Vector2(-1,-1);

    bool complete;
    

    void Start () {
        newTex = new Texture2D(oldTex.width,oldTex.height);       
        Color[] colors = oldTex.GetPixels(0, 0, oldTex.width, oldTex.height);
        newTex.SetPixels(colors);      
    }


    void Update () {
        if (Input.GetMouseButton(0))
        {            
            Vector2 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickedPosition, -Vector2.up);
            if (hit)
            {
                Vector2 hitPoint = this.transform.InverseTransformPoint(hit.point);

                BrushPlace(hitPoint);
               
                //float dist = Mathf.Sqrt(Mathf.Pow(hitPoint.x - lastpos.x,2) + Mathf.Pow(hitPoint.y - lastpos.y, 2));
                //Debug.Log("dist" + dist);
                //if (dist > 0.4f && lastpos.x != -1)
                //{
                  // InterpolateBrush(lastpos, hitPoint, dist);                    
                //}
                lastpos = hitPoint;
                UpdatePercent();
            }
        }

	}


    private void InterpolateBrush(Vector2 startpos, Vector2 endpos, float dist)
    {
        //defunct
    }

    private void BrushPlace(Vector2 hp)
    {
        float fy = hp.y * 100 + newTex.height / 2;
        float fx = hp.x * 100 + newTex.width / 2;
        int xx = (int)fx;
        int yy = (int)fy;
        int dx = 0;
        int dy = 0;
        for (int x = -(int)brushsize; x < brushsize; x++)
        {
            dx = Mathf.Clamp(xx + x,0,newTex.width-1);
            for (int y = -(int)brushsize; y < brushsize; y++)
            {
                dy = Mathf.Clamp(yy + y, 0, newTex.height -1);     
                if (newTex.GetPixel(dx, dy) != Color.clear)
                {
                    newTex.SetPixel(dx, dy, Color.clear);                                    
                    scanned++;
                }
                                            
            }
        }
        newTex.Apply();
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
        
    }

    private void OnMouseUp()
    {
        lastpos = new Vector2(-1, -1);
    }


    
    private void UpdatePercent()
    {        
        int percent = scanned / 512;
        percent *= 100;
        percent /= 300;
        if (percent < 99)
        {
            textPercent.text = "Percent Scanned: " + percent + "%";
        }
        else
        {
            textPercent.text = "Scan Complete!";
            buttonContinue.gameObject.SetActive(true);
            GetComponent<SpriteRenderer>().gameObject.SetActive(false);
        }
        
    }
}

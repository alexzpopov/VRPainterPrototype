using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.Collections.Generic;

public class Inventory // Used in Lists.
{
    public GameObject model { get; set; }
    public GameObject modelPainting { get; set; }
    public Inventory(GameObject newmodel, GameObject newmodelPainting)
    {
        model = newmodel;
        modelPainting = newmodelPainting;
    }
}

public class CaptureTexture : MonoBehaviour {

    public GameObject virtuCamera;
    public GameObject view;
    public GameObject display;
    GameObject paintObject;
    public Texture2D map;
    public GameObject canvasPoint;
    public GameObject canvas;
    public List<Inventory> paintingList = new List<Inventory>();
    // Use this for initialization
    void Start () {
        //display = null;
        //display.transform.parent = transform;
        //display.GetComponent<MeshRenderer>().material.mainTexture = map;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (view.GetComponent<ViewManager>().hotObject != null)                
            {
                //if (display == null)
                //{
                    paintObject = view.GetComponent<ViewManager>().hotObject;
                    {
                        //display = Instantiate(canvas, canvasPoint.transform.position, Quaternion.identity) as GameObject;
                        //display.transform.parent = canvasPoint.transform;
                        MakeSquarePngFromOurVirtualThingy(paintObject);
                    }
                //}
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (display!=null)
            {
                display.GetComponent<MeshRenderer>().material.mainTexture = map;
                //Instantiate(paintingList[paintingList.Count-1].model,display.transform.position,Quaternion.identity);
                paintingList[paintingList.Count - 1].model.transform.parent = null;
                //paintingList[paintingList.Count - 1].model.layer = default;
                paintingList[paintingList.Count - 1].model.SetActive(true);
                //paintingList[paintingList.Count - 1].model.GetComponent<ParticleSystem>().Play();
                //paintingList[paintingList.Count - 1].model.transform.position = display.transform.position + transform.forward * 3;
                //view.GetComponent<ViewManager>().hotObject.SetActive(true);
                //view.GetComponent<ViewManager>().hotObject.transform.position = transform.position + transform.forward * 3;
                paintingList.RemoveAt(paintingList.Count - 1);
                //display = null;
            }
        }

    }
    void OnMouseDown()
    {
        //Application.CaptureScreenshot("Screenshot.png");
       // Debug.Log("Mouseclick");
       // MakeSquarePngFromOurVirtualThingy();
    }
    public void MakeSquarePngFromOurVirtualThingy(GameObject currentObject)
    {
        // capture the virtuCam and save it as a square PNG.

        int sqr = 1024;

        virtuCamera.GetComponent<Camera>().aspect = 1.0f;
        // recall that the height is now the "actual" size from now on

        RenderTexture tempRT = new RenderTexture(sqr, sqr, 32);
        // the 24 can be 0,16,24, formats like
        // RenderTextureFormat.Default, ARGB32 etc.

        virtuCamera.GetComponent<Camera>().targetTexture = tempRT;
        virtuCamera.GetComponent<Camera>().Render();

        RenderTexture.active = tempRT;
        Texture2D virtualPhoto =
            new Texture2D(sqr, sqr, TextureFormat.ARGB32, false);
        // false, meaning no need for mipmaps
        virtualPhoto.ReadPixels(new Rect(0, 0, sqr, sqr), 0, 0, false);
        virtualPhoto.Apply();
        
        RenderTexture.active = null; //can help avoid errors 
        virtuCamera.GetComponent<Camera>().targetTexture = null;
        // consider ... Destroy(tempRT);
        
        byte[] bytes;
        bytes = virtualPhoto.EncodeToPNG();

        System.IO.File.WriteAllBytes(Application.dataPath + "\\Paintings\\" + currentObject.name + ".png", bytes);
        //Debug.Log(Application.dataPath + "/"+  currentObject.name + ".png");
        // virtualCam.SetActive(false); ... no great need for this.

        // now use the image, 
        //UseFileImageAt(OurTempSquareImageLocation());
        display.GetComponent<MeshRenderer>().material.mainTexture = virtualPhoto;
        paintingList.Add( new Inventory(view.GetComponent<ViewManager>().hotObject, display));
        display.GetComponent<MeshRenderer>().material.mainTexture = paintingList[paintingList.Count -1].modelPainting.GetComponent<MeshRenderer>().material.mainTexture as Texture;
        //UnityEditor.AssetDatabase.Refresh();
        if (view.GetComponent<ViewManager>().hotObject!=null) {
            view.GetComponent<ViewManager>().hotObject.SetActive(false);
            //view.GetComponent<ViewManager>().hotObject.GetComponent<ParticleSystem>().Pause();
            view.GetComponent<ViewManager>().hotObject.transform.parent = transform;
            //Destroy(view.GetComponent<ViewManager>().hotObject);
            view.GetComponent<ViewManager>().hotObject = null;
        }
        if (paintingList[0].modelPainting != null)
        {
            Debug.Log("Epic");
        }
        //Debug.Log("Model: " + paintingList[0].model.name + "Painting : " + paintingList[0].modelPainting.name);
    }

    private string OurTempSquareImageLocation()
    {
        string r = Application.persistentDataPath + "atlassss.png";
        return r;
    }

}

using UnityEngine;
//using UnityEditor;
using System.Collections;

public class ReadCamera : MonoBehaviour {
    public bool _grab = false;
    public Texture2D grabTexture;
    public Vector2 size = new Vector2(400.0f, 400.0f);
    public GameObject target;
    public LayerMask paintable;
   // public Camera readCamera;
    Color[] targetColor;

    // Use this for initialization
    private void Start()
    {
        GetComponent<Camera>().pixelRect = new Rect(0.0f, 0.0f, this.size.x, this.size.y);

        this.grabTexture = new Texture2D((int)this.size.x, (int)this.size.y, TextureFormat.ARGB32,true);
        _grab = false;
    }
    // Update is called once per frame
    void Update () {
        //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        //Debug.DrawRay(transform.position, forward, Color.green);
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, forward, out hit))
        //{
        //    if ((Input.GetKeyDown(KeyCode.F)) && hit.collider.gameObject.layer == paintable)
        //    {
        //        _grab = true;
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.F)){
            _grab = true;
        }

    }
    private void OnPostRender()
    {
        if (this._grab)
        {
            //yield new WaitForEndOfFrame();
            this.grabTexture.ReadPixels(new Rect(0, 0, this.size.x, this.size.y), 0, 0,true);
            //this.grabTexture.Apply();
            target.GetComponent<MeshRenderer>().material.mainTexture = this.grabTexture;
            this._grab = false;
            targetColor = new Color[(int)size.x * (int)size.y];
            //for (int i = 0; i < targetColor.Length; i++)
            //{
            //    if (targetColor[i] == new Color(255,255,255,5)) {
            //        targetColor[i] = Color.clear;
            //    }
            //}
            //grabTexture.SetPixels(targetColor);
            //grabTexture.Apply();
            //Rect[] rects = grabTexture.PackTextures(textures.ToArray(), 0, false);

            byte[] bytes = grabTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/atlas.png", bytes);
            //UnityEditor.AssetDatabase.Refresh();
            //TextureImporter tempImporter = (TextureImporter)AssetImporter.GetAtPath(Application.dataPath + "/atlas.png");
            targetColor = grabTexture.GetPixels();
            Debug.Log(targetColor[20].ToString());
            //if (tempImporter != null)
            //{

            //}
            //for (int i = 0; i < 128; i++)
            //{
            //    for (int j = 0; j < 128; j++)
            //    {
            //        Debug.Log(grabTexture.GetPixel(i, j).ToString());
            //    }
            //}
            // targetColor = grabTexture.GetPixels();
            //for (int i =0;i<targetColor.Length;i++)
            //{
            //    Debug.Log(targetColor[i].ToString());
            //}
        }
    }
}








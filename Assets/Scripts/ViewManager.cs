using UnityEngine;
using System.Collections;

public class ViewManager : MonoBehaviour {
    public GameObject hotObject;
    public LayerMask paintLayer;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hotObject!=null)
            {
                //hotObject.SetActive(false);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (hotObject == null)
        {
            if (other.tag == "Paintable")
            {
                hotObject = other.GetComponent<Collider>().gameObject;
                hotObject.layer = LayerMask.NameToLayer("Water");
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        hotObject = null;
    }
}

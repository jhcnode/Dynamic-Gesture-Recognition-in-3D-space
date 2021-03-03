using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepGes;
using UnityEngine.UI;


public class TextItem : MonoBehaviour {
    public string path;
    public string name;
    private VisualizationManager v_manager;



    // Use this for initialization
    void Start () {
        v_manager = GameObject.Find("Updater").GetComponent<VisualizationManager>();
        gameObject.GetComponent<Text>().text = name;



    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Select()
    {
        List<Vector3> v = GUtil.ReadData(path);
        v_manager.DrawGesture(ref v);
   

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepGes;
using UnityEngine.UI;
public class VisualizationManager : MonoBehaviour {
    public string path = null;
    //Line Renderer
    public LineRenderer currentGestureLineRenderer;
    public GameObject dot_prefab;
    private List<GameObject> dot_list = new List<GameObject>();
    private Canvas canvas;
    private GameObject scrollView;
    private InputField inputField;
    private bool isRun = false;

    public void CleanGesture()
    {

        for (int i = 0; i < dot_list.Count; ++i)
        {
            dot_list[i].transform.position = new Vector3(0, 0, 0);
            dot_list[i].SetActive(false);
        }
        currentGestureLineRenderer.SetVertexCount(0);
    }
    public void DrawGesture(ref List<Vector3> v_list)
    {
        CleanGesture();

        Text t=scrollView.GetComponent<ScrollViewManager>().dropdown.transform.Find("Label").GetComponent<Text>();

        if (t.text.Equals("Vector Mode") ==true)
        {
            DrawGestureDot(ref v_list);
        }
        else
        {
            DrawGestureLine(ref v_list);
        }
    }
    public void DrawGestureLine(ref List<Vector3> v_list)
    {
        for (int i=0; i< v_list.Count; ++i)
        {
            int ver_count = i + 1;
            currentGestureLineRenderer.SetVertexCount(ver_count);
            currentGestureLineRenderer.SetPosition(i, v_list[i]);


        }

    }

    public void DrawGestureDot(ref List<Vector3> v_list)
    {
        
        int count = v_list.Count;
        if (v_list.Count < GConfig.MaxPoint)
            v_list = GUtil.getSupplementVector(v_list);
        v_list = GUtil.EliminateVector(v_list);


        for (int i = 0; i < v_list.Count; ++i)
        {
            if(dot_list.Count< v_list.Count)
            {
                if(i>dot_list.Count-1)
                {
                    GameObject dot_obj = (GameObject)Instantiate(dot_prefab);
                    dot_obj.transform.position = v_list[i];
                    dot_list.Add(dot_obj);
                }
                else
                {
                    dot_list[i].transform.position = v_list[i];
                    dot_list[i].SetActive(true);

                }


            }
            else
            {
                dot_list[i].transform.position = v_list[i];
                dot_list[i].SetActive(true);
            }



        }

    }
    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        scrollView = canvas.transform.Find("Scroll View").gameObject;
        inputField = canvas.gameObject.transform.Find("InputField").GetComponent< InputField>();



    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            path = inputField.text;
            isRun = !isRun;
            inputField.interactable = !inputField.interactable;
            if (isRun)
                scrollView.GetComponent<ScrollViewManager>().CreateList(path);
            else
            {
                CleanGesture();
                scrollView.GetComponent<ScrollViewManager>().CleanList();
            }


        }



    }
}

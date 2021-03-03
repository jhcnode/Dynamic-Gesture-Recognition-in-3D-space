using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ScrollViewManager : MonoBehaviour {
    public List<TextItem> ItemList;
    public DirectoryInfo di;
    public GameObject prefab_item;
    private Canvas canvas;
    private GameObject viewport;
    private GameObject content;
    public GameObject dropdown;
    private VisualizationManager visualizationManager;
    
    public void CleanList()
    {
        foreach(TextItem item in ItemList)
        {
            item.path = null;
            item.name = null;
            item.gameObject.SetActive(false);

        }

    }
    public void CreateList(string path,bool multidir=false)
    {

        if (Directory.Exists(path) == true)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if(multidir==false)
            {
                int iter = 0;
                FileInfo[] files = di.GetFiles();
                Array.Sort<FileInfo>(files, new Comparison<FileInfo>(CompareFiles));
                foreach (FileInfo f in files)
                {
                    if (iter > ItemList.Count - 1)
                    {
                        GameObject item_obj = (GameObject)Instantiate(prefab_item);
                        item_obj.transform.SetParent(content.transform);
                        item_obj.transform.localPosition = new Vector3(0, 0, 0);
                        item_obj.GetComponent<TextItem>().path = f.FullName;
                        item_obj.GetComponent<TextItem>().name = f.Name;
                        TextItem item_comp = item_obj.GetComponent<TextItem>();
                        ItemList.Add(item_comp);
                    }
                    else
                        ItemList[iter].path = f.FullName;
                    ItemList[iter].name = f.Name;
                    ItemList[iter].gameObject.SetActive(true);


                    iter += 1;

                }

            }
            else
            {
                DirectoryInfo[] sub_di = di.GetDirectories();
                Array.Sort<DirectoryInfo>(sub_di, new Comparison<DirectoryInfo>(CompareFiles));
                int iter = 0;
                foreach (DirectoryInfo d in sub_di)
                {

                    DirectoryInfo raw_d = new DirectoryInfo(d.FullName + "/raw");
                    FileInfo[] files = raw_d.GetFiles();
                    Array.Sort<FileInfo>(files, new Comparison<FileInfo>(CompareFiles));
                    foreach (FileInfo f in files)
                    {
                        if (iter > ItemList.Count - 1)
                        {
                            GameObject item_obj = (GameObject)Instantiate(prefab_item);
                            item_obj.transform.SetParent(content.transform);
                            item_obj.transform.localPosition = new Vector3(0, 0, 0);
                            item_obj.GetComponent<TextItem>().path = f.FullName;
                            item_obj.GetComponent<TextItem>().name = f.Name;
                            TextItem item_comp = item_obj.GetComponent<TextItem>();
                            ItemList.Add(item_comp);
                        }
                        else
                            ItemList[iter].path = f.FullName;
                        ItemList[iter].name = f.Name;
                        ItemList[iter].gameObject.SetActive(true);


                        iter += 1;

                    }

                }

            }

        }

    }


    // Use this for initialization
    void Start () {
        visualizationManager = GameObject.Find("Updater").gameObject.GetComponent<VisualizationManager>();
        canvas = GameObject.Find("Canvas").gameObject.GetComponent<Canvas>();
        viewport = gameObject.transform.Find("Viewport").gameObject;
        content = viewport.transform.Find("Content").gameObject;
        dropdown = canvas.transform.Find("Dropdown").gameObject;


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    int CompareFiles(FileInfo a, FileInfo b)
    {
        string s1 = a.Name.ToString();
        string s2 = b.Name.ToString();
        char[] c1 = s1.ToCharArray();
        char[] c2 = s2.ToCharArray();
        int index1 = 0, index2 = 0;
        int count1 = c1.Length, count2 = c2.Length;
        long val1, val2;
        while (index1 < count1 && index2 < count2)
        {
            if (c1[index1] >= '0' && c1[index1] <= '9' && c2[index2] >= '0' && c2[index2] <= '9')
            {
                // 양쪽다 숫자인 경우 숫자를 하나의 문자로 보고 크기를 비교한다.
                val1 = c1[index1] - '0';
                index1++;
                while (index1 < count1 && c1[index1] >= '0' && c1[index1] <= '9')
                {
                    val1 *= 10;
                    val1 += c1[index1] - '0';
                    index1++;
                }
                val2 = c2[index2] - '0';
                index2++;
                while (index2 < count2 && c2[index2] >= '0' && c2[index2] <= '9')
                {
                    val2 *= 10;
                    val2 += c2[index2] - '0';
                    index2++;
                }
                if (val1 == val2) continue;
                else if (val1 < val2) return -1;
                else return 1;
            }
            else if (c1[index1] == c2[index2])
            {
                index1++;
                index2++;
                continue;
            }
            else if (c1[index1] < c2[index2]) return -1;
            else return 1;
        }
        if (index1 >= count1 && index2 >= count2) return 0;
        else if (index1 >= count1) return -1;
        else return 1;
    }
    int CompareFiles(DirectoryInfo a, DirectoryInfo b)
    {
        string s1 = a.Name.ToString();
        string s2 = b.Name.ToString();
        char[] c1 = s1.ToCharArray();
        char[] c2 = s2.ToCharArray();
        int index1 = 0, index2 = 0;
        int count1 = c1.Length, count2 = c2.Length;
        long val1, val2;
        while (index1 < count1 && index2 < count2)
        {
            if (c1[index1] >= '0' && c1[index1] <= '9' && c2[index2] >= '0' && c2[index2] <= '9')
            {
                // 양쪽다 숫자인 경우 숫자를 하나의 문자로 보고 크기를 비교한다.
                val1 = c1[index1] - '0';
                index1++;
                while (index1 < count1 && c1[index1] >= '0' && c1[index1] <= '9')
                {
                    val1 *= 10;
                    val1 += c1[index1] - '0';
                    index1++;
                }
                val2 = c2[index2] - '0';
                index2++;
                while (index2 < count2 && c2[index2] >= '0' && c2[index2] <= '9')
                {
                    val2 *= 10;
                    val2 += c2[index2] - '0';
                    index2++;
                }
                if (val1 == val2) continue;
                else if (val1 < val2) return -1;
                else return 1;
            }
            else if (c1[index1] == c2[index2])
            {
                index1++;
                index2++;
                continue;
            }
            else if (c1[index1] < c2[index2]) return -1;
            else return 1;
        }
        if (index1 >= count1 && index2 >= count2) return 0;
        else if (index1 >= count1) return -1;
        else return 1;
    }

}

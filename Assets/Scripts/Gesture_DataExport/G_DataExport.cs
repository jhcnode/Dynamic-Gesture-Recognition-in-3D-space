﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DeepGes;
using Leap;
using Leap.Unity;

public class G_DataExport : MonoBehaviour {
    //네트워크 오브젝트
    private GUser user;

    //Line Renderer
    public LineRenderer currentGestureLineRenderer;
    //Interpolation - Threshold
    public float pixel_Interpolation = 0.1f;
    //포인트 리스트
    private List<Vector3> points = new List<Vector3>();
    private List<Vector3> t_points = new List<Vector3>();
    //스트로크 ID
    private int strokeId = -1;
    private Rect drawArea;
    private int vertexCount = 0;
    //Line Renderer List
    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private Vector3 prePos;
    private Vector3 currPos;
    private int currFingerId = 0;

    private bool endFlag;

    //Input 관련 변수
    public bool isInit = false;
    public bool isStart = false;
    public bool isPress = false;
    public bool isEnd = false;


    private bool systemTrigger = false;
    private bool startTrigger = false;
    private InputField label_InputField;

    private LeapProvider provider;

    public GameObject[] palm;

    public bool isTransform = false;


    //프로그램 시작 시 초기화
    void Awake()
    {
        
        provider = GameObject.FindObjectOfType<LeapProvider>();




        label_InputField = GameObject.Find("Label(InputField)").GetComponent<InputField>();
        user = gameObject.GetComponent<GUser>();

        //변수 초기화 및 생성
        currPos = new Vector3();
        prePos = new Vector3();
    }


    //게임 업데이트
    void Update()
    {

        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKeyDown("return"))
        {
            if (systemTrigger == false)
                systemTrigger = true;
            else
            {
                startTrigger = false;
                systemTrigger = false;
            }


        }
        label_InputField.interactable = !systemTrigger;

        if (systemTrigger == true)
        {

            if (Input.GetKeyDown(KeyCode.Space) == true) startTrigger = !startTrigger;


            GestureDraw(int.Parse(label_InputField.text),palm[1].transform.position, startTrigger,Camera.main.transform, isTransform);

        }


    }

    //게임 업데이트 - Late
    void LateUpdate()
    {
        isEnd = false;
        isStart = false;
    }


    //컨트롤러 제스처 Draw 처리, 라벨링/ 위치 값/ 활성화 트리거(true = 시작 , false= 끝) / 플레이어 머리 transform 
    public void GestureDraw(int label, Vector3 position, bool active,Transform head,bool isTransform)
    {
        
        bool state = active;
        if (state == true)
        {
            if (isStart == false && isInit == false)
            {
                isStart = true;
                isInit = true;
            }
            isEnd = false;
        }
        else
        {
            if (isPress == true) isEnd = true;
            isPress = false;
        }

        currPos = position;
        Vector3 t_currPos = currPos;
        if (isTransform==true)
            t_currPos = user.getLocalizedPoint(currPos, head);

        //클릭이 시작된 경우(클릭하는 순간)
        if (isStart == true)
        {
            //Debug.Log("start");
            vertexCount = 0;    //버텍스카운트 0
            points.Clear();     //포인트 리스트 클리어
            t_points.Clear();
            currentGestureLineRenderer.SetVertexCount(0);       //Line Renderer 초기화
            points.Add(currPos);            //Current Pos 추가
            t_points.Add(t_currPos);
            prePos = currPos;
            isPress = true;
        }
        //누르고 있는 경우
        if (isPress == true)
        {
            //이전 Pos와 현재 Pos의 거리구해서 points 추가
            if (Vector3.Distance(currPos, prePos) > 0.001f)
            {
                points.Add(currPos);
                t_points.Add(t_currPos);
            }
            //points의 개수가 1개 이상일 경우1
            if (points.Count > 0)
            {
                //points가 2개 이상인 경우
                if (points.Count > 1)
                {
                    if (points.Count > vertexCount)
                    {

                        //가장 최근(마지막)에 들어온 벡터 사이를 threshold(pixel_Interpolation)에 따라  보간 
                        GUtil.getInterpolationBetweenLastVector(ref points, pixel_Interpolation);
                        //가장 최근(마지막)에 들어온 벡터 사이를 threshold(pixel_Interpolation)에 따라  보간 
                        GUtil.getInterpolationBetweenLastVector(ref t_points, pixel_Interpolation);

                        //Line Renderer에 적용
                        int start = vertexCount;
                        for (int i = start; i < points.Count; i++)
                        {
                            vertexCount += 1;
                            currentGestureLineRenderer.SetVertexCount(vertexCount);
                            currentGestureLineRenderer.SetPosition(vertexCount - 1, points[vertexCount - 1]);
                        }
                    }
                }
                //points가 1개인 경우
                else
                {
                    vertexCount = 1;
                    currentGestureLineRenderer.SetVertexCount(vertexCount);
                    currentGestureLineRenderer.SetPosition(vertexCount - 1, points[points.Count - 1]);
                }
            }
        }
        //클릭이 끝난 경우(끝나는 순간)
        if (isEnd == true)
        {
            if (points.Count > 1)
            {
                GUtil.SaveData(ref points, ref t_points,label);
                Debug.Log("save");
            }

            isInit = false;
        }
        prePos = currPos;
            
        

    }

}

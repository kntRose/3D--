using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInner : MonoBehaviour {
    public ParticleSystem parSystem;   //粒子系统
    private ParticleSystem.Particle[] parArray; //粒子数组
    private CirclePosition[] cirPosition;   //极坐标数组
    //public Gradient colorGradient;

    public int count = 10000;       //粒子数量
    public float size = 0.03f;      //粒子大小
    public float minRadius = 5.0f;  //最小半径
    public float maxRadius = 12.0f; //最大半径
    public bool clockWise = false;   //顺时针|逆时针
    public float speed = 2f;        //速度
    public float distance = 0.02f;  //游离范围
    private int tier = 10;  // 速度差分层数 

    // Use this for initialization
    void Start()
    {
        parArray = new ParticleSystem.Particle[count];
        cirPosition = new CirclePosition[count];

        parSystem = this.GetComponent<ParticleSystem>();
        parSystem.startSpeed = 0;       //粒子位置由程序控制
        parSystem.startSize = size;     //设置粒子大小
        parSystem.loop = false;
        parSystem.maxParticles = count; //设置最大粒子量
        parSystem.Emit(count);          //发射粒子
        parSystem.GetParticles(parArray);

        RandomlySpread();               //初始化各粒子位置
    }

    void RandomlySpread()
    {
        for (int i = 0; i < count; ++i)
        {   // 随机每个粒子距离中心的半径，同时希望粒子集中在平均半径附近  
            float midRadius = (maxRadius + minRadius) / 2;
            float minRate = Random.Range(1.0f, midRadius / minRadius);
            float maxRate = Random.Range(midRadius / maxRadius, 1.0f);
            float radius = Random.Range(minRadius * minRate, maxRadius * maxRate);

            // 随机每个粒子的角度  
            float angle = Random.Range(0.0f, 360.0f);
            float theta = angle / 180 * Mathf.PI;

            // 随机每个粒子的游离起始时间  
            float time = Random.Range(0.0f, 360.0f);

            cirPosition[i] = new CirclePosition(radius, angle, time);

            parArray[i].position = new Vector3(cirPosition[i].radius * Mathf.Cos(theta), 
                0f, cirPosition[i].radius * Mathf.Sin(theta));
        }

        parSystem.SetParticles(parArray, parArray.Length);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < count; i++)
        {
            cirPosition[i].time += Time.deltaTime;
            cirPosition[i].radius += Mathf.PingPong(cirPosition[i].time 
                / minRadius / maxRadius, distance) - distance / 2.0f;
            //parArray[i].color = colorGradient.Evaluate(cirPosition[i].angle / 360.0f);
            if (clockWise)  // 顺时针旋转  
                cirPosition[i].angle -= (i % tier + 1) * (speed / cirPosition[i].radius / tier);
            else            // 逆时针旋转  
                cirPosition[i].angle += (i % tier + 1) * (speed / cirPosition[i].radius / tier);

            // 保证angle在0~360度  
            cirPosition[i].angle = (360.0f + cirPosition[i].angle) % 360.0f;
            float theta = cirPosition[i].angle / 180 * Mathf.PI;

            parArray[i].position = new Vector3(cirPosition[i].radius * Mathf.Cos(theta),
                0f, cirPosition[i].radius * Mathf.Sin(theta));
        }

        parSystem.SetParticles(parArray, parArray.Length);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;//视差效应

    private float xPosition;
    void Start()
    {
        cam = GameObject.Find("Virtual Camera");

        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float DistanceMove = cam.transform.position.x * parallaxEffect;//摄像头跟随着player动，cam.transform.position.x是和player的相对位置
        transform.position = new Vector3(xPosition + DistanceMove, transform.position.y);
    }
}

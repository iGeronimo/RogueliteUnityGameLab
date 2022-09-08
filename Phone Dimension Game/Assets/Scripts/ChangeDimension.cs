using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDimension : MonoBehaviour
{
    public GameObject Dimension;
    public int currentDimension = 0;
    
    private int lastDimension = 0;

    private Image dimensionLayer;

    private Color redDimension = Color.red;
    private Color blueDimension = Color.blue;
    private Color greenDimension = Color.green;
    private Color targetColor;

    private float dimensionAlpha = 0.35f;

    Vector2 startTouchPosition;
    Vector2 endTouchPosition;

    Vector2 startMousePosition;
    Vector2 endMousePosition;

    private void Awake()
    {
        dimensionLayer = Dimension.GetComponent<Image>();
        targetColor = dimensionLayer.color;
    }

    private void Start()
    {
        redDimension.a = dimensionAlpha;
        blueDimension.a = dimensionAlpha;
        greenDimension.a = dimensionAlpha;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseSwipe();
        CheckSwipe();
        ContainDimension();
        TransitionDimensions();
    }

    //Swipe functionality

    void CheckMouseSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endMousePosition = Input.mousePosition;

            if (endMousePosition.x < startMousePosition.x)
            {
                currentDimension++;
            }

            if (startMousePosition.x < endMousePosition.x)
            {
                currentDimension--;
            }
        }
    }
    
    void CheckSwipe()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if(endTouchPosition.x < startTouchPosition.x)
            {
                currentDimension++;
            }

            if(startTouchPosition.x < endTouchPosition.x)
            {
                currentDimension--;
            }
        }
    }

    void ContainDimension()
    {
        if(currentDimension < 0)
        {
            currentDimension = 2;
        }
        if(currentDimension > 2)
        {
            currentDimension = 0;
        }
    }

    void TransitionDimensions()
    {
        if(lastDimension != currentDimension)
        {
            lastDimension = currentDimension;
            if(currentDimension == 0)
            {
                targetColor = redDimension;
            }

            if (currentDimension == 1)
            {
                targetColor = greenDimension;
            }

            if (currentDimension == 2)
            {
                targetColor = blueDimension;
            }
        }

        if (targetColor != dimensionLayer.color)
        {
            dimensionLayer.color = Color.Lerp(dimensionLayer.color, targetColor, 0.05f);
        }
    }
}

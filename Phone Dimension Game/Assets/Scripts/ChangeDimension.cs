using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeDimension : MonoBehaviour
{
    public GameObject Dimension;
    public int currentDimension = 0;

    public GameObject greenDimensionUI;
    public GameObject redDimensionUI;

    private Button greenButton;
    private Button redButton;

    private int lastDimension = 0;

    private Image dimensionLayer;

    private int lastClickedDimension = 0;

    private Color redDimension = Color.red;
    private Color greenDimension = Color.green;
    private Color realDimension = Color.white;
    private Color targetColor;

    private float dimensionAlpha = 0.27f;

    //private float minimumSwipeLength;

    //Vector2 startTouchPosition;
    //Vector2 endTouchPosition;

    //Vector2 startMousePosition;
    //Vector2 endMousePosition;

    private void Awake()
    {
        dimensionLayer = Dimension.GetComponent<Image>();
        greenButton = greenDimensionUI.GetComponent<Button>();
        redButton = redDimensionUI.GetComponent<Button>();
        targetColor = dimensionLayer.color;
    }

    private void Start()
    {
        redDimension.a = dimensionAlpha;
        greenDimension.a = dimensionAlpha;
        realDimension.a = dimensionAlpha;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckMouseSwipe();
        //CheckSwipe();
        //ContainDimension();
        TransitionDimensions();
        DimensionSelection();
        staySelected();
    }

    void staySelected()
    {
        //Debug.Log("Last clicked: " + lastClickedDimension);
        if (lastClickedDimension == 1 && EventSystem.current.currentSelectedGameObject != redDimensionUI)
        {
            redButton.Select();
        }
        if (lastClickedDimension == 2 && EventSystem.current.currentSelectedGameObject != greenDimensionUI)
        {
            greenButton.Select();
        }
        if (lastClickedDimension == 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void toggleRedDimension()
    {
        if (lastClickedDimension != 1)
        {
            lastClickedDimension = 1;
        }
        else
        {
            lastClickedDimension = 0;
        }
    }

    public void toggleGreenDimension()
    {
        if (lastClickedDimension != 2)
        {
            lastClickedDimension = 2;
        }
        else
        {
            lastClickedDimension = 0;
        }
    }

    void DimensionSelection()
    {
        if (currentDimension != lastClickedDimension)
        {
            currentDimension = lastClickedDimension;
            Debug.Log("The dimension has changed!");
        }
    }

    void TransitionDimensions()
    {
        if (lastDimension != currentDimension)
        {
            lastDimension = currentDimension;
            if (currentDimension == 0)
            {
                targetColor = realDimension;
            }

            if (currentDimension == 1)
            {
                targetColor = redDimension;
            }

            if (currentDimension == 2)
            {
                targetColor = greenDimension;
            }
        }

        if (targetColor != dimensionLayer.color)
        {
            dimensionLayer.color = Color.Lerp(dimensionLayer.color, targetColor, 0.05f);
        }
    }





























    //void ContainDimension()
    //{
    //    if(currentDimension < 0)
    //    {
    //        currentDimension = 2;
    //    }
    //    if(currentDimension > 2)
    //    {
    //        currentDimension = 0;
    //    }
    //}



    //void CheckMouseSwipe()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        startMousePosition = Input.mousePosition;
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        endMousePosition = Input.mousePosition;

    //        if (endMousePosition.x - startMousePosition.x > minimumSwipeLength)
    //        {
    //            currentDimension++;
    //        }

    //        if (startMousePosition.x - endMousePosition.x < -minimumSwipeLength)
    //        {
    //            currentDimension--;
    //        }
    //    }
    //}

    //void CheckSwipe()
    //{
    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //    {
    //        startTouchPosition = Input.GetTouch(0).position;
    //    }

    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
    //    {
    //        endTouchPosition = Input.GetTouch(0).position;

    //        if (endTouchPosition.x < startTouchPosition.x)
    //        {
    //            currentDimension++;
    //        }

    //        if (startTouchPosition.x < endTouchPosition.x)
    //        {
    //            currentDimension--;
    //        }
    //    }
    //}

}

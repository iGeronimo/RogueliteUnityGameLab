using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDimensions : MonoBehaviour
{
    public Material playerMat;

    public Material redMat;
    public Material blueMat;
    public Material greenMat;

    public GameObject dimensionControl;
    private ChangeDimension dimensionCheck;

    enum playerState { redDim, blueDim, greenDim}

    private playerState currentState;

    private Color targetColor;

    private int currentDimension;
    
    // Start is called before the first frame update
    void Start()
    {
        dimensionCheck = dimensionControl.GetComponent<ChangeDimension>();
    }

    // Update is called once per frame
    void Update()
    {
        //Current Dimension Variable
        currentDimension = dimensionCheck.currentDimension;
        changePlayerState();
    }

    void changePlayerState()
    {
        if(currentDimension == 0) { currentState = playerState.redDim; }
        if(currentDimension == 1) { currentState = playerState.blueDim; }
        if(currentDimension == 2) { currentState = playerState.greenDim; }

        if (currentState == playerState.redDim)
        {
            playerStateRed();
        }

        if (currentState == playerState.blueDim)
        {
            playerStateBlue();
        }

        if (currentState == playerState.greenDim)
        {
            playerStateGreen();
        }

        changingColor();
    }

    void playerStateRed()
    {
        targetColor = new Color(255, 0, 0, 255);
    }

    void playerStateBlue()
    {
        targetColor = new Color(0, 255, 0, 255);
    }

    void playerStateGreen()
    {
        targetColor = new Color(0, 255, 255, 255);
    }

    void changingColor()
    {
        if (targetColor != playerMat.color)
        {
            playerMat.color = Color.Lerp(playerMat.color, targetColor, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Material bulletMat = other.gameObject.GetComponent<Renderer>().material;
            if(currentState == playerState.redDim && bulletMat.color != redMat.color)
            {
                Destroy(other.gameObject);
            }

            if (currentState == playerState.blueDim && bulletMat.color != blueMat.color)
            {
                Destroy(other.gameObject);
            }

            if (currentState == playerState.greenDim && bulletMat.color != greenMat.color)
            {
                Destroy(other.gameObject);
            }
                   
        }
    }
}

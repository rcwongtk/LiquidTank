using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;
using TMPro;

public class ChangeLiquidVolume : MonoBehaviour
{

    public GameObject pumpDischargePiping;

    // For the Suction Tank Level
    public GameObject referenceTank;
    public GameObject referenceValue;
    public float yPosition;
    public GameObject suctionTankLevelSign;


    // For the Static Suction Head
    public GameObject topBarHs;
    public GameObject bottomBarHs;
    public GameObject verticalBarHs;
    public GameObject suctionStaticHeadSign;
    public GameObject totalheadValueSign;
    public GameObject topBarTotalH;

    // For turning on/off Pump
    public bool pumpOn = false;
    public bool fluidMoving = false;
    public float currentLevel;
    public Material pumpOffMaterial;
    public Material pumpOnMaterial;
    public GameObject pumpIndicator;
    public GameObject dischargePipeToTank;
    public GameObject fluidFlow;

    // Start is called before the first frame update
    void Start()
    {
        //volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        LiquidVolume liquid = referenceTank.GetComponent<LiquidVolume>();
        LiquidVolume pipeLiquid = pumpDischargePiping.GetComponent<LiquidVolume>();
        LiquidVolume dischargeTankLiquid = dischargePipeToTank.GetComponent<LiquidVolume>();
        //liquid.level = volume;
        yPosition = liquid.liquidSurfaceYPosition;

        // Depending on the position of the scale, change the liquid level.
        // Interpolation Equation
        // y = [y0(x1-x) + y1(x-x0)] / [x1-x0]
        // y = suction tank volume
        // y1 = suction tank max (1)
        // y0 = suction tank min (0m)
        // x = y-Value of height indicator (text box)
        // x1 = highest y-Value (12.5)
        // x0 = lowest y-Value (-1.25)

        // Determine x1 & x0 depending on scale
        float staticHead = (float)(1 * (referenceValue.transform.localPosition.y + 1.25)) / (float)(12.5 + 1.25);
        liquid.level = staticHead;
        //pipeLiquid.level = staticHead;

        // Change the text on the reference Value to show the tank level.
        float tankHeight = liquid.level * 10;
        float totalHead = 10 - tankHeight;
        suctionTankLevelSign.GetComponentInChildren<TextMeshPro>().text = tankHeight.ToString("#.##") + "m";
        suctionStaticHeadSign.GetComponentInChildren<TextMeshPro>().text = "Hs\n" + "(" + tankHeight.ToString("#.##") + "m" + ")";
        totalheadValueSign.GetComponentInChildren<TextMeshPro>().text = "Total H\n" + "(" + totalHead.ToString("#.##") + "m" + ")";


        // We want to update the static suction head, to reduce and increase the bar size as the tank level changes.

        // The top bar will match the height of the liquid level.
        topBarHs.transform.localPosition = new Vector3(topBarHs.transform.localPosition.x, 
            suctionTankLevelSign.transform.localPosition.y, topBarHs.transform.localPosition.z);

        // The suction head sign will be halfway between the middle bar and the bottom bar
        float signNewYPosHs = (suctionTankLevelSign.transform.localPosition.y + bottomBarHs.transform.localPosition.y) / 2;
        suctionStaticHeadSign.transform.localPosition = new Vector3(suctionStaticHeadSign.transform.localPosition.x,
            signNewYPosHs, suctionStaticHeadSign.transform.localPosition.z);

        // If the Pump is not on, do not show total Head

        if (pumpOn == false)
        {
            fluidFlow.SetActive(false);
            dischargeTankLiquid.level = 0;
            topBarTotalH.SetActive(false);
            totalheadValueSign.SetActive(false);
            if (pipeLiquid.level <= staticHead)
            {
                pipeLiquid.level = staticHead;
            }
            else
            {
                pipeLiquid.level = pipeLiquid.level - 0.003f;
            }


        }
        else
        {
            topBarTotalH.SetActive(true);
            totalheadValueSign.SetActive(true);

            // The total head sign will be between the top bar and the middle bar
            float signNewYPosTotalH = (topBarTotalH.transform.localPosition.y + suctionTankLevelSign.transform.localPosition.y) / 2;
            totalheadValueSign.transform.localPosition = new Vector3(totalheadValueSign.transform.localPosition.x, signNewYPosTotalH, totalheadValueSign.transform.localPosition.z);

            // Slowly change the level of the discharge piping till it reaches the top
            if (pipeLiquid.level >= 1)
            {
                pipeLiquid.level = 1;
                fluidFlow.SetActive(true);
                dischargeTankLiquid.level = 1;
            }
            else
            {
                pipeLiquid.level = pipeLiquid.level + 0.003f;
            }


        }

    }

    public void turnOnPump()
    {
        pumpOn = true;
        currentLevel = pumpDischargePiping.GetComponent<LiquidVolume>().liquidSurfaceYPosition;
        pumpIndicator.GetComponent<MeshRenderer>().material = pumpOnMaterial;
    }

    public void turnOffPump()
    {
        pumpOn = false;
        currentLevel = pumpDischargePiping.GetComponent<LiquidVolume>().liquidSurfaceYPosition;
        pumpIndicator.GetComponent<MeshRenderer>().material = pumpOffMaterial;
    }
}

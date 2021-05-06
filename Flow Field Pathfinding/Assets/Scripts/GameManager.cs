using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* 
     * Code adapted from https://github.com/nature-of-code/noc-examples-processing/blob/master/chp06_agents/NOC_6_04_Flowfield/NOC_6_04_Flowfield.pde
     * 3 lines below copied from FlowField.pde Lines 1-3:
     * The Nature of Code 
     * Daniel Shiffman
     * http://natureofcode.com
     *
     * Daniel Shiffman's code is based on Steering Behaviors by Craig Reynolds 
     * 2 lines below copied from lines 5 and 6 of NOC_6_04_Flowfield.pde
     *
     * Flow Field Following 
     * Via Reynolds: http://www.red3d.com/cwr/steer/FlowFollow.html
     * 
     * Any line that is adapted from or inspired by NOC_6_04_Flowfield.pde has a comment. 
     * NOC_6_04_Flowfield.pde was written in Processing, so the structure was adapted to fit Unity
     * The original code is MIT Licensed, and this project is as well.
     *
     * Some of this code is an adaptation of this tutorial by Peter Olthof of Peer Play
     * https://www.youtube.com/watch?v=gPNdnIMbe8o
     * 
     * While the code is MIT Licensed, Peter Olthof put the original code behind a paywall.
     * So many thanks to mwburke on github for uploading a completed version of the tutorial. 
     * I will be referring to their lines of code, specifically for the noiseFlowfield.cs script.
     * https://github.com/mwburke/unity-flowfield-tutorial/blob/main/Assets/noiseFlowfield.cs
     * So any credited lines will be from mwburkes adaptation of noiseFlowfield.cs
     */

    // Flowfield object (NOC_6_04_Flowfield.pde line 11)
    public FlowField flowfield; //NOC_6_04_Flowfield.pde line 12
    // An ArrayList of vehicles NOC_6_04_Flowfield.pde line 13
    List<Vehicle> vehicles;  //NOC_6_04_Flowfield.pde line 14
    
    public int vehicleCount; //noiseFlowfield.cs line 17
    public Vehicle vehiclePrefab; //noiseFlowfield.cs line 16

    private void Start() //noiseFlowfield.cs line 23
    {
        flowfield = Instantiate(flowfield, Vector3.zero, Quaternion.identity); //noiseFlowfield.cs line 36
        vehicles = new List<Vehicle>();  //noiseFlowfield.cs line 27

        //shortens variable name
        Vector3 tp = transform.position;
        for (int i =0; i < vehicleCount; i++) //noiseFlowfield.cs line 28
        {
            //adapted from noiseFlowfield.cs lines 31-33 and adjusted for negative offset
            Vector3 randomPos = new Vector3( 
                Random.Range(flowfield.cellSize * -flowfield.gridSize.x / 2, (flowfield.gridSize.x - 1) / 2 * flowfield.cellSize),
                Random.Range(flowfield.cellSize * -flowfield.gridSize.y / 2, (flowfield.gridSize.y - 1)/ 2 * flowfield.cellSize),
                Random.Range(flowfield.cellSize * -flowfield.gridSize.z / 2, (flowfield.gridSize.z - 1) / 2 * flowfield.cellSize)
                );
            Vehicle newVehicle = Instantiate(vehiclePrefab, randomPos, Quaternion.identity);  //noiseFlowfield.cs line 36
            newVehicle.constructVehicle(0.5f, 0.8f, flowfield);  //Constructor inspired by NOC_06_04_Flowfield.pde line 23
            vehicles.Add(newVehicle);  //noiseFlowfield.cs line 40
        }
    }


    private void Update()
    {
        // Tell all the vehicles to follow the flow field (NOC_6_04_Flowfield.pde line 31)
        foreach (Vehicle v in vehicles) //NOC_6_04_Flowfield.pde line 32
        {
            v.movementManager(); //inspired by NOC_6_04_Flowfield.pde line 33
        }
    }

}

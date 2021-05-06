using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    /*
     * Code adapted from https://github.com/nature-of-code/noc-examples-processing/blob/master/chp06_agents/NOC_6_04_Flowfield/Vehicle.pde
     * 
     * 3 lines below copied from Vehicle.pde Lines 1-3:
     * The Nature of Code 
     * Daniel Shiffman
     * http://natureofcode.com
     * 
     * Daniel Shiffman's code is based on Steering Behaviors by Craig Reynolds 
     * 2 lines below copied from lines 5 and 6 of
     * https://github.com/nature-of-code/noc-examples-processing/blob/master/chp06_agents/NOC_6_04_Flowfield/NOC_6_04_Flowfield.pde
     * Flow Field Following 
     * Via Reynolds: http://www.red3d.com/cwr/steer/FlowFollow.html
     * 
     * Any line that is adapted from or inspired by Vehicle.pde has a comment. 
     * Vehicle.pde was written in Processing, so the structure was adapted to fit Unity
     * The original code is MIT Licensed, and this project is as well.
     */
    public FlowField flowfield; 

    Vector3 velocity; //Vehicle.pde Line 11
    Vector3 acceleration; //Vehicle.pde Line 12
    float maxforce; // Maximum steering force (Vehicle.pde Line 14)
    float maxspeed; // Maximum speed (Vehicle.pde Line 15)

    bool in_bounds = true;
    public void constructVehicle(float ms, float mf, FlowField flow) //Vehicle.pde Line 17
    {
        maxspeed = ms; //Vehicle.pde Line 20
        maxforce = mf; //Vehicle.pde Line 21
        acceleration = Vector3.zero; //Vehicle.pde Line 22
        velocity = Vector3.zero; //Vehicle.pde Line 23
        flowfield = flow; 
    }

    public void movementManager()
    {
        if (in_bounds)
        {
            Vector3Int index = boundsCheck();
            Vector3Int failExample = new Vector3Int(-1, -1, -1);
            if (index == failExample) in_bounds = false;
            else applyForce(flowfield.lookup(index));
        } 
        else
        {
            //Vehicle continues with set velocity
            applyForce(Vector3.zero);
        }
    }
    
    //Part of Deep Dive Two
    //boundsCheck takes the current position and checks whether it is within the bounds of the flowfield
    //if the vehicle is within the flowfield, the corresponding index is returned
    //if not, it returns (-1,-1,-1)
    Vector3Int boundsCheck()
    {
        Vector3Int gridSize = flowfield.getGridSize();
        float cellSize = flowfield.getCellSize();
        Vector3 tp = transform.position;

        //[from a transform.position, the corresponding index is returned]
        Vector3 locationConversion =
            new Vector3(
                tp.x / cellSize + gridSize.x / 2,
                tp.y / cellSize + gridSize.y / 2,
                tp.z / cellSize + gridSize.z / 2);
        //[simplifies bound corrections]
        Vector3 gridConversion =
            new Vector3(
                gridSize.x / 2 * cellSize,
                gridSize.y / 2 * cellSize,
                gridSize.z / 2 * cellSize
                );


        //Mutates transform.position to fit within bounds
        Vector3Int failedCheck = new Vector3Int(-1, -1, -1);
        if (locationConversion.x >= gridSize.x - 1) return failedCheck;
        if (locationConversion.x < 0) return failedCheck;

        if (locationConversion.y >= gridSize.y -  1) return failedCheck;
        if (locationConversion.y < 0) return failedCheck;

        if (locationConversion.z >= gridSize.z - 1) return failedCheck;
        if (locationConversion.z < 0) return failedCheck;

        //New transform.position converted to appropriate index (should fit within bounds now)
        int x = Mathf.FloorToInt(tp.x / cellSize + gridSize.x / 2);
        int y = Mathf.FloorToInt(tp.y / cellSize + gridSize.y / 2);
        int z = Mathf.FloorToInt(tp.z / cellSize + gridSize.z / 2);
        return new Vector3Int(x, y, z);
    }


//Heavilly based on Vehicle.pde Lines 52-60
    void applyForce(Vector3 force)
    {
        acceleration += force;
        //Update velocity (Vehicle.pde Line 54)
        velocity += acceleration; //Vehicle.pde Line 55
        // Limit Speed (Vehicle.pde Line 56)
        velocity = Vector3.ClampMagnitude(velocity, maxspeed); //Unity equivalent of Vehicle.pde Line 56
        transform.position += velocity; //Vehicle.pde Line 57
        //Acceleration never actually increases, just a placeholder from Vehicle.pde Line 58
        acceleration *= 0; //Vehicle.pde Line 59
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour
{
    /* 
     * Code adapted from https://github.com/nature-of-code/noc-examples-processing/blob/master/chp06_agents/NOC_6_04_Flowfield/FlowField.pde
     * 3 lines below copied from FlowField.pde Lines 1-3:
     * The Nature of Code 
     * Daniel Shiffman
     * http://natureofcode.com
     *
     * Daniel Shiffman's code is based on Steering Behaviors by Craig Reynolds 
     * 2 lines below copied from lines 5 and 6 of
     * https://github.com/nature-of-code/noc-examples-processing/blob/master/chp06_agents/NOC_6_04_Flowfield/NOC_6_04_Flowfield.pde
     *
     * Flow Field Following 
     * Via Reynolds: http://www.red3d.com/cwr/steer/FlowFollow.html
     * 
     * Any line that is adapted from or inspired by FlowField.pde has a comment. 
     * Flowfield.pde was written in Processing, so the structure was adapted to fit Unity
     * The original code is MIT Licensed, and this project is as well.
     *
     * Much of this code is an adaptation of this tutorial by Peter Olthof of Peer Play
     * https://www.youtube.com/watch?v=gPNdnIMbe8o
     * 
     * While the code is MIT Licensed, Peter Olthof put the original code behind a paywall.
     * So many thanks to mwburke on github for uploading a completed version of the tutorial. 
     * I will be referring to their lines of code, specifically for the noiseFlowfield.cs script.
     * https://github.com/mwburke/unity-flowfield-tutorial/blob/main/Assets/noiseFlowfield.cs
     * So any credited lines will be from mwburkes adaptation of noiseFlowfield.cs
     */
    public Vector3[,,] flowfieldDirection; //noiseFlowfield.cs line 11
    public float cellSize = 2; //noiseFlowfield.cs line 10
    public Vector3Int gridSize = new Vector3Int(10, 10, 10); //noiseFlowfield.cs line 9


    private void Start()
    {
        flowfieldDirection = new Vector3[gridSize.x, gridSize.y, gridSize.z];  //noiseFlowfield.cs line 25
        //(I only call this once, as opposed to using noise and updating every frame)
        CalculateFlowFieldDirections();  //noiseFlowfield.cs line 47 
    }

    public Vector3Int getGridSize()
    {
        return gridSize;
    }
    public float getCellSize()
    {
        return cellSize;
    }

    //[Lookup takes a Vector3 representing location, and returns the appropriate velocity]
    //Inspired by Flowfield.pde lines 67-71
    public Vector3 lookup(Vector3Int lookup)
    {
        return flowfieldDirection[lookup.x, lookup.y, lookup.z];
    }

    /* 
     * Below code adapted from https://www.youtube.com/watch?v=gPNdnIMbe8o
     * 
     */
    void CalculateFlowFieldDirections()  //noiseFlowfield.cs line 51
    {
        for (int x = 0; x < gridSize.x; x++)  //noiseFlowfield.cs line 53
        {
            for (int y = 0; y < gridSize.y; y++)  //noiseFlowfield.cs line 59
            {
                for (int z = 0; z < gridSize.z; z++)  //noiseFlowfield.cs line 62
                {
                    //noiseDirection has no noise, just leftover from original
                    Vector3 noiseDirection = CustomFlowFunction(new Vector3Int(x, y, z));  //noiseFlowfield.cs line 65
                    Vector3 nd = noiseDirection.normalized;  //noiseFlowfield.cs line 66
                    flowfieldDirection[x, y, z] = nd;  //noiseFlowfield.cs line 66
                }
            }
        }
    }

    //Part of Deep Dive One
    private Vector3 CustomFlowFunction(Vector3Int index)
    {
        //F(x,y,z) = [-z, Abs(y) + 0.1, -x]
        //Adjusts for negative offset
        return new Vector3(-(index.x - gridSize.x / 2), Mathf.Abs(index.y - gridSize.y / 2) + 0.1f, -(index.z - gridSize.z / 2));
    }
    private void OnDrawGizmos()  //noiseFlowfield.cs line 122
    {
            for (int x = 0; x < gridSize.x; x++)  //noiseFlowfield.cs line 131
            {
                for (int y = 0; y < gridSize.x; y++)  //noiseFlowfield.cs line 134
                {
                    for (int z = 0; z < gridSize.x; z++)  //noiseFlowfield.cs line 137
                    {
                        Vector3 nd = flowfieldDirection[x, y, z];  //noiseFlowfield.cs line 140
                        Gizmos.color = new Color(Mathf.Abs(nd.x), Mathf.Abs(nd.y), Mathf.Abs(nd.z), 0.4f); //noiseFlowfield.cs line 143[Adjusted for negative values]
                        Vector3 pos = cellSize * (new Vector3(x, y, z) + transform.position - gridSize/2); //noiseFlowfield.cs line 141[Adjusted for negative offset]
                        Vector3 endpos = pos + Vector3.Normalize(nd);  //noiseFlowfield.cs line 142
                        Gizmos.DrawLine(pos, endpos);  //noiseFlowfield.cs line 144
                        Gizmos.DrawSphere(endpos, 0.05f);  //noiseFlowfield.cs line 145
                    }
                }
            }
        }
}

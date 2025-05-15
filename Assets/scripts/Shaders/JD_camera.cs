using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class camura : MonoBehaviour
{
    // Shader to apply the pixelated cel-shading effect
    public Shader CamuraShader;

    // Material to hold and control the shader
    public Material CamuraMaterial;

    // these values set the amount of pixels that the screen UV gets set to (higher the value, higher the resolution. 1080 would be 1080P for example)
    [Range(10.0f, 1080.0f)]
    public float pixelSize = 100.0f;

    // these values are for the general warp effect
    [Range(0.0f, 0.2f)]
    public float warpStrength = 0.05f;

    [Range(0.0f, 10.0f)]
    public float warpDropOff = 5.0f;

    // these values are for the dithering/wave effect
    [Range(0.0f, 0.01f)]
    public float waveWarpStrength = 0.01f;

    [Range(0.5f, 5.0f)]
    public float waveWarpSpeed = 2.0f;


    public Color shadowColor = Color.black;

    void Start()
    {
        if (!CamuraShader)
        {
            Debug.LogError("Shader missing, Assign a shader in the inspector. should be called camura.");
            enabled = false;
            return;
        }

        CamuraMaterial = new Material(CamuraShader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest) // this allows the shader be used as a post processing effect, all shader values and properties needs to be passed through here.
    {
        if (CamuraMaterial)
        {
            CamuraMaterial.SetFloat("_PixelSize", pixelSize); 

            CamuraMaterial.SetFloat("_WaveWarpStrength", waveWarpStrength);
            CamuraMaterial.SetFloat("_WaveWarpSpeed", waveWarpSpeed);

            CamuraMaterial.SetFloat("_WarpStrength", warpStrength);
            CamuraMaterial.SetFloat("_WarpDropOff", warpDropOff);

            CamuraMaterial.SetColor("_ShadowColor", shadowColor);


            Graphics.Blit(src, dest, CamuraMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
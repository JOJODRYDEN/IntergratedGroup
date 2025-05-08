using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class camura : MonoBehaviour
{
    // Shader to apply the pixelated cel-shading effect
    public Shader pixelCelShader;

    // Material to hold and control the shader
    public Material pixelCelMaterial;

    // Controls how large the pixel blocks are (1 = no pixelation, higher = more pixelation)
    [Range(100, 1000)]
    public float pixelSize = 100.0f;

    // Texture that defines how "hard" or "smooth" the cel-shading is
    public Texture2D rampTexture;

    // Color for the shadows (can be any color you want)
    public Color shadowColor = Color.black;

    // Runs once when the script is first loaded
    void Start()
    {
        // Ensure a shader is assigned, or disable the effect
        if (pixelCelShader == null)
        {
            Debug.LogError("Please assign the Pixelated Cel-Shading shader!");
            enabled = false;
            return;
        }

        // Create a new material instance based on the provided shader
        pixelCelMaterial = new Material(pixelCelShader);
    }

    // This method is called every frame after the camera renders the scene
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // If the shader material exists, apply the post-processing effect
        if (pixelCelMaterial != null)
        {
            // Pass the pixel size value to the shader (controls pixelation intensity)
            pixelCelMaterial.SetFloat("_PixelSize", pixelSize);

            // Pass the cel-shading ramp texture to the shader
            pixelCelMaterial.SetTexture("_Ramp", rampTexture);

            // Pass the shadow color to the shader
            pixelCelMaterial.SetColor("_ShadowColor", shadowColor);

            // Apply the shader using Graphics.Blit (process input to output)
            Graphics.Blit(src, dest, pixelCelMaterial);
        }
        else
        {
            // If the shader is missing, just render the scene as normal
            Graphics.Blit(src, dest);
        }
    }
}
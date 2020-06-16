using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UVPainter : MonoBehaviour
{
    public Transform paintCircle = null;
    private Texture2D resourceTexture = null;
    private RenderTexture renderTexture = null;
    private CommandBuffer renderCommand  = null;
    public Material paintMaterial = null;
    // Start is called before the first frame update
    void Start()
    {
        //获取当前材质的图片
        Renderer renderer = GetComponent<Renderer>();
        Material material = renderer.material;
        resourceTexture = material.GetTexture("_MainTex") as Texture2D;
        if(null == resourceTexture)
        {
            Debug.LogError("只支持修改Texture2D");
            return;
        }

        //创建渲染表面
        renderTexture = new RenderTexture(resourceTexture.width, resourceTexture.height, 0, RenderTextureFormat.ARGB32);
        material.SetTexture("_MainTex", renderTexture);
        Graphics.Blit(resourceTexture, renderTexture);

        //创建绘制材质
        if(null == paintMaterial)
        {
            paintMaterial = new Material(Shader.Find("Custom/UVRender"));
        }
        
        //加入绘制指令
        renderCommand = new CommandBuffer();
        renderCommand.SetRenderTarget(renderTexture);
        renderCommand.DrawRenderer(renderer, paintMaterial, 0, -1);
        Camera.main.AddCommandBuffer(CameraEvent.AfterEverything, renderCommand);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = paintCircle.position;
        float scale = paintCircle.localScale.x / 2;
        paintMaterial.SetVector("_CirclePoint", new Vector4(position.x, position.y, position.z, scale));
    }
}

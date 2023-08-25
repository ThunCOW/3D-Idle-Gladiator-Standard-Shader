using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcessingOutline
{
    public class PostProcessOutlineComposite : PostProcessEffectRenderer<PostProcessComposite>
    {
        public override void Render(PostProcessRenderContext context)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Hidden/OutlineComposite"));
            sheet.properties.SetColor("_Color", settings.color);

            if (PostProcessOutlineRenderer.outlineRendererTexture != null)
                sheet.properties.SetTexture("_OutlineText", PostProcessOutlineRenderer.outlineRendererTexture);

            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
    }
}
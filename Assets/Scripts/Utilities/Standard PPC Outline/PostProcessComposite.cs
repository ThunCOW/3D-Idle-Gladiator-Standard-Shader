using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcessingOutline
{
    [SerializeField]
    [PostProcess(typeof(PostProcessOutlineComposite), PostProcessEvent.AfterStack, "Outlnie Composite")]
    public sealed class PostProcessComposite : PostProcessEffectSettings
    {
        public ColorParameter color = new ColorParameter() { value = Color.white };
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcessingOutline
{
    [SerializeField]
    [PostProcess(typeof(PostProcessOutlineRenderer), PostProcessEvent.AfterStack, "Outlnie")]
    public sealed class PostProcessOutline : PostProcessEffectSettings
    {
        public FloatParameter thickness = new FloatParameter();
        public FloatParameter depthMin = new FloatParameter();
        public FloatParameter depthMax = new FloatParameter();
        public ColorParameter edgeColor = new ColorParameter();
    }
}
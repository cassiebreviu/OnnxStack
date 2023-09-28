﻿using NumSharp;
using System.Collections.Generic;

namespace OnnxStack.StableDiffusion.Config
{
    public class SchedulerOptions
    {
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        ///  The height of the image. Default is 512 and must be divisible by 64.
        /// </value>
        public int Height { get; set; } = 512;

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width of the image. Default is 512 and must be divisible by 64.
        /// </value>
        public int Width { get; set; } = 512;

        /// <summary>
        /// Gets or sets the seed.
        /// </summary>
        /// <value>
        /// If value is set to 0 a random seed is used.
        /// </value>
        public int Seed { get; set; }

        /// <summary>
        /// Gets or sets the number inference steps.
        /// </summary>
        /// <value>
        /// The number of steps to run inference for. The more steps the longer it will take to run the inference loop but the image quality should improve.
        /// </value>
        public int InferenceSteps { get; set; } = 15;

        /// <summary>
        /// Gets or sets the guidance scale.
        /// </summary>
        /// <value>
        /// The scale for the classifier-free guidance. The higher the number the more it will try to look like the prompt but the image quality may suffer.
        /// </value>
        public float GuidanceScale { get; set; } = 7.5f;

        public int TrainTimesteps { get; set; } = 1000;
        public float BetaStart { get; set; } = 0.00085f;
        public float BetaEnd { get; set; } = 0.012f;
        public IEnumerable<float> TrainedBetas { get; set; }
        public TimestepSpacing TimestepSpacing { get; set; } = TimestepSpacing.Linspace;
        public BetaSchedule BetaSchedule { get; set; } = BetaSchedule.ScaledLinear;
        public int StepsOffset { get; set; } = 0;
        public bool UseKarrasSigmas { get; set; } = false;
        public VarianceType VarianceType { get; internal set; } = VarianceType.FixedSmall;
        public float SampleMaxValue { get; set; } = 1.0f;
        public bool Thresholding { get; internal set; } = false;
        public bool ClipSample { get; internal set; } = false;
        public float ClipSampleRange { get; internal set; } = 1f;
        public PredictionType PredictionType { get; internal set; } = PredictionType.Epsilon;
        public AlphaTransformType AlphaTransformType { get; set; } = AlphaTransformType.Cosine;
        public float MaximumBeta { get; set; } = 0.999f;

    }
}
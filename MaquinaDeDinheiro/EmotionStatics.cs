using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeDinheiro
{
    public class EmotionStatics
    {
        public float Anger { get; set; }
        public float Disgust { get; set; }
        public float Fear { get; set; }
        public float Joy { get; set; }
        public float Sadness { get; set; }
        public float TotalBadEmotion { get; set; }
        public float PercentualOfChance { get; set; }
        public bool IsGoodNews { get; set; }
        public bool IsBadNews { get; set; }

        public EmotionStatics(AnalysisResults analysis)
        {
            if (analysis.Emotion.Document.Emotion.Anger.HasValue)
            {
                Anger = analysis.Emotion.Document.Emotion.Anger.Value;
            }
            if (analysis.Emotion.Document.Emotion.Disgust.HasValue)
            {
                Disgust = analysis.Emotion.Document.Emotion.Disgust.Value;
            }
            if (analysis.Emotion.Document.Emotion.Fear.HasValue)
            {
                Fear = analysis.Emotion.Document.Emotion.Fear.Value;
            }
            if (analysis.Emotion.Document.Emotion.Joy.HasValue)
            {
                Joy = analysis.Emotion.Document.Emotion.Joy.Value;
            }
            if (analysis.Emotion.Document.Emotion.Sadness.HasValue)
            {
                Sadness = analysis.Emotion.Document.Emotion.Sadness.Value;
            }

            AnalyseResults();
            SwingPercentage();
        }

        private void AnalyseResults()
        {
            TotalBadEmotion = Anger + Disgust + Fear + Sadness;

            if (TotalBadEmotion > Joy)
            {
                IsBadNews = true;
            }
            else
            {
                IsGoodNews = true;
            }
        }

        private void SwingPercentage()
        {
            if (IsBadNews)
            {
                PercentualOfChance = ((TotalBadEmotion - Joy) * 100);
            }
            else
            {
                PercentualOfChance = (Joy - TotalBadEmotion) * 100;
            }
        }
    }
}
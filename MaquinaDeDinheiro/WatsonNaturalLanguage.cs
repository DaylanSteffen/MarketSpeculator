using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;

namespace MaquinaDeDinheiro
{
    public class WatsonNaturalLanguage
    {
        NaturalLanguageUnderstandingService _naturalLanguageUnderstandingService;
        static string _userName = "a1a0fe7e-5bb7-407d-9789-2d3fdaa52011";
        static string _password = "TNsK6nLKYQyt";
        static string _versionDate = "2017-02-27";
        static string _apiKey = "B8CVyELgP_06IvM8fxhcwhZ3vm95aHhr4DTp0V-BJpzo";
        public WatsonNaturalLanguage()
        {
            _naturalLanguageUnderstandingService = new NaturalLanguageUnderstandingService(_userName, _password, _versionDate);
        }


        public AnalysisResults AnalyzeText(string text)
        {
            var parameters = new Parameters()
            {
                Text = text,
                Features = new Features()
                {
                    Keywords = new KeywordsOptions()
                    {
                        Limit = 8,
                        Sentiment = true,
                        Emotion = true
                    },

                    Emotion = new EmotionOptions()
                    {
                        Document = true
                    }
                }
            };

            return _naturalLanguageUnderstandingService.Analyze(parameters);
        }

    }
}

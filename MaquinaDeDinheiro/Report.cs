
using HtmlAgilityPack;
using System.Linq;
using System.Text;

namespace MaquinaDeDinheiro
{
    public class Report
    {
        public Report(string url, Coin coin)
        {
            URL = url;
            Coin = coin;
            ExtractTextFromWebPage();
            var watsonNaturalLanguage = new WatsonNaturalLanguage().AnalyzeText(TextNotice);
            EmotionStatics = new EmotionStatics(watsonNaturalLanguage);
        }

        protected string URL { get; set; }
        public string TextNotice { get; set; }
        public Coin Coin { get; set; }
        public EmotionStatics EmotionStatics { get; set; }

        void ExtractTextFromWebPage()
        {
            var web = new HtmlWeb();
            var doc = web.Load(URL);
            var stringBuilderText = new StringBuilder();

            var nodes = doc.DocumentNode.Descendants().FirstOrDefault(q => q.Id == "storytext").Descendants()
                .Where(x => x.HasClass("speakable")).ToList();

            if (nodes == null || nodes.Count == 0)
            {
                return;
            }

            nodes.ForEach(q => stringBuilderText.Append(q.InnerText));
            TextNotice = stringBuilderText.ToString();
            var ax = 0;
        }
    }
}

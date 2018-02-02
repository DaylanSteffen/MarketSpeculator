using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace MaquinaDeDinheiro
{
    public class Manager
    {
        public Coin Dolar, Euro, BitCoin;

        public Manager()
        {
            Dolar = new Coin(TypeCoins.Dolar);
            Euro = new Coin(TypeCoins.EUR);
            BitCoin = new Coin(TypeCoins.Bitcoin);
        }

        public List<Coin> GetListOfCoinsAndValues()
        {
            var response = GetValuesOfCoin();
            return GetValuesFromResponse(response);
        }

        public string GetValuesOfCoin()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(APIURL.GetValues);
            request.Method = "POST";
            request.ContentType = "application/json";

            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();
            responseReader.Close();
            return response;
        }

        public List<Coin> GetValuesFromResponse(string response)
        {

            if (response.Contains("Erro ao conectar ao banco de dados"))
            {
                return new List<Coin>();
            };
            var coins = new List<Coin>();
            XElement xmlTree = XElement.Parse(response);
            foreach (var el in xmlTree.Elements().ElementAtOrDefault(1).Elements())
            {
                Coin moeda = new Coin(el.Name.ToString());
                ExtractDataFromElement(el, ref moeda);
                coins.Add(moeda);
            }

            return coins;
        }

        private void ExtractDataFromElement(XElement element, ref Coin coin)
        {
            foreach (var subElement in element.Elements())
            {
                if (subElement.Name.LocalName.Equals("nome"))
                {
                    coin.nome = subElement.Value;
                }
                else if (subElement.Name.LocalName.Equals("valor"))
                {
                    var coinValue = 0d;
                    if (Double.TryParse(subElement.Value, out coinValue))
                    {
                        coin.valor = coinValue;
                    }
                }
            }
            coin.ultima_consulta = DateTime.Now;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }
    }

    public static class APIURL
    {
        public static string GetValues = "http://api.promasters.net.br/cotacao/v1/valores?alt=xml";
    }

    public class Coin
    {
        public Coin()
        {

        }
        public Coin(TypeCoins coin)
        {
            switch (coin)
            {
                case TypeCoins.Dolar:
                    nome = "USD";
                    break;
                case TypeCoins.EUR:
                    nome = "EUR";
                    break;
                case TypeCoins.Bitcoin:
                    nome = "BTC";
                    break;
                default:
                    nome = "OTHER";
                    break;
            }
        }

        public Coin(string coin)
        {
            switch (coin)
            {
                case "USD":
                    TypeCoin = TypeCoins.Dolar;
                    break;
                case "EUR":
                    TypeCoin = TypeCoins.EUR;
                    break;
                case "BTC":
                    TypeCoin = TypeCoins.Bitcoin;
                    break;
                default:
                    break;
            }
        }

        public TypeCoins TypeCoin { get; set; }
        public string nome { get; set; }
        public double valor { get; set; }
        public DateTime ultima_consulta { get; set; }
    }

    public enum TypeCoins
    {
        Dolar,
        EUR,
        Bitcoin
    }
}

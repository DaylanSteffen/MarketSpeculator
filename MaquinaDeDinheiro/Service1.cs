using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeDinheiro
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();

            var report = new Report("http://money.cnn.com/2018/02/02/technology/bitcoin-8000-india-crackdown/index.html", new Coin(TypeCoins.Dolar));
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;
namespace App01_ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BOTAO.Clicked += BuscarCEP;
    
        }

        private void BuscarCEP(Object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();
            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if(end != null)
                    {
                        RESULTADO.Text = string.Format("Endereco: {2} de {3} {0},{1}", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO","endereco nao encontrado "+cep,"OK");
                    }


                }
                catch(Exception e)
                {
                    DisplayAlert("ERRO CRITICO", e.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;
            if(cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP invalido! deve conter 8 caracteres", "Ok");
                valido = false;
            }
            int NovoCEP = 0;
            if(!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP invalido! composto apenas por numeros", "Ok");
                valido = false;
            }
            return valido;
        }
    }
}

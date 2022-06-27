using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1.Servico.Modelo;
using App1.Servico;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;

        }
        private void BuscarCEP(object sender, EventArgs args)
        {
            try { 
                string cep = CEP.Text.Trim();

                if (isValidCep(cep))
                {
                    Endereco end = ViaCepServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                    {
                        
                        RESULTADO.Text = string.Format("Endereco: {2} de {3} {0}, {1}", end.Localidade, end.Uf, end.Logradouro, end.Bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O Endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }
                }
            }
            catch(Exception e)
            {
                DisplayAlert("ERRO", "ERRO CRITICO" + e.Message, "OK");
            }
        }

        private bool isValidCep(string cep)
        {
            bool valido = true;
            int NovoCep = 0;

            if (cep.Length != 8)
            {
                //ERRO
                DisplayAlert("ERRO", "O CEP deve conter 8 caracteres", "OK");
                valido = false;
            }
            else if(!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve ser composto apenas por numeros", "OK");
                valido = false;
            }
            return valido;
        }
    }
}

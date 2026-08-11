using System;
using System.Collections.Generic;

namespace CRMAPI.Classes
{
    public class ProdutoSerasaAutenticacaoClass
    {
        public string accessToken { get; set; }
        public string tokenType { get; set; }
        public List<string> scope { get; set; }
        public DateTime DataExpiracao { get; set; }
        private string _expiresIn;

        private string Client_ID { get; set; }
        private string Secret_ID { get; set; }
        private string URLAUTENTICACAO { get; set; }
        private string URLEXECUCAO { get; set; }
        private string reportName { get; set; }
        private string OptionalFeatures { get; set; }

        public string expiresIn
        {
            get { return _expiresIn; }
            set
            {
                _expiresIn = value;
                long seconds;

                // Converte o expiresIn para um long e calcula a DataExpiracao 
                if (long.TryParse(value, out seconds))
                {
                    // Converte o timestamp em segundos para DateTime 
                    DataExpiracao = DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
                }
            }
        }

        // Métodos para Client_ID 
        public void SetClientID(string clientID)
        {
            this.Client_ID = clientID;
        }

        public string GetClientID()
        {
            return this.Client_ID;
        }

        // Métodos para Secret_ID 
        public void SetSecretID(string secretID)
        {
            this.Secret_ID = secretID;
        }

        public string GetSecretID()
        {
            return this.Secret_ID;
        }

        // Métodos para URLAUTENTICACAO 
        public void SetURLAutenticacao(string urlAutenticacao)
        {
            this.URLAUTENTICACAO = urlAutenticacao;
        }

        public string GetURLAutenticacao()
        {
            return this.URLAUTENTICACAO;
        }

        // Métodos para URLEXECUCAO 
        public void SetURLExecucao(string urlExecucao)
        {
            this.URLEXECUCAO = urlExecucao;
        }

        public string GetURLExecucao()
        {
            return this.URLEXECUCAO;
        }

        // Métodos para reportName 
        public void SetReportName(string reportName)
        {
            this.reportName = reportName;
        }

        public string GetReportName()
        {
            return this.reportName;
        }

        // Métodos para OptionalFeatures 
        public void SetOptionalFeatures(string OptionalFeatures)
        {
            this.OptionalFeatures = OptionalFeatures;
        }

        public string GetOptionalFeatures()
        {
            return this.OptionalFeatures;
        }
    }
}
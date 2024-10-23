// -----------------------------------------------------------------------
// Autor: João Marcelo
// E-mail: a23041@alunos.ipca.pt
// Versão: 1.0.0
// Data: ${DATE}
// Disciplina: Programação Orientada Objetos
// Licença: MIT
// -----------------------------------------------------------------------

namespace POO
{
    public abstract class Ocorrencia
    {
        #region Private Properties
        private DateTime dataOcorrencia;
        private string localizacao;
        private string descricao;
        private string tipoAtendimento;
        #endregion

        #region Public Properties
        public DateTime DataOcorrencia
        {
            get { return dataOcorrencia; }
            set { dataOcorrencia = value; }
        }

        public string Localizacao
        {
            get { return localizacao; }
            set { localizacao = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public string TipoAtendimento
        {
            get { return tipoAtendimento; }
            set { tipoAtendimento = value; }
        }
        #endregion

        #region Constructors
        public Ocorrencia(DateTime dataOcorrencia, string localizacao, string descricao, string tipoAtendimento)
        {
            this.dataOcorrencia = dataOcorrencia;
            this.localizacao = localizacao;
            this.descricao = descricao;
            this.tipoAtendimento = tipoAtendimento;
        }
        #endregion

        #region Abstract Method
        // Método abstrato para exibir os detalhes da ocorrência
        public abstract void ExibirDetalhesOcorrencia();
        #endregion
    }
}
// -----------------------------------------------------------------------
// Autor: João Marcelo
// E-mail: a23041@alunos.ipca.pt
// Versão: 1.0.0
// Data: ${DATE}
// Discipla: Programação Orientada Objetos
// Licença: MIT
// -----------------------------------------------------------------------

namespace POO
{
    public class OcorrenciaHospitalar : Ocorrencia
    {
        #region Private Properties
        private string tipoOcorrencia;
        #endregion

        #region Public Properties
        public string TipoOcorrencia
        {
            get { return tipoOcorrencia; }
            set { tipoOcorrencia = value; }
        }
        #endregion

        #region Constructors
        public OcorrenciaHospitalar(DateTime dataOcorrencia, string localizacao, string descricao, string tipoAtendimento, string tipoOcorrencia)
            : base(dataOcorrencia, localizacao, descricao, tipoAtendimento)
        {
            this.tipoOcorrencia = tipoOcorrencia;
        }
        #endregion

        #region Override Method
        public override void ExibirDetalhesOcorrencia()
        {
            Console.WriteLine($"Data: {DataOcorrencia}, Localização: {Localizacao}, Descrição: {Descricao}, Tipo de Atendimento: {TipoAtendimento}, Tipo de Ocorrência: {TipoOcorrencia}");
        }
        #endregion
    }
}
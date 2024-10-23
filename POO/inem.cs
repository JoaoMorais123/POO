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
    public class Inem : Equipa
    {
        #region Private Properties
        private string zonaAtuacao;
        #endregion

        #region Public Properties
        public string ZonaAtuacao
        {
            get { return zonaAtuacao; }
            set { zonaAtuacao = value; }
        }
        #endregion

        #region Constructors
        public Inem(string nomeEquipa, string zonaAtuacao)
            : base(nomeEquipa)
        {
            this.zonaAtuacao = zonaAtuacao;
        }
        #endregion

        #region Override Method
        public override void ExibirDetalhesEquipa()
        {
            Console.WriteLine($"Nome da Equipa: {NomeEquipa}, Zona de Atuação: {ZonaAtuacao}");
            foreach (var membro in Membros)
            {
                membro.ExibirDetalhes();
            }
        }
        #endregion
    }
}
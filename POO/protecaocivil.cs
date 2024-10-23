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
    public class ProtecaoCivil : Equipa
    {
        #region Private Properties
        private string areaResponsabilidade;
        #endregion

        #region Public Properties
        public string AreaResponsabilidade
        {
            get { return areaResponsabilidade; }
            set { areaResponsabilidade = value; }
        }
        #endregion

        #region Constructors
        public ProtecaoCivil(string nomeEquipa, string areaResponsabilidade)
            : base(nomeEquipa)
        {
            this.areaResponsabilidade = areaResponsabilidade;
        }
        #endregion

        #region Override Method
        public override void ExibirDetalhesEquipa()
        {
            Console.WriteLine($"Nome da Equipa: {NomeEquipa}, Área de Responsabilidade: {AreaResponsabilidade}");
            foreach (var membro in Membros)
            {
                membro.ExibirDetalhes();
            }
        }
        #endregion
    }
}
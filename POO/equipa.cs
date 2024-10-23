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
    public abstract class Equipa
    {
        #region Private Properties
        private string nomeEquipa;
        private List<Funcionario> membros = new List<Funcionario>();
        #endregion

        #region Public Properties
        public string NomeEquipa
        {
            get { return nomeEquipa; }
            set { nomeEquipa = value; }
        }

        public List<Funcionario> Membros
        {
            get { return membros; }
            set { membros = value; }
        }
        #endregion

        #region Constructors
        public Equipa(string nomeEquipa)
        {
            this.nomeEquipa = nomeEquipa;
        }
        #endregion

        #region Methods
        public void AdicionarMembro(Funcionario funcionario)
        {
            membros.Add(funcionario);
        }

        public abstract void ExibirDetalhesEquipa();
        #endregion
    }
}


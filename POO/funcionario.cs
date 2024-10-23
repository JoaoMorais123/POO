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
    public abstract class Funcionario
    {
        #region Private Properties
        
        private string nome;
        private int numeroFuncionario;
        private string cargo;
        
        #endregion

        #region Public Properties
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public int NumeroFuncionario
        {
            get { return numeroFuncionario; }
            set { numeroFuncionario = value; }
        }

        public string Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }
        #endregion

        #region Constructors
        public Funcionario(string nome, int numeroFuncionario, string cargo)
        {
            this.nome = nome;
            this.numeroFuncionario = numeroFuncionario;
            this.cargo = cargo;
        }
        #endregion

        #region Abstract Method
        public abstract void ExibirDetalhes();
        #endregion
    }
}


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
    public class Enfermeiro : Funcionario
    {
        #region Private Properties
        private string especializacao;
        private int idHospital;
        #endregion

        #region Public Properties
        public string Especializacao
        {
            get { return especializacao; }
            set { especializacao = value; }
        }

        public int IDHospital
        {
            get { return idHospital; }
            set { idHospital = value; }
        }
        #endregion

        #region Constructors
        public Enfermeiro(string nome, int numeroFuncionario, string cargo, string especializacao, int idHospital)
            : base(nome, numeroFuncionario, cargo)
        {
            this.especializacao = especializacao;
            this.idHospital = idHospital;
        }
        #endregion

        #region Override Method
        public override void ExibirDetalhes()
        {
            Console.WriteLine($"Nome: {Nome}, Nº Funcionario: {NumeroFuncionario}, Cargo: {Cargo}, Especialização: {Especializacao}, ID Hospital: {IDHospital}");
        }
        #endregion
    }
}
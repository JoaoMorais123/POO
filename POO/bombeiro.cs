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
    public class Bombeiro : Funcionario
    {
        #region Private Properties
        private string idQuartel;
        private string patente;
        #endregion

        #region Public Properties
        public string IDQuartel
        {
            get { return idQuartel; }
            set { idQuartel = value; }
        }

        public string Patente
        {
            get { return patente; }
            set { patente = value; }
        }
        #endregion

        #region Constructors
        public Bombeiro(string nome, int numeroFuncionario, string cargo, string idQuartel, string patente)
            : base(nome, numeroFuncionario, cargo)
        {
            this.idQuartel = idQuartel;
            this.patente = patente;
        }
        #endregion

        #region Override Method
        public override void ExibirDetalhes()
        {
            Console.WriteLine($"Nome: {Nome}, Nº Funcionario: {NumeroFuncionario}, Cargo: {Cargo}, ID Quartel: {IDQuartel}, Patente: {Patente}");
        }
        #endregion
    }
}
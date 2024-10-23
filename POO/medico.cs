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
    public class Medico : Funcionario
    {
        public string Especializacao { get; set; }
        public int IDHospital { get; set; }

        public override void ExibirDetalhes() 
        {
            Console.WriteLine($"Nome: {Nome}, Nº Funcionario: {NumeroFuncionario}, Cargo: {Cargo}, Especialização: {Especializacao}, ID Hospital: {IDHospital}");
        }
    }
}
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
        public string NomeEquipa { get; set; } = "Nome padrão";
        public List<Funcionario> Membros { get; set; } = new List<Funcionario>();

        // Método abstrato para exibir detalhes da equipa
        public abstract void ExibirDetalhesEquipa();

        public void AdicionarMembro(Funcionario funcionario)
        {
            Membros.Add(funcionario);
        }
    }
}


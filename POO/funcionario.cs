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
        public string Nome { get; set; }
        public int NumeroFuncionario { get; set; }
        public string Cargo { get; set; }

        // Método abstrato para exibir detalhes, as subclasses serão obrigadas a implementar
        public abstract void ExibirDetalhes();
    }
}


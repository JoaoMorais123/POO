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


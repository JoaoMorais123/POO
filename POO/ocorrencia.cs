namespace POO
{
    public abstract class Ocorrencia
    {
        public DateTime DataOcorrencia { get; set; }
        public string Localizacao { get; set; }
        public string Descricao { get; set; }
        
        public string TipoAtendimento { get; set; }

        // Método abstrato para exibir os detalhes da ocorrência
        public abstract void ExibirDetalhesOcorrencia();
    }
}


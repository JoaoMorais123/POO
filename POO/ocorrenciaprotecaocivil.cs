namespace POO
{
    public class OcorrenciaProtecaoCivil : Ocorrencia
    {
        public string TipoOcorrencia { get; set; }
        public string Local { get; set; }
        
        public string Responsavel { get; set; }

        public override void ExibirDetalhesOcorrencia()
        {
            Console.WriteLine($"Data: {DataOcorrencia}, Localização: {Localizacao}, Descrição: {Descricao}, Tipo de Ocorrência: {TipoOcorrencia}");
        }
    }
}
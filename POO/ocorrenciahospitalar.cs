namespace POO
{
    public class OcorrenciaHospitalar : Ocorrencia
    {
        public string TipoOcorrencia { get; set; }

        public override void ExibirDetalhesOcorrencia()
        {
            Console.WriteLine($"Data: {DataOcorrencia}, Localização: {Localizacao}, Descrição: {Descricao}, Tipo de Ocorrência: {TipoOcorrencia}");
        }
    }
}


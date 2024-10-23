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
    public class OcorrenciaHospitalar : Ocorrencia
    {
        public string TipoOcorrencia { get; set; }

        public override void ExibirDetalhesOcorrencia()
        {
            Console.WriteLine($"Data: {DataOcorrencia}, Localização: {Localizacao}, Descrição: {Descricao}, Tipo de Ocorrência: {TipoOcorrencia}");
        }
    }
}


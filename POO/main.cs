namespace POO
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Criar uma ocorrência hospitalar
            OcorrenciaHospitalar ocorrenciaHospitalar = new OcorrenciaHospitalar
            {
                TipoOcorrencia = "Emergência Médica",
                Localizacao = "Hospital São João",
                DataOcorrencia = DateTime.Now,
                TipoAtendimento = "UTI"
            };

            // Criar uma ocorrência de Proteção Civil
            OcorrenciaProtecaoCivil ocorrenciaProtecaoCivil = new OcorrenciaProtecaoCivil
            {
                TipoOcorrencia = "Incêndio Florestal",
                Local = "Serra do Gerês",
                DataOcorrencia = DateTime.Now,
                Responsavel = "Proteção Civil do Porto"
            };

            // Exibir os detalhes das ocorrências
            ocorrenciaHospitalar.ExibirDetalhesOcorrencia();
            ocorrenciaProtecaoCivil.ExibirDetalhesOcorrencia();
            // Criar um bombeiro
            Bombeiro bombeiro = new Bombeiro
            {
                Nome = "João", NumeroFuncionario = 123, Cargo = "Bombeiro", IDQuartel = "Q123", Patente = "Sargento"
            };

            // Criar um médico
            Medico medico = new Medico
            {
                Nome = "Ana", NumeroFuncionario = 456, Cargo = "Médico", Especializacao = "Cardiologista", IDHospital = 789
            };

            // Criar uma equipa de INEM e adicionar o bombeiro e o médico
            Inem equipaInem = new Inem { NomeEquipa = "Equipa INEM Norte", ZonaAtuacao = "Porto" };
            equipaInem.AdicionarMembro(bombeiro);
            equipaInem.AdicionarMembro(medico);

            // Exibir os detalhes da equipa
            equipaInem.ExibirDetalhesEquipa();
        }
    }
}


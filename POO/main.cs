// -----------------------------------------------------------------------
// Autor: João Marcelo
// E-mail: a23041@alunos.ipca.pt
// Versão: 1.0.0
// Data: ${DATE}
// Disciplina: Programação Orientada Objetos
// Licença: MIT
// -----------------------------------------------------------------------

using System;

namespace POO
{
    class Program
    {
        static void Main(string[] args)
        {
            // Criar uma ocorrência hospitalar
            OcorrenciaHospitalar ocorrenciaHospitalar = new OcorrenciaHospitalar(
                DateTime.Now, // Data da ocorrência
                "Hospital São João", // Localização
                "Paciente com parada cardíaca", // Descrição
                "UTI", // Tipo de atendimento
                "Emergência Médica" // Tipo de ocorrência
            );

            // Criar uma ocorrência de Proteção Civil
            OcorrenciaProtecaoCivil ocorrenciaProtecaoCivil = new OcorrenciaProtecaoCivil(
                DateTime.Now, // Data da ocorrência
                "Serra do Gerês", // Localização
                "Incêndio de grande proporção", // Descrição
                "Resgate e Controle", // Tipo de atendimento
                "Incêndio Florestal" // Tipo de ocorrência
            );

            // Exibir os detalhes das ocorrências
            ocorrenciaHospitalar.ExibirDetalhesOcorrencia();
            ocorrenciaProtecaoCivil.ExibirDetalhesOcorrencia();

            // Criar um bombeiro
            Bombeiro bombeiro = new Bombeiro(
                "João", 123, "Bombeiro", "Q123", "Sargento"
            );

            // Criar um médico
            Medico medico = new Medico(
                "Ana", 456, "Médico", "Cardiologista", 789
            );

            // Criar uma equipa de INEM e adicionar o bombeiro e o médico
            Inem equipaInem = new Inem("Equipa INEM Norte", "Porto");
            equipaInem.AdicionarMembro(bombeiro);
            equipaInem.AdicionarMembro(medico);

            // Exibir os detalhes da equipa
            equipaInem.ExibirDetalhesEquipa();
        }
    }
}
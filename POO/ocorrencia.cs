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


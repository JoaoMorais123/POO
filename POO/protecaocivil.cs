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
    public class ProtecaoCivil : Equipa
    {
        public string AreaResponsabilidade { get; set; }

        public override void ExibirDetalhesEquipa()
        {
            Console.WriteLine($"Nome da Equipa: {NomeEquipa}, Área de Responsabilidade: {AreaResponsabilidade}");
            foreach (var membro in Membros)
            {
                membro.ExibirDetalhes();
            }
        }
    }
}
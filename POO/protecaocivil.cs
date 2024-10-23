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
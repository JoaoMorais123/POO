namespace POO
{
    public class Inem : Equipa
    {
        public string ZonaAtuacao { get; set; }
        public object Exibir { get; set; }

        public override void ExibirDetalhesEquipa()
        {
            Console.WriteLine($"Nome da Equipa: {NomeEquipa}, Zona de Atuação: {ZonaAtuacao}");
            foreach (var membro in Membros)
            {
                membro.ExibirDetalhes();
            }
        }
    }
}
namespace POO
{
    public class Bombeiro : Funcionario
    {
        public string IDQuartel { get; set; }
        public string Patente { get; set; }

        // Implementação do método abstrato
        public override void ExibirDetalhes() 
        {
            Console.WriteLine($"Nome: {Nome}, Nº Funcionario: {NumeroFuncionario}, Cargo: {Cargo}, ID Quartel: {IDQuartel}, Patente: {Patente}");
        }
    }
}
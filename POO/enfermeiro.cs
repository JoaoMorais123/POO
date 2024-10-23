namespace POO
{
    public class Enfermeiro : Funcionario
    {
        public string Especializacao { get; set; }
        public int IDHospital { get; set; }

        public override void ExibirDetalhes() 
        {
            Console.WriteLine($"Nome: {Nome}, Nº Funcionario: {NumeroFuncionario}, Cargo: {Cargo}, Especialização: {Especializacao}, ID Hospital: {IDHospital}");
        }
    }
}
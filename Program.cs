using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ValidadorCpfCnpjApi1
{
    // Classe principal do programa
    public class Program
    {
        // Método principal que é o ponto de entrada da aplicação
        public static void Main(string[] args)
        {
            // Cria o host builder, configura e executa a aplicação
            CreateHostBuilder(args).Build().Run();
        }

        // Método para criar e configurar o host builder
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Especifica a classe de inicialização da aplicação
                    webBuilder.UseStartup<Startup>();
                });
    }
}





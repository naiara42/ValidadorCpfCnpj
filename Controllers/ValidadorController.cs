using Microsoft.AspNetCore.Mvc;

namespace ValidadorCpfCnpjApi1.Controllers
{
    // Define the route for this controller / Define a rota para este controlador
    [Route("api/[controller]")]
    // Indicates that this is an API controller / Indica que este é um controlador de API
    [ApiController]
    public class ValidadorController : ControllerBase
    {
        // HTTP GET method that validates the given document / Método HTTP GET que valida o documento fornecido
        [HttpGet("validar/{documento}")]
        public IActionResult Validar(string documento)
        {
            // Check if the document is a CPF / Verifica se o documento é um CPF
            if (documento.Length == 11 && IsCpf(documento))
            {
                return Ok("CPF válido");
            }
            // Check if the document is a CNPJ / Verifica se o documento é um CNPJ
            else if (documento.Length == 14 && IsCnpj(documento))
            {
                return Ok("CNPJ válido");
            }
            else
            {
                return BadRequest("Documento inválido");
            }
        }

        // Method to validate CPF / Método para validar CPF
        private bool IsCpf(string cpf)
        {
            // Remove unnecessary characters / Remove caracteres desnecessários
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            // Check the length and if all digits are the same / Verifica o comprimento e se todos os dígitos são iguais
            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            int[] digits = cpf.Select(c => int.Parse(c.ToString())).ToArray();
            int sum = 0, weight;

            // Validate the first digit / Valida o primeiro dígito
            weight = 10;
            for (int i = 0; i < 9; i++)
            {
                sum += digits[i] * weight--;
            }

            int remainder = (sum * 10) % 11;
            if (remainder == 10) remainder = 0;
            if (remainder != digits[9]) return false;

            // Validate the second digit / Valida o segundo dígito
            sum = 0;
            weight = 11;
            for (int i = 0; i < 10; i++)
            {
                sum += digits[i] * weight--;
            }

            remainder = (sum * 10) % 11;
            if (remainder == 10) remainder = 0;
            return remainder == digits[10];
        }

        // Method to validate CNPJ / Método para validar CNPJ
        private bool IsCnpj(string cnpj)
        {
            // Remove unnecessary characters / Remove caracteres desnecessários
            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");

            // Check the length and if all digits are the same / Verifica o comprimento e se todos os dígitos são iguais
            if (cnpj.Length != 14 || cnpj.All(c => c == cnpj[0]))
                return false;

            int[] digits = cnpj.Select(c => int.Parse(c.ToString())).ToArray();
            int sum = 0, weight;

            // Validate the first digit / Valida o primeiro dígito
            weight = 5;
            for (int i = 0; i < 12; i++)
            {
                sum += digits[i] * weight;
                weight = weight == 2 ? 9 : weight - 1;
            }

            int remainder = (sum % 11);
            if (remainder < 2) remainder = 0;
            else remainder = 11 - remainder;

            if (remainder != digits[12]) return false;

            // Validate the second digit / Valida o segundo dígito
            sum = 0;
            weight = 6;
            for (int i = 0; i < 13; i++)
            {
                sum += digits[i] * weight;
                weight = weight == 2 ? 9 : weight - 1;
            }

            remainder = (sum % 11);
            if (remainder < 2) remainder = 0;
            else remainder = 11 - remainder;

            return remainder == digits[13];
        }
    }
}



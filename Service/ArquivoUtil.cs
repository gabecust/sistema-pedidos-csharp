using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace ProjetoLoja.Service
{
    public class ArquivoUtil
    {
        public void SalvarEmArquivo<T>(string caminho, List<T> dados)
        {
            var json = JsonSerializer.Serialize(dados, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(caminho, json);
        }

        public List<T> CarregarDeArquivo<T>(string caminho)
        {
            if (!File.Exists(caminho))
                return new List<T>();

            var json = File.ReadAllText(caminho);
            try
            {
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    ReadCommentHandling = JsonCommentHandling.Skip
                };
                return JsonSerializer.Deserialize<List<T>>(json, options)
                       ?? new List<T>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine(
                  $"Aviso: JSON inválido em {caminho}. Carregando lista vazia. " +
                  $"Detalhes: {ex.Message}");
                return new List<T>();
            }
        }
    }
}

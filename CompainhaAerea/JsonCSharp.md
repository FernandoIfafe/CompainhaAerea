# Trabalhando com System.Text.Json em C#

## Introdução

`System.Text.Json` é uma biblioteca integrada ao .NET (disponível a partir do .NET Core 3.0 e .NET 5+) para serialização e desserialização de objetos JSON. Ela é uma alternativa mais eficiente e moderna ao `Newtonsoft.Json` (Json.NET), oferecendo melhor desempenho e menor uso de memória. Este guia detalha como usar `System.Text.Json` para trabalhar com JSON em aplicações C#.

## Pré-requisitos

- .NET Core 3.0 ou superior, ou .NET 5+.
- Para versões mais antigas do .NET Framework, considere usar `Newtonsoft.Json`.
- Adicione a referência ao namespace: `using System.Text.Json;`

## Serialização Básica

A serialização converte um objeto C# em uma string JSON.

### Exemplo Simples

```csharp
using System;
using System.Text.Json;

public class Pessoa
{
    public string Nome { get; set; }
    public int Idade { get; set; }
}

class Program
{
    static void Main()
    {
        var pessoa = new Pessoa { Nome = "João", Idade = 30 };
        string json = JsonSerializer.Serialize(pessoa);
        Console.WriteLine(json); // {"Nome":"João","Idade":30}
    }
}
```

### Serializando para Stream ou Arquivo

```csharp
using System.IO;

// Serializando para um arquivo
await File.WriteAllTextAsync("pessoa.json", JsonSerializer.Serialize(pessoa));

// Serializando para um stream
using var stream = new MemoryStream();
await JsonSerializer.SerializeAsync(stream, pessoa);
```

## Desserialização Básica

A desserialização converte uma string JSON em um objeto C#.

### Exemplo Simples

```csharp
string json = "{\"Nome\":\"João\",\"Idade\":30}";
var pessoa = JsonSerializer.Deserialize<Pessoa>(json);
Console.WriteLine($"{pessoa.Nome} tem {pessoa.Idade} anos.");
```

### Desserializando de Stream ou Arquivo

```csharp
// De um arquivo
string jsonFromFile = await File.ReadAllTextAsync("pessoa.json");
var pessoaFromFile = JsonSerializer.Deserialize<Pessoa>(jsonFromFile);

// De um stream
using var stream = new FileStream("pessoa.json", FileMode.Open);
var pessoaFromStream = await JsonSerializer.DeserializeAsync<Pessoa>(stream);
```

## Trabalhando com Coleções

`System.Text.Json` suporta listas, arrays e dicionários.

### Exemplo com Lista

```csharp
var pessoas = new List<Pessoa>
{
    new Pessoa { Nome = "Ana", Idade = 25 },
    new Pessoa { Nome = "Carlos", Idade = 35 }
};

string json = JsonSerializer.Serialize(pessoas);
Console.WriteLine(json); // [{"Nome":"Ana","Idade":25},{"Nome":"Carlos","Idade":35}]

var pessoasDesserializadas = JsonSerializer.Deserialize<List<Pessoa>>(json);
```

### Exemplo com Dicionário

```csharp
var dicionario = new Dictionary<string, int>
{
    ["chave1"] = 1,
    ["chave2"] = 2
};

string json = JsonSerializer.Serialize(dicionario);
var dicionarioDesserializado = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
```

## Usando JsonSerializerOptions

`JsonSerializerOptions` permite personalizar o comportamento da serialização/desserialização.

### Opções Comuns

- `PropertyNamingPolicy`: Define a convenção de nomes (ex.: camelCase).
- `WriteIndented`: Formata o JSON com indentação.
- `IgnoreNullValues`: Ignora propriedades nulas.
- `DefaultIgnoreCondition`: Ignora propriedades com valores padrão.

### Exemplo

```csharp
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};

var pessoa = new Pessoa { Nome = "João", Idade = 30 };
string json = JsonSerializer.Serialize(pessoa, options);
Console.WriteLine(json);
// {
//   "nome": "João",
//   "idade": 30
// }
```

### Outras Opções Importantes de JsonSerializerOptions

- `AllowTrailingCommas`: Permite vírgulas finais em arrays e objetos JSON (padrão: false).
- `ReadCommentHandling`: Controla como comentários no JSON são tratados (ex.: Skip para ignorar).
- `NumberHandling`: Define como números são tratados (ex.: AllowReadingFromString para aceitar strings como números).
- `Encoder`: Define o codificador para caracteres especiais (ex.: JavaScriptEncoder.UnsafeRelaxedJsonEscaping).
- `MaxDepth`: Define a profundidade máxima para desserialização (protege contra ataques de negação de serviço).
- `ReferenceHandler`: Trata referências circulares (ex.: Preserve para manter referências).
- `TypeInfoResolver`: Para reflexão personalizada (avançado, para cenários de AOT).

Exemplo com mais opções:

```csharp
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    AllowTrailingCommas = true,
    ReadCommentHandling = JsonCommentHandling.Skip,
    MaxDepth = 10
};
```

## Métodos Assíncronos do JsonSerializer

`System.Text.Json` oferece métodos assíncronos para operações de I/O, evitando bloqueios em threads durante leituras/escritas de streams ou arquivos grandes.

### SerializeAsync

Serializa um objeto para um stream de forma assíncrona.

```csharp
using System.IO;

// Exemplo: Serializando para um arquivo
var pessoa = new Pessoa { Nome = "João", Idade = 30 };
using var stream = new FileStream("pessoa.json", FileMode.Create);
await JsonSerializer.SerializeAsync(stream, pessoa, options);
```

### DeserializeAsync

Desserializa de um stream de forma assíncrona.

```csharp
using var stream = new FileStream("pessoa.json", FileMode.Open);
var pessoa = await JsonSerializer.DeserializeAsync<Pessoa>(stream, options);
```

### Vantagens dos Métodos Assíncronos

- Não bloqueiam a thread principal, melhorando a responsividade em aplicações web ou UI.
- Ideais para arquivos grandes ou streams de rede.
- Use sempre que possível em cenários assíncronos para evitar deadlocks.

## Atributos JSON

Use atributos para controlar a serialização.

- `[JsonPropertyName]`: Define o nome da propriedade no JSON.
- `[JsonIgnore]`: Ignora a propriedade.
- `[JsonInclude]`: Inclui propriedades privadas ou campos.

### Exemplo

```csharp
public class Produto
{
    [JsonPropertyName("nome_produto")]
    public string Nome { get; set; }

    [JsonIgnore]
    public decimal PrecoInterno { get; set; }

    [JsonInclude]
    private string CodigoSecreto { get; set; } = "ABC123";
}
```

## Trabalhando com Tipos Complexos

### Datas e Horas

Por padrão, datas são serializadas em ISO 8601. Use `JsonSerializerOptions` para personalizar.

```csharp
var options = new JsonSerializerOptions
{
    Converters = { new JsonStringEnumConverter() } // Para enums
};

public enum Status { Ativo, Inativo }

public class Item
{
    public DateTime DataCriacao { get; set; }
    public Status Status { get; set; }
}
```

### Conversores Personalizados

Implemente `JsonConverter<T>` para tipos customizados.

```csharp
public class PessoaConverter : JsonConverter<Pessoa>
{
    public override Pessoa Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Lógica de desserialização
        return new Pessoa { Nome = "Desserializado" };
    }

    public override void Write(Utf8JsonWriter writer, Pessoa value, JsonSerializerOptions options)
    {
        // Lógica de serialização
        writer.WriteStringValue(value.Nome);
    }
}

// Uso
var options = new JsonSerializerOptions
{
    Converters = { new PessoaConverter() }
};
```

## Tratamento de Erros

Use try-catch para capturar erros de desserialização.

```csharp
try
{
    var pessoa = JsonSerializer.Deserialize<Pessoa>(jsonInvalido);
}
catch (JsonException ex)
{
    Console.WriteLine($"Erro de JSON: {ex.Message}");
}
```

## Comparação com Newtonsoft.Json

- `System.Text.Json` é mais rápido e usa menos memória.
- Suporte limitado a alguns recursos avançados do Newtonsoft (ex.: referências circulares).
- Para migração, use `JsonSerializerOptions` para replicar comportamentos.

## Exemplos Avançados

### Serialização Condicional

```csharp
public class Configuracao
{
    public string Nome { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Descricao { get; set; }
}
```

### Trabalhando com JSON Aninhado

```csharp
public class Empresa
{
    public string Nome { get; set; }
    public List<Pessoa> Funcionarios { get; set; }
}
```

## Trabalhando com JSON Dinâmico usando JsonNode

A partir do .NET 6, `JsonNode` permite manipular JSON de forma dinâmica, sem definir classes fixas. É útil para JSONs desconhecidos ou modificações em tempo real.

### Exemplo Básico

```csharp
using System.Text.Json.Nodes;

// Criando um JsonObject dinamicamente
JsonObject obj = new JsonObject
{
    ["nome"] = "João",
    ["idade"] = 30,
    ["endereco"] = new JsonObject
    {
        ["rua"] = "Rua A",
        ["cidade"] = "São Paulo"
    }
};

// Serializando
string json = obj.ToJsonString();
Console.WriteLine(json);

// Modificando
obj["idade"] = 31;

// Desserializando de string
JsonNode node = JsonNode.Parse(json);
string nome = node["nome"].GetValue<string>();
```

### Teste do meu código
```csharp
public class Pessoa
    {
        [JsonInclude]
        private MyStruct[] pessoas { get; set; } = new MyStruct[4];
        public Pessoa()
        {
            for (int i = 0; i < pessoas.Length; i++)
            {
                pessoas[i] = new MyStruct();
            }
        }
        public void setPessoas(int indice,  int idade, string nome)
        {
            if (indice >= 0 && indice < pessoas.Length)
            {
                pessoas[indice] = new  MyStruct(nome, idade, "ghjhghjhgh");
            }
        }
        public void Consultar()
        {
            for (int i = 0; i < pessoas.Length; i++)
            {
                pessoas[i].ExibirDados();
            }
        }
        private class MyStruct
        {
            [JsonInclude]
            string? Nome { get; set; }
            [JsonInclude]
            int Idade { get; set; }
            [JsonInclude]
            string? Endereco { get; set; }
            public MyStruct(string? nome, int idade, string? endereco)
            {
                Nome = nome;
                Idade = idade;
                Endereco = endereco;
            }
            public MyStruct()
            {
                
            }
            public void ExibirDados()
            {
                if (!(Nome is null))
                {
                    Console.WriteLine($"Nome: {Nome}");
                    Console.WriteLine($"Idade: {Idade}");
                    Console.WriteLine($"Endereco: {Endereco}");
                }
            }
        }
    }
    
    Pessoa p = new Pessoa();
    p.setPessoas(0, 18, "Adao Ifafe");
    p.setPessoas(3, 21, "Adao Lufuma Fernando Ifafe");
    p.Consultar();
    var Json = JsonSerializer.Serialize(p, new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
    Console.WriteLine("Na serialização: " + Json + "\n");
    Pessoa p2 = new Pessoa();
    p2 = JsonSerializer.Deserialize<Pessoa>(Json);
    Console.Write("Pressione enter para continuar: ");
    Console.ReadKey();
    Console.Clear();
    Console.WriteLine("Na deserialização: \n");
    p2.Consultar();
    return 0;
```

### Vantagens

- Flexibilidade para JSONs variáveis.
- Integração com LINQ para consultas.
- Menos código para estruturas simples.

## Conclusão

`System.Text.Json` é uma ferramenta poderosa para trabalhar com JSON em C#. Comece com serialização/desserialização básica e explore opções avançadas conforme necessário. Para mais detalhes, consulte a documentação oficial da Microsoft: [System.Text.Json Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.text.json).

Este guia cobre os conceitos fundamentais. Experimente os exemplos em seu ambiente de desenvolvimento!
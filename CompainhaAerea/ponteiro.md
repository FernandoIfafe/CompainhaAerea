# Span&lt;T&gt; e Memory&lt;T&gt; em C#

## Introdução
Span&lt;T&gt; e Memory&lt;T&gt; são tipos do .NET (C#) introduzidos no namespace `System.Memory` para manipulação eficiente de memória sem cópias desnecessárias. Eles são úteis para trabalhar com buffers de dados contíguos de forma segura e performática.

### Span&lt;T&gt;
- **O que é**: Uma estrutura que representa uma região contígua de memória (como um array ou parte dele). É leve, não aloca memória própria e é ideal para operações de leitura/escrita em buffers.
- **Quando usar**: Para manipular arrays, strings ou buffers de forma eficiente, especialmente em métodos que precisam de acesso direto à memória sem cópias.
- **Características**: Não possui propriedade de propriedade da memória; é uma "visão" sobre dados existentes. É stack-only (não pode ser armazenado em heap diretamente).

**Exemplo básico**:
```csharp
using System;

class Program
{
    static void Main()
    {
        int[] array = { 1, 2, 3, 4, 5 };
        
        // Criar um Span<T> sobre o array inteiro
        Span<int> span = array;
        Console.WriteLine(span[0]); // 1
        
        // Criar um Span<T> sobre uma parte do array (slice)
        Span<int> slice = span.Slice(1, 3); // {2, 3, 4}
        slice[0] = 99; // Modifica o array original
        Console.WriteLine(array[1]); // 99
        
        // Usar em métodos
        ProcessSpan(span);
    }
    
    static void ProcessSpan(Span<int> data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] *= 2;
        }
    }
}
```

### Memory&lt;T&gt;
- **O que é**: Uma estrutura que representa memória gerenciada (managed memory), como arrays ou buffers alocados no heap. Pode ser usada para armazenar dados que precisam ser passados entre threads ou métodos assincronos.
- **Quando usar**: Quando você precisa de uma "visão" sobre memória que pode ser movida ou compartilhada, especialmente em cenários assíncronos ou com pooling de memória.
- **Características**: Pode ser convertido para Span&lt;T&gt; via `.Span`, mas Memory&lt;T&gt; própria não permite acesso direto (use Span&lt;T&gt; para operações).

**Exemplo básico**:
```csharp
using System;
using System.Buffers;

class Program
{
    static void Main()
    {
        // Alocar memória usando MemoryPool
        using (MemoryPool<int>.Shared.Rent(5, out Memory<int> memory))
        {
            Span<int> span = memory.Span; // Converter para Span para acesso
            span[0] = 10;
            span[1] = 20;
            
            Console.WriteLine(span[0]); // 10
            
            // Passar para um método assíncrono
            ProcessMemoryAsync(memory).Wait();
        }
    }
    
    static async Task ProcessMemoryAsync(Memory<int> data)
    {
        await Task.Delay(100); // Simular operação assíncrona
        Span<int> span = data.Span;
        span[0] = 999;
    }
}
```

## Diferenças e Casos de Uso Avançados

### Diferenças Principais
- **Span&lt;T&gt;**: 
  - **Escopo**: Stack-only; não pode ser armazenado em campos de classe ou retornado de métodos assíncronos sem conversão.
  - **Propriedade**: Não possui a memória; é uma referência temporária a dados existentes (arrays, strings, buffers nativos).
  - **Conversão**: Pode ser criado de `Memory<T>` via `.Span`, mas o inverso não é direto.
  - **Uso Ideal**: Operações síncronas rápidas, como parsing de dados ou manipulação de buffers em loops.

- **Memory&lt;T&gt;**:
  - **Escopo**: Pode ser armazenado em heap (campos de classe, async/await).
  - **Propriedade**: Pode gerenciar memória própria (ex.: via `MemoryPool`) ou referenciar dados existentes.
  - **Conversão**: Converte para `Span<T>` via `.Span` para acesso, mas não vice-versa diretamente.
  - **Uso Ideal**: Cenários assíncronos, compartilhamento entre threads ou pooling de memória para reduzir GC.

Ambos suportam slicing (`.Slice()`), são ref structs (Span&lt;T&gt;) ou structs (Memory&lt;T&gt;), e evitam boxing/cópias.

### Exemplos Avançados

1. **Span&lt;T&gt; com Buffers Nativos (Unsafe)**:
   ```csharp
   using System;
   using System.Runtime.InteropServices;

   unsafe class Program
   {
       static void Main()
       {
           // Buffer nativo (ex.: de P/Invoke)
           IntPtr ptr = Marshal.AllocHGlobal(10 * sizeof(int));
           try
           {
               Span<int> span = new Span<int>((int*)ptr, 10);
               span[0] = 42;
               Console.WriteLine(span[0]); // 42
           }
           finally
           {
               Marshal.FreeHGlobal(ptr);
           }
       }
   }
   ```

2. **Memory&lt;T&gt; com Pooling para Performance**:
   ```csharp
   using System;
   using System.Buffers;
   using System.Threading.Tasks;

   class Program
   {
       static async Task Main()
       {
           // Pool de memória para reutilização
           MemoryPool<byte> pool = MemoryPool<byte>.Shared;
           using (IMemoryOwner<byte> owner = pool.Rent(1024))
           {
               Memory<byte> memory = owner.Memory;
               Span<byte> span = memory.Span;
               
               // Preencher dados
               for (int i = 0; i < span.Length; i++) span[i] = (byte)i;
               
               // Passar para tarefa assíncrona
               await ProcessAsync(memory);
           }
       }
       
       static async Task ProcessAsync(Memory<byte> data)
       {
           await Task.Delay(100);
           Span<byte> span = data.Span;
           Console.WriteLine(span[0]); // 0
       }
   }
   ```

3. **Conversão e Slicing**:
   ```csharp
   using System;

   class Program
   {
       static void Main()
       {
           Memory<int> memory = new int[] { 1, 2, 3, 4, 5 };
           Span<int> span = memory.Span; // Converter Memory para Span
           
           Span<int> subSpan = span.Slice(1, 3); // {2, 3, 4}
           subSpan.Reverse(); // Modifica in-place
           
           Console.WriteLine(string.Join(", ", memory.Span.ToArray())); // 1, 4, 3, 2, 5
       }
   }
   ```

## Considerações Adicionais
- **Performance**: Span&lt;T&gt; é mais rápido para acesso direto; Memory&lt;T&gt; adiciona overhead para gerenciamento.
- **Thread Safety**: Memory&lt;T&gt; é mais seguro para compartilhamento; Span&lt;T&gt; não é thread-safe.
- **Limitações**: Span&lt;T&gt; não suporta async diretamente (use `Memory<T>` ou `ValueTask` com `ReadOnlyMemory<T>`).
- **Alternativas em C++**: Use `std::span` (C++20) para visões de memória, e `std::unique_ptr` ou `std::vector` para gerenciamento. Não há equivalente direto a Memory&lt;T&gt;.
- **Bibliotecas**: Para .NET antigo, instale `System.Memory` via NuGet.
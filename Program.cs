using System;
using System.Globalization;

namespace Pluton
{
    class Program
    {
        const int MAX = 50;

        static void Main()
        {
            int[] ra = new int[MAX];
            double[] nota = new double[MAX];
            int n = 0;

            CadastrarTurma(ra, nota, ref n);

            int opcao;
            do
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1 – Buscar aluno pelo RA");
                Console.WriteLine("2 – Remover aluno (evasão)");
                Console.WriteLine("3 – Ordenar turma por média (Bubble Sort decrescente)");
                Console.WriteLine("4 – Listar alunos");
                Console.WriteLine("0 – Sair");
                Console.Write("Escolha: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Write("Digite o RA a buscar: ");
                        int raBusca = int.Parse(Console.ReadLine());
                        int idx = BuscarRA(ra, n, raBusca);
                        if (idx != -1)
                            Console.WriteLine($"Aluno encontrado: RA = {ra[idx]}, Média = {nota[idx].ToString("F2", CultureInfo.InvariantCulture)}");
                        else
                            Console.WriteLine("RA não encontrado.");
                        break;

                    case 2:
                        Console.Write("Digite o RA a remover: ");
                        int raRemover = int.Parse(Console.ReadLine());
                        int pos = BuscarRA(ra, n, raRemover);
                        if (pos != -1)
                        {
                            RemoverAluno(ra, nota, ref n, pos);
                            Console.WriteLine("Aluno removido.");
                        }
                        else
                            Console.WriteLine("RA não encontrado.");
                        break;

                    case 3:
                        int trocas = OrdenarPorNota(ra, nota, n);
                        Console.WriteLine($"Turma ordenada. Número de trocas: {trocas}");
                        break;

                    case 4:
                        ListarAlunos(ra, nota, n);
                        break;

                    case 0:
                        Console.WriteLine("Saindo...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (opcao != 0);
        }

        static void CadastrarTurma(int[] ra, double[] nota, ref int n)
        {
            int total;
            do
            {
                Console.Write("Quantos alunos deseja cadastrar? (1 a 50): ");
                total = int.Parse(Console.ReadLine());
            } while (total < 1 || total > MAX);

            for (int i = 0; i < total; i++)
            {
                int novoRA;
                while (true)
                {
                    Console.Write($"RA do aluno {i + 1}: ");
                    novoRA = int.Parse(Console.ReadLine());
                    if (BuscarRA(ra, n, novoRA) == -1)
                    {
                        ra[n] = novoRA;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("RA já cadastrado. Digite outro.");
                    }
                }

                Console.Write($"Média final do aluno {i + 1}: ");
                nota[n] = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                n++;
            }
        }

        // Busca linear – Complexidade: O(n)
        static int BuscarRA(int[] ra, int n, int alvo)
        {
            for (int i = 0; i < n; i++)
            {
                if (ra[i] == alvo)
                    return i;
            }
            return -1;
        }

        // Remoção com deslocamento – Complexidade: O(n)
        static void RemoverAluno(int[] ra, double[] nota, ref int n, int pos)
        {
            for (int i = pos; i < n - 1; i++)
            {
                ra[i] = ra[i + 1];
                nota[i] = nota[i + 1];
            }
            n--;
        }

        // Bubble Sort decrescente – Complexidade: O(n²)
        static int OrdenarPorNota(int[] ra, double[] nota, int n)
        {
            int trocas = 0;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (nota[j] < nota[j + 1])
                    {
                        // Trocar notas
                        double tempNota = nota[j];
                        nota[j] = nota[j + 1];
                        nota[j + 1] = tempNota;

                        // Trocar RA
                        int tempRA = ra[j];
                        ra[j] = ra[j + 1];
                        ra[j + 1] = tempRA;

                        trocas++;
                    }
                }
            }
            return trocas;
        }

        // Listagem da turma – Complexidade: O(n)
        static void ListarAlunos(int[] ra, double[] nota, int n)
        {
            if (n == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
                return;
            }

            Console.WriteLine("Listagem da turma:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{ra[i]} - {nota[i].ToString("F2", CultureInfo.InvariantCulture)}");
            }
        }
    }
}

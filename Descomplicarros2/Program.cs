using System.Data.SqlClient;
using System.Runtime.CompilerServices;

class Program
{
    static readonly string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=DESCOMPLICARROS2;Integrated Security=True;TrustServerCertificate=true;";
    static readonly string[] entidadesNegocio = ["usuários", "perfil de usuários", "marcas montadoras", "modelos", "tipos de cambio", "carrocerias", "veiculos", "vendedores"];
    static readonly string[] entidadesNegocioSingular = ["usuário", "perfil de usuário", "marca montadora", "modelo", "tipo de cambio", "carroceria", "veiculo", "vendedor"];
    static readonly string[] tabelasEntidadesNegocio = ["usuarios", "perfilusuarios", "marcas_montadoras", "modelos", "tipocambios", "carrocerias", "veiculos", "vendedores"];
    static readonly string[] chavesPrimariasTabelas = ["Idusuario", "IdPerfilusuario", "IdMarcasMontadora", "IdModelo", "IdTipoCambio", "IdCarroceria", "IdVeiculo", "IdVendedor"];

    static void Main()
    {
        Console.WriteLine("Seja bem vindo ao sistema descomplicarros");

        while (true)
        {
            Console.WriteLine("\nMenu principal");
            for (int i = 0; i < entidadesNegocio.Length; i++)
            {
                Console.WriteLine($"{i+1}. Gerenciar {entidadesNegocio[i]}");
            }
            Console.WriteLine("9. Sair");

            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();


            switch (opcao)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                    int opcaoNumerica = int.Parse(opcao) - 1;
                    MenuCrud(opcaoNumerica);
                    break;
                case "9":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void MenuCrud(int indexEntidade)
    {
        string nomeEntidadeNegocio = entidadesNegocio[indexEntidade];
        string nomeentidadesNegocioSingular = entidadesNegocioSingular[indexEntidade];
        string nomeTabela = tabelasEntidadesNegocio[indexEntidade];
        string chavePrimariasTabela = chavesPrimariasTabelas[indexEntidade];

        while (true)
        {
            Console.WriteLine($"\nMenu CRUD de {nomeEntidadeNegocio}");
            Console.WriteLine($"1. Listar todos os {nomeEntidadeNegocio}");
            Console.WriteLine($"2. Buscar {nomeentidadesNegocioSingular} por ID");
            Console.WriteLine($"3. Criar novo {nomeentidadesNegocioSingular}");
            Console.WriteLine($"4. Atualizar {nomeentidadesNegocioSingular}");
            Console.WriteLine($"5. Deletar {nomeentidadesNegocioSingular}");
            Console.WriteLine("6. Sair");

            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ListarRegistros($"{nomeTabela}");
                    break;
                case "2":
                    BuscarPorId($"{nomeTabela}", $"{chavePrimariasTabela}");
                    break;
                case "3":
                    CriarNovoRegistro($"{nomeTabela}");
                    break;
                case "4":
                    AtualizarRegistroPorId($"{nomeTabela}", $"{chavePrimariasTabela}");
                    break;
                case "5":
                    DeletarPorId($"{nomeTabela}", $"{chavePrimariasTabela}");
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    #region metodos de conexão com o banco de dados

    #endregion
    static void ListarRegistros(string nomeTabela)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $"SELECT * FROM {nomeTabela}";

            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine($"Lista de Registros da Tabela {nomeTabela}:");

                // Imprimir cabeçalho com os nomes das colunas
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader.GetName(i)}\t");
                }
                Console.WriteLine();

                while (reader.Read())
                {
                    // Imprimir valores das colunas
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader[i]}\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }

    static void BuscarPorId(string nomeTabela, string nomeCampoId)
    {
        Console.Write($"Digite o ID do {nomeTabela}: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM {nomeTabela} WHERE {nomeCampoId} = @{nomeCampoId}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue($"@{nomeCampoId}", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Detalhes do {nomeTabela} ({nomeCampoId}: {id}):");

                            // Imprimir cabeçalho com os nomes das colunas
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write($"{reader.GetName(i)}\t");
                            }
                            Console.WriteLine();

                            // Imprimir valores das colunas
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write($"{reader[i]}\t");
                            }
                            Console.WriteLine("\n");
                        }
                        else
                        {
                            Console.WriteLine($"{nomeTabela} não encontrado.\n");
                        }
                    }
                }

                connection.Close();
            }
        }
        else
        {
            Console.WriteLine("ID inválido.\n");
        }
    }

    static void CriarNovoRegistro(string nomeTabela)
    {
        Dictionary<string, string> camposValores = ObterValoresCampos(nomeTabela);

        if (!camposValores.Any())
        {
            Console.WriteLine($"Informações digitadas para a tabela {nomeTabela} são inválidas ou insuficientes para criar um cadastro.\n");
            return;
        }




        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string campos = string.Join(", ", camposValores.Keys);
            string valores = string.Join(", ", camposValores.Values.Select(valor => $"'{valor}'"));

            string query = $"INSERT INTO {nomeTabela} ({campos}) VALUES ({valores})";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                foreach (var keyValue in camposValores)
                {
                    command.Parameters.AddWithValue($"@{keyValue.Key}", keyValue.Value);
                }

                command.ExecuteNonQuery();
                Console.WriteLine($"Novo registro em {nomeTabela} criado com sucesso.\n");
            }
        }
    }

    static Dictionary<string, string> ObterValoresCampos(string nomeTabela)
    {
        Dictionary<string, string> camposValores = new Dictionary<string, string>();

        Console.WriteLine($"Digite os valores para cada campo da tabela {nomeTabela}:");

        camposValores = ObterValoresTabela(nomeTabela.ToLower());

        if (!camposValores.Any())
        {
            Console.WriteLine($"A criação de registro para a tabela {nomeTabela} ainda não foi implementada ou não teve campos informados.");
        }

        return camposValores;
    }

    private static Dictionary<string, string> ObterValoresTabela(string nomeTabela)
    {
        Dictionary<string, string> camposValores = new Dictionary<string, string>();
        Dictionary<string, string> descricaoCampos = new Dictionary<string, string>();

        switch (nomeTabela)
        {
            case "usuarios":
                descricaoCampos = ObterCamposCadastroUsuarios();
                break;
            case "perfilusuarios":
                descricaoCampos = ObterCamposCadastroPerfilUsuarios();
                break;
            case "tipocambios":
                descricaoCampos = ObterCamposCadastroTipoCambio();
                break;
            case "carrocerias":
                descricaoCampos = ObterCamposCadastroCarroceria();
                break;
            case "marcas_montadoras":
                descricaoCampos = ObterCamposCadastroMarcaMontadora();
                break;
            case "modelos":
                descricaoCampos = ObterCamposCadastroModelo();
                break;
            case "veiculos":
                descricaoCampos = ObterCamposCadastroVeiculo();
                break;
            case "vendedores":
                descricaoCampos = ObterCamposCadastroVendedor();
                break;
            default:
                Console.WriteLine($"Os campos para a tabela {nomeTabela} ainda não foram especificados.");
                break;
        }

        foreach (var descricaoCampo in descricaoCampos)
        {
            Console.Write(descricaoCampo.Key);
            camposValores.Add(descricaoCampo.Value, Console.ReadLine());
        }
        return camposValores;
    }

    private static Dictionary<string, string> ObterCamposCadastroUsuarios()
    {
        Dictionary<string, string> descricaoCampos = new Dictionary<string, string>();
        descricaoCampos.Add("ID do Perfil: ", "IdPerfilUsuario");
        descricaoCampos.Add("Nome do Usuário: ", "NomeUsuario");
        descricaoCampos.Add("CPF do Usuário: ", "CpfUsuario");
        descricaoCampos.Add("Data de Nascimento do Usuário(aaaa-mm-dd): ", "DtNascimentoUsuario");
        descricaoCampos.Add("Email do Usuário: ", "EmailUsuario");
        descricaoCampos.Add("Senha do Usuário: ", "Senha");
        descricaoCampos.Add("Ativo(0 ou 1): ", "Ativo");
        descricaoCampos.Add("ID do Usuário que está cadastrando: ", "IdUsuarioCadastro");
        return descricaoCampos;
    }

    private static Dictionary<string, string> ObterCamposCadastroPerfilUsuarios()
    {
        Dictionary<string, string> descricaoCampos = new Dictionary<string, string>();
        descricaoCampos.Add("Nome do perfil de Usuário: ", "NomePerfilUsuario");
        descricaoCampos.Add("Descricao do perfil de Usuário: ", "DescricaoPerfilUsuario");
        descricaoCampos.Add("Ativo(0 ou 1): ", "Ativo");
        descricaoCampos.Add("ID do Usuário que está cadastrando: ", "IdUsuarioCadastro");
        return descricaoCampos;
    }

    private static Dictionary<string, string> ObterCamposCadastroTipoCambio()
    {
        Dictionary<string, string> descricaoCampos = new Dictionary<string, string>();
        descricaoCampos.Add("Nome do tipo de cambio: ", "NomeTipoCambio");
        descricaoCampos.Add("Ativo(0 ou 1): ", "Ativo");
        descricaoCampos.Add("ID do Usuário que está cadastrando: ", "IdUsuarioCadastro");
        return descricaoCampos;
    }

    private static Dictionary<string, string> ObterCamposCadastroCarroceria()
    {
        Dictionary<string, string> descricaoCampos = new Dictionary<string, string>();
        descricaoCampos.Add("Nome da carroceria: ", "NomeCarroceria");
        descricaoCampos.Add("Ativo(0 ou 1): ", "Ativo");
        descricaoCampos.Add("ID do Usuário que está cadastrando: ", "IdUsuarioCadastro");
        return descricaoCampos;
    }

    private static Dictionary<string, string> ObterCamposCadastroMarcaMontadora()
    {
        Dictionary<string, string> descricaoCampos = new Dictionary<string, string>();
        descricaoCampos.Add("Nome da marca montadora: ", "NomeMarcaMontadora");
        descricaoCampos.Add("Ativo(0 ou 1): ", "Ativo");
        descricaoCampos.Add("ID do Usuário que está cadastrando: ", "IdUsuarioCadastro");
        return descricaoCampos;
    }
    private static Dictionary<string, string> ObterCamposCadastroModelo()
    {
        Dictionary<string, string> descricaoCampos = new Dictionary<string, string>();
        descricaoCampos.Add("Nome do modelo: ", "NomeModelo");
        descricaoCampos.Add("Id da Carroceria: ", "IdCarroceria");
        descricaoCampos.Add("Ativo(0 ou 1): ", "Ativo");
        descricaoCampos.Add("ID do Usuário que está cadastrando: ", "IdUsuarioCadastro");
        return descricaoCampos;
    }

    private static Dictionary<string, string> ObterCamposCadastroVeiculo()
    {
        Dictionary<string, string> descricaoCampos = new Dictionary<string, string>();
        descricaoCampos.Add("Id da Marca Montadora: ", "IdMarcaMontadora");
        descricaoCampos.Add("Id do modelo: ", "IdModelo");
        descricaoCampos.Add("Id da carroceria: ", "IdCarroceria"); //to do: remover dessa tabela
        descricaoCampos.Add("Id do tipo de cambio: ", "IdTipoCambio");
        descricaoCampos.Add("ano inicio veículo: ", "AnoInicioVeiculo");
        descricaoCampos.Add("ano fim veículo: ", "AnoFimVeiculo");
        descricaoCampos.Add("Ativo(0 ou 1): ", "Ativo");
        descricaoCampos.Add("ID do Usuário que está cadastrando: ", "IdUsuarioCadastro");
        return descricaoCampos;
    }
    private static Dictionary<string, string> ObterCamposCadastroVendedor()
    {
        Dictionary<string, string> descricaoCampos = new Dictionary<string, string>();
        descricaoCampos.Add("Nome do Vendedor: ", "NomeVendedor");
        
       
        return descricaoCampos;
    }

    static void AtualizarRegistroPorId(string nomeTabela, string nomeCampoId)
    {
        Console.Write($"Digite o ID do {nomeTabela} que deseja atualizar: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = $"SELECT * FROM {nomeTabela} WHERE {nomeCampoId} = @{nomeCampoId}";

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue($"@{nomeCampoId}", id);

                    Dictionary<string, string> updatedValues = new Dictionary<string, string>();

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Detalhes do {nomeTabela} ({nomeCampoId}: {id}):");

                            // Imprimir cabeçalho com os nomes das colunas
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write($"{reader.GetName(i)}\t");
                            }
                            Console.WriteLine();

                            // Imprimir valores das colunas
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write($"{reader[i]}\t");
                            }
                            Console.WriteLine("\n");

                            // Perguntar por novos valores
                            updatedValues = ConstructUpdatedValues(reader);
                        }
                        else
                        {
                            Console.WriteLine($"{nomeTabela} não encontrado.\n");
                        }
                    }

                    if (updatedValues.Any())
                    {
                        // Executar a atualização
                        AtualizarRegistro(connection, id, updatedValues, nomeTabela, nomeCampoId);
                        Console.WriteLine($"{nomeTabela} atualizado com sucesso.\n");
                    }
                }

                connection.Close();
            }
        }
        else
        {
            Console.WriteLine("ID inválido.\n");
        }
    }
    static Dictionary<string, string> ConstructUpdatedValues(SqlDataReader reader)
    {
        Dictionary<string, string> updatedValues = new Dictionary<string, string>();

        Console.WriteLine("Digite os novos valores:");

        for (int i = 1; i < reader.FieldCount; i++)
        {
            Console.Write($"{reader.GetName(i)}: ");
            string novoValor = Console.ReadLine();
            if (!string.IsNullOrEmpty(novoValor))
            updatedValues.Add(reader.GetName(i), novoValor);
        }

        return updatedValues;
    }
    static void AtualizarRegistro(SqlConnection connection, int id, Dictionary<string, string> updatedValues, string nomeTabela, string nomeCampoId)
    {
        string updateQuery = $"UPDATE {nomeTabela} SET ";

        foreach (var keyValue in updatedValues)
        {
            updateQuery += $"{keyValue.Key} = '{keyValue.Value}', ";
        }

        // Remover a vírgula extra no final
        updateQuery = updateQuery.TrimEnd(',', ' ');

        updateQuery += $" WHERE {nomeCampoId} = {id}";

        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
        {
            updateCommand.ExecuteNonQuery();
        }
    }
    static void DeletarPorId(string nomeTabela, string nomeCampoId)
    {
        Console.Write($"Digite o ID do {nomeTabela}: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"DELETE {nomeTabela} WHERE {nomeCampoId} = @{nomeCampoId}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue($"@{nomeCampoId}", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Registro da tabela {nomeTabela} de {nomeCampoId} de valor {id} deletado com sucesso.\n");
                    }
                    else
                    {
                        Console.WriteLine("Registro não encontrado.\n");
                    }
                }
            }
        }
    }
}
